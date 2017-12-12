/*
* RunCodeMaidCleaner
*
* RunCodeMaidCleaner es un software que permite ejecutar la  opción "Cleanup"
* de la extesión de Visual Studio "CodeMaid", desde la linea de comandos
* con lo que es posible ejecutarlo dentro de procesos bath de generación de
* código.
*
* El proyecto fue iniciado por José Luis Bautista Martín, el 6 de enero
* de 2016.
*
* Puede modificar y distribuir este software, según le plazca, y usarlo
* para cualquier fin ya sea comercial, personal, educativo, o de cualquier
* índole, siempre y cuando incluya este mensaje, y se permita acceso al
* código fuente.
*
* Este software es código libre, y se licencia bajo LGPL.
*
* Para más información consultar http://www.gnu.org/licenses/lgpl.html
*/

using EnvDTE;
using EnvDTE80;
using RunCodeMaidCleaner.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RunCodeMaidCleaner
{
    internal class CodeMaidCleaner
    {
        private const string folderKindGUID = "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}";

        private EnvDTE.Solution GetSolution(ClearFilesArgs clearFilesArgs)
        {
            EnvDTE.Solution soln = TryHelper.Run(() => System.Activator.CreateInstance(Type.GetTypeFromProgID(Settings.Default.VisualStudio))) as EnvDTE.Solution;

            TryHelper.Run(() => { soln.DTE.MainWindow.Visible = false; });

            if (!String.IsNullOrWhiteSpace(clearFilesArgs.ProyectFile))
            {
                TryHelper.Run(() => { soln.AddFromFile(clearFilesArgs.ProyectFile); });
            }
            else
            {
                soln.Open(clearFilesArgs.SoluctionFile);
            }

            return soln;
        }

        public void ClearFiles(ClearFilesArgs clearFilesArgs)
        {
            try
            {
                TryHelper.Run(() => CleanUp());

                //If it throws exeption you may want to retry couple more times
                EnvDTE.Solution soln = GetSolution(clearFilesArgs);

                if (!String.IsNullOrWhiteSpace(clearFilesArgs.ProyectFile))
                {
                    if (!clearFilesArgs.MinimumOutput) Console.WriteLine(String.Format("Limpiando '{0}'", Path.GetFileName(clearFilesArgs.ProyectFile)));
                }
                else if (!String.IsNullOrWhiteSpace(clearFilesArgs.SoluctionFile))
                {
                    if (!clearFilesArgs.MinimumOutput) Console.WriteLine(String.Format("Limpiando '{0}'", Path.GetFileName(clearFilesArgs.SoluctionFile)));
                }

                if (String.IsNullOrWhiteSpace(clearFilesArgs.FilesFile))
                {
                    TryHelper.Run(() => soln.DTE.Commands.Raise(Settings.Default.GuidCodeMaidCommandCleanupAllCodeString, Settings.Default.CmdIDCodeMaidCleanupAllCode, null, null));
                }
                else
                {
                    ProjectItemIterator iterator = new ProjectItemIterator(soln);
                    String[] files = File.ReadAllLines(clearFilesArgs.FilesFile).Select(f => Path.GetFileName(f)).ToArray();

                    foreach (var addedItem in iterator)
                    {
                        string itemName = TryHelper.Run(() => addedItem.Name);

                        string kind = TryHelper.Run(() => addedItem.Kind);

                        if (kind == ProjectKinds.vsProjectKindSolutionFolder || kind == folderKindGUID) continue;

                        if (files.Select(f => String.Compare(itemName, f, true) == 0).Count() == 0) continue;

                        if (!clearFilesArgs.MinimumOutput)
                        {
                            Console.WriteLine(String.Format("\tLimpiando {0}...", itemName));
                        }

                        TryHelper.Run(() =>
                        {
                            // Console.WriteLine(nameFile);
                            addedItem.Open(Constants.vsViewKindCode);
                            addedItem.Document.Activate();

                            addedItem.Document.DTE.Commands.Raise(Settings.Default.GuidCodeMaidCommandCleanupActiveCodeString, Settings.Default.CmdIDCodeMaidCleanupActiveCode, null, null);

                            addedItem.Save();
                        });

                        if (clearFilesArgs.MinimumOutput) Console.WriteLine(itemName);
                    }
                }

                TryHelper.Run(() => soln.Close());

                if (!String.IsNullOrWhiteSpace(clearFilesArgs.ProyectFile))
                {
                    if (!clearFilesArgs.MinimumOutput) Console.WriteLine(String.Format("Finalizado limpieza '{0}'", Path.GetFileName(clearFilesArgs.ProyectFile)));
                }
                else if (!String.IsNullOrWhiteSpace(clearFilesArgs.SoluctionFile))
                {
                    if (!clearFilesArgs.MinimumOutput) Console.WriteLine(String.Format("Finalizado limpieza '{0}'", Path.GetFileName(clearFilesArgs.SoluctionFile)));
                }
            }
            finally
            {
                TryHelper.Run(() => CleanUp());
            }
        }

        private void CleanUp()
        {
            List<System.Diagnostics.Process> visualStudioProcesses = System.Diagnostics.Process.GetProcesses().Where(p => p.ProcessName.Contains("devenv")).ToList();
            foreach (System.Diagnostics.Process process in visualStudioProcesses)
            {
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                    break;
                }
            }
        }
    }
}