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
    /// <summary>
    ///  Table Configuration
    /// </summary>
    public class TableConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the table.
        /// </summary>
        /// <value>
        /// The name of the table.
        /// </value>
        public string TableName { get; set; }
    }

    /// <summary>
    /// View Configurtion
    /// </summary>
    public class ViewConfigurtion
    {
        /// <summary>
        /// Gets or sets the name of the view.
        /// </summary>
        /// <value>
        /// The name of the view.
        /// </value>
        public string ViewName { get; set; }
    }

    /// <summary>
    /// Stored Procedure Configuration
    /// </summary>
    public class StoredProcedureConfiguration
    {
        /// <summary>
        /// Gets or sets the name of the stored procedure.
        /// </summary>
        /// <value>
        /// The name of the stored procedure.
        /// </value>
        public string StoredProcedureName { get; set; }
    }

    /// <summary>
    /// Data Access Configuration
    /// </summary>
    public class DataAccessConfiguration
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the database.
        /// </summary>
        /// <value>
        /// The name of the database.
        /// </value>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        public IEnumerable<TableConfiguration> Tables { get; set; }

        /// <summary>
        /// Gets or sets the views.
        /// </summary>
        /// <value>
        /// The views.
        /// </value>
        public IEnumerable<TableConfiguration> Views { get; set; }

        /// <summary>
        /// Gets or sets the stored procedures.
        /// </summary>
        /// <value>
        /// The stored procedures.
        /// </value>
        public IEnumerable<TableConfiguration> StoredProcedures { get; set; }

        /// <summary>
        /// Gets or sets the net database provider factory.
        /// </summary>
        /// <value>
        /// The net database provider factory.
        /// </value>
        public string NetDbProviderFactory { get; set; }
    }
}