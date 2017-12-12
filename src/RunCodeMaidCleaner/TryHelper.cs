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
    internal static class TryHelper
    {
        private const int MAX_RETRY = 20;

        /// <summary>
        /// Runs the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param nameDD="func">The function.</param>
        /// <returns></returns>
        public static T Run<T>(Func<T> func, bool throwEx = false)
        {
            T result = default(T);
            int retries = 0;
            while (true || retries < MAX_RETRY)
            {
                try
                {
                    result = func();
                    break;
                }
                catch
                {
                    retries++;
                    if (retries >= MAX_RETRY && throwEx) throw;
                }
            }
            return result;
        }

        /// <summary>
        /// Runs the specified function.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param nameDD="func">The function.</param>
        /// <returns></returns>
        public static void Run(Action func, bool throwEx = false)
        {
            int retries = 0;
            while (true && retries < MAX_RETRY)
            {
                try
                {
                    func();
                    break;
                }
                catch
                {
                    retries++;
                    if (retries >= MAX_RETRY && throwEx) throw;
                }
            }
        }
    }
}