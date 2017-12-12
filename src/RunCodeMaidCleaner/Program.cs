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

using System;

namespace RunCodeMaidCleaner
{
    internal class Program
    {
        private const string SYNTAX = "RunCodeMaidCleaner.exe /p <proyect.csproj> /a <solution.sln> /f <files.txt> /mo";

        private static void Main(string[] args)
        {
            ClearFilesArgs result = GetParameters(args);

            if (result != null)
            {
                CodeMaidCleaner cleaner = new CodeMaidCleaner();
                cleaner.ClearFiles(result);
            }
        }

        public static ClearFilesArgs GetParameters(String[] args)
        {
            ClearFilesArgs result = new ClearFilesArgs();

            if (args.Length == 0)
            {
                Console.WriteLine(String.Format("{0}{1}{1}", SYNTAX, Environment.NewLine, Environment.NewLine));
                return null;
            }

            for (int i = 0; i < args.Length; i++)
            {
                string parameter = args[i].ToLower();

                switch (parameter)
                {
                    case "/?":
                    case "/h":
                        Console.WriteLine(String.Format("{0}{1}{1}", SYNTAX, Environment.NewLine, Environment.NewLine));
                        return null;

                    case "/p":
                        result.ProyectFile = args[++i];
                        break;

                    case "/s":
                        result.SoluctionFile = args[++i];
                        break;

                    case "/f":
                        result.FilesFile = args[++i];
                        break;

                    case "/mo":
                        result.MinimumOutput = true;
                        break;
                }
            }

            return result;
        }
    }
}