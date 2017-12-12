/*
* CGDbSchemaAnalizer
*
* CGDbSchemaAnalizer es una herramienta que analizar el esquema de una base de datos
* y dar información al respecto utilizable como parte de una generación
* por CapicuaGen
*
* El proyecto fue iniciado por José Luis Bautista Martín, el 1 de diciembre de 2017
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

using System.Collections.Generic;

namespace CapicuaGen.CGDbSchemaAnalizer
{
    internal class EntitySchema
    {
        private string name { get; set; }
        private IEnumerable<EntityFieldSchema> fields { get; set; }

        private string sql_name { get; set; }
    }

    internal class EntityFieldSchema
    {
        public string name { get; set; }

        public string type { get; set; }

        public int size { get; set; }

        public bool allow_null { get; set; }
        public object default_value { get; set; }

        public bool primary_key { get; set; }
        public string sql_type { get; set; }
        public bool identity { get; set; }
        public string sql_name { get; set; }
    }
}