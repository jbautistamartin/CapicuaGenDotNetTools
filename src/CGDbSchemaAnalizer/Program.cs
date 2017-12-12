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

using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace CapicuaGen.CGDbSchemaAnalizer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StringBuilder configurationInput = new StringBuilder();
            string currentConfigurationInput;

            while ((currentConfigurationInput = Console.ReadLine()) != null)
            {
                configurationInput.Append(currentConfigurationInput);
            }

            JsonSerializer serializer = new JsonSerializer();

            DataAccessConfiguration[] configurations = JsonConvert.DeserializeObject<DataAccessConfiguration[]>(configurationInput.ToString());

            Console.WriteLine(configurationInput.ToString());

            foreach (DataAccessConfiguration configuracion in configurations)
            {
                //  EntitySchema schema = AnalizeConfiguratio(configuration);
            }
        }

        private static EntitySchema AnalizeConfiguratio(DataAccessConfiguration configuration)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(configuration.NetDbProviderFactory);
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = configuration.ConnectionString;

                foreach (TableConfiguration table in configuration.Tables)
                {
                    string query = $"SELECT * FROM {table.TableName} WHERE 1=0";
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    DbDataAdapter adapter = factory.CreateDataAdapter();
                    adapter.SelectCommand = command;
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);

                    foreach (DataColumn column in dataset.Tables[0].Columns)
                    {
                        EntityFieldSchema field = new EntityFieldSchema
                        {
                            allow_null = column.AllowDBNull,
                            default_value = column.DefaultValue,
                            identity = column.AutoIncrement,
                            name = column.ColumnName,
                            primary_key = column.Unique,
                            size = column.MaxLength,
                            sql_name = column.ColumnName,
                            sql_type = GetSqlType(column.DataType)
                        };
                    }
                }
            }

            throw new NotImplementedException();
        }

        private static string GetSqlType(Type dataType)
        {
            return null;
        }
    }
}