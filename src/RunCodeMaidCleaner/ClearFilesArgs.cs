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

namespace RunCodeMaidCleaner
{
    internal class ClearFilesArgs
    {
        public string SoluctionFile { get; set; }

        public string ProyectFile { get; set; }

        public string FilesFile { get; set; }

        public bool MinimumOutput { get; set; }
    }
}