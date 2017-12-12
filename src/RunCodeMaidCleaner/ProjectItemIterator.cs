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
using System.Collections.Generic;
using System.Linq;

namespace RunCodeMaidCleaner
{
    public class ProjectItemIterator : IEnumerable<EnvDTE.ProjectItem>
    {
        private IEnumerable<EnvDTE.Project> projects;

        public ProjectItemIterator(EnvDTE.Solution solution)
        {
            if (solution == null)
                throw new ArgumentNullException("solution");

            projects = TryHelper.Run<IEnumerable<EnvDTE.Project>>(() => solution.Projects.Cast<EnvDTE.Project>());
        }

        public ProjectItemIterator(IEnumerable<EnvDTE.Project> projects)
        {
            if (projects == null)
                throw new ArgumentNullException("projects");

            this.projects = projects;
        }

        public IEnumerator<EnvDTE.ProjectItem> GetEnumerator()
        {
            foreach (EnvDTE.Project currentProject in TryHelper.Run<EnvDTE.Project[]>(() => this.projects.ToArray()))
            {
                foreach (var currentProjectItem in Enumerate(TryHelper.Run<EnvDTE.ProjectItems>(() => currentProject.ProjectItems)))
                    yield return currentProjectItem;
            }
        }

        private IEnumerable<EnvDTE.ProjectItem> Enumerate(EnvDTE.ProjectItems projectItems)
        {
            foreach (EnvDTE.ProjectItem item in GetProjectItems(projectItems))
            {
                yield return item;

                if (item.SubProject != null)
                {
                    foreach (EnvDTE.ProjectItem childItem in Enumerate(TryHelper.Run<EnvDTE.ProjectItems>(() => item.SubProject.ProjectItems)))
                        yield return childItem;
                }
                else
                {
                    foreach (EnvDTE.ProjectItem childItem in Enumerate(TryHelper.Run<EnvDTE.ProjectItems>(() => item.ProjectItems)))
                        yield return childItem;
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<EnvDTE.ProjectItem> GetProjectItems(EnvDTE.ProjectItems projectItems)
        {
            List<EnvDTE.ProjectItem> result = new List<EnvDTE.ProjectItem>();

            int itemCount = TryHelper.Run(() => projectItems.Count);

            for (int i = 0; i < itemCount; i++)
            {
                result.Add(TryHelper.Run(() => projectItems.Item(i + 1)));
            }

            return result;
        }
    }
}