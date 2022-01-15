using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Oracle;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection; 
using System.Threading.Tasks;
using EPM.Plan.Dto.Extensions;

namespace EPM.Plan.Repository.Repository
{
    public class PlanRepository : IPlanRepository
    {
        public static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.1.1.50)(PORT=1571)))  (CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=PROD)));User Id=XXER;Pooling=true;Password=XXER123;";

        public  DataTable QueryFill(DataTable dt, string sql)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                new OracleDataAdapter(new OracleCommand(sql, connection)).Fill(dt);
                return dt;
            }
        }

        public  DataTable QueryFill(string sql)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                DataTable dt = new DataTable();
                new OracleDataAdapter(new OracleCommand(sql, connection)).Fill(dt);
                return dt;
            }
        }

        public  IEnumerable<object> GetResult<T>(IEnumerable<T> list, string propName)
        {
            var retList = new List<object>();

            var prop = typeof(T).GetProperty(propName);
            if (prop == null)
                throw new Exception("Property not found");

            retList = list.Select(c => prop.GetValue(c)).ToList();
            return retList;
        }

        public  void BulkInsertSecond<T>(List<T> obj)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                T d = Activator.CreateInstance<T>();
                Type dt = d.GetType();
                string tableName = typeof(T).Name;
                if (((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))) != null)
                    tableName = ((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))).Name;
                string fieldNames = string.Empty;
                string insertParameters = string.Empty;
                string updateParameters = string.Empty;
                PropertyInfo[] pi = dt.GetProperties();
                OracleCommand command = new OracleCommand();
                for (int i = 0; i < pi.Length; i++)
                {
                    if (((KeyAttribute)Attribute.GetCustomAttribute(pi[i], typeof(KeyAttribute))) == null)
                    {
                        fieldNames += (fieldNames == string.Empty ? string.Empty : ",") + pi[i].Name;
                        insertParameters += (insertParameters == string.Empty ? string.Empty : ",") + "@" + pi[i].Name;

                    }
                }
                connection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2}) ", tableName, fieldNames, insertParameters), obj);
            }
        }

        public  void BulkInsert<T>(List<T> obj)
        {
            using (var connection = new OracleConnection(connectionString))
            {
                connection.Open();
                T d = Activator.CreateInstance<T>();
                Type dt = d.GetType();
                string tableName = typeof(T).Name;
                if (((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))) != null)
                    tableName = ((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))).Name;
                string fieldNames = string.Empty;
                string insertParameters = string.Empty;
                string updateParameters = string.Empty;
                PropertyInfo[] pi = dt.GetProperties();
                OracleCommand command = new OracleCommand();
                for (int i = 0; i < pi.Length; i++)
                {
                    if (((KeyAttribute)Attribute.GetCustomAttribute(pi[i], typeof(KeyAttribute))) == null)
                    {
                        fieldNames += (fieldNames == string.Empty ? string.Empty : ",") + pi[i].Name;
                        insertParameters += (insertParameters == string.Empty ? string.Empty : ",") + ":" + pi[i].Name;
                        using (OracleParameter prm = new OracleParameter())
                        {
                            prm.ParameterName = ":" + pi[i].Name;
                            prm.DbType = pi[i].PropertyType.DbType();
                            prm.Value = GetResult(obj, pi[i].Name).ToArray();
                            command.Parameters.Add(prm);
                        }
                    }
                }
                command.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2}) ", tableName, fieldNames, insertParameters);
                command.Connection = connection;
                command.ArrayBindCount = obj.Count;

                command.ExecuteNonQuery();
            }
        } 

        public  T Serialize<T>(T obj)
        {
            using (OracleConnection sqlConnection = new OracleConnection(connectionString))
            {
                T d = Activator.CreateInstance<T>();
                Type dt = d.GetType();
                string tableName = typeof(T).Name;
                if (((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))) != null)
                    tableName = ((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))).Name;
                string fieldNames = string.Empty;
                string insertParameters = string.Empty;
                string updateParameters = string.Empty;
                string keyName = string.Empty;
                object keyValue = 0;
                PropertyInfo keyInfo = null;
                PropertyInfo[] pi = dt.GetProperties();
                DynamicParameters parameters = new DynamicParameters();
                for (int i = 0; i < pi.Length; i++)
                {
                    if (((KeyAttribute)Attribute.GetCustomAttribute(pi[i], typeof(KeyAttribute))) == null)
                    {

                        //if (!(pi[i].PropertyType.DbType() == System.Data.DbType.DateTime && (pi[i].GetValue(obj, null) == null || ((DateTime)pi[i].GetValue(obj, null)).Year == 1)))
                        //{
                            fieldNames += (fieldNames == string.Empty ? string.Empty : ",") + pi[i].Name;
                            insertParameters += (insertParameters == string.Empty ? string.Empty : ",") + ":" + pi[i].Name;
                            updateParameters += (updateParameters == string.Empty ? string.Empty : ",") + pi[i].Name + "=:" + pi[i].Name;
                            parameters.Add(pi[i].Name, pi[i].GetValue(obj, null));
                        //}
                    }
                    else
                    {
                        keyName = pi[i].Name;
                        keyValue = pi[i].GetValue(obj, null);
                        keyInfo = pi[i];
                    }
                }
                if (Convert.ToInt64(keyValue) > 0)
                    sqlConnection.Execute(string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", tableName, updateParameters, keyName, keyValue), parameters, null, commandType: System.Data.CommandType.Text);
                else
                {
                    if (keyInfo != null)
                    {
                        parameters.Add(name: "LAST_INSERT_ID_ORACLE", dbType: keyInfo.PropertyType.DbType(), direction: ParameterDirection.Output);
                        sqlConnection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2}) RETURNING {3} INTO :LAST_INSERT_ID_ORACLE", tableName, fieldNames, insertParameters, keyName), parameters, null, commandType: System.Data.CommandType.Text);
                        object identity = parameters.Get<object>("LAST_INSERT_ID_ORACLE");
                        if (keyInfo.GetValue(obj, null).GetType().ToString() == "System.Int16")
                            keyInfo.SetValue(obj, Convert.ToInt16(identity), null);
                        if (keyInfo.GetValue(obj, null).GetType().ToString() == "System.Int32")
                            keyInfo.SetValue(obj, Convert.ToInt32(identity), null);
                        else keyInfo.SetValue(obj, Convert.ToInt64(identity), null);
                    }
                    else sqlConnection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, fieldNames, insertParameters), parameters, null, commandType: System.Data.CommandType.Text);

                }
                return obj;
            }
        }

        public  async Task<T> SerializeAsync<T>(T obj)
        {
            using (OracleConnection sqlConnection = new OracleConnection(connectionString))
            {
                T d = Activator.CreateInstance<T>();
                Type dt = d.GetType();
                string tableName = typeof(T).Name;
                if (((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))) != null)
                    tableName = ((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))).Name;
                string fieldNames = string.Empty;
                string insertParameters = string.Empty;
                string updateParameters = string.Empty;
                string keyName = string.Empty;
                object keyValue = 0;
                PropertyInfo keyInfo = null;
                PropertyInfo[] pi = dt.GetProperties();
                DynamicParameters parameters = new DynamicParameters();
                for (int i = 0; i < pi.Length; i++)
                {
                    if (((KeyAttribute)Attribute.GetCustomAttribute(pi[i], typeof(KeyAttribute))) == null)
                    {
                        fieldNames += (fieldNames == string.Empty ? string.Empty : ",") + pi[i].Name;
                        insertParameters += (insertParameters == string.Empty ? string.Empty : ",") + ":" + pi[i].Name;
                        updateParameters += (updateParameters == string.Empty ? string.Empty : ",") + pi[i].Name + "=:" + pi[i].Name;

                        if (pi[i].PropertyType.DbType() == System.Data.DbType.DateTime && ((DateTime)pi[i].GetValue(obj, null)).Year == 1)
                            parameters.Add(pi[i].Name, Convert.DBNull);
                        else
                            parameters.Add(pi[i].Name, pi[i].GetValue(obj, null));
                    }
                    else
                    {
                        keyName = pi[i].Name;
                        keyValue = pi[i].GetValue(obj, null);
                        keyInfo = pi[i];
                    }
                }
                if (Convert.ToInt64(keyValue) > 0)
                    await sqlConnection.ExecuteAsync(string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", tableName, updateParameters, keyName, keyValue), parameters, null, commandType: System.Data.CommandType.Text);
                else
                {
                    if (keyInfo != null)
                    {
                        parameters.Add(name: "LAST_INSERT_ID_ORACLE", dbType: keyInfo.PropertyType.DbType(), direction: ParameterDirection.Output);
                        sqlConnection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2}) RETURNING {3} INTO :LAST_INSERT_ID_ORACLE", tableName, fieldNames, insertParameters, keyName), parameters, null, commandType: System.Data.CommandType.Text);
                        object identity = parameters.Get<object>("LAST_INSERT_ID_ORACLE");
                        if (keyInfo.GetValue(obj, null).GetType().ToString() == "System.Int16")
                            keyInfo.SetValue(obj, Convert.ToInt16(identity), null);
                        if (keyInfo.GetValue(obj, null).GetType().ToString() == "System.Int32")
                            keyInfo.SetValue(obj, Convert.ToInt32(identity), null);
                        else keyInfo.SetValue(obj, Convert.ToInt64(identity), null);
                    }
                    else await sqlConnection.ExecuteAsync(string.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, fieldNames, insertParameters), parameters, null, commandType: System.Data.CommandType.Text);

                }
                return obj;
            }
        }

        public  T Deserialize<T>(int id) where T : class
        {
            using (var sqlConnection = new OracleConnection(connectionString))
            {
                T d = Activator.CreateInstance<T>();
                Type dt = d.GetType();
                string tableName = typeof(T).Name;
                if (((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))) != null)
                    tableName = ((TableAttribute)Attribute.GetCustomAttribute(dt, typeof(TableAttribute))).Name;
                IEnumerable<PropertyInfo> props = typeof(T).GetRuntimeProperties();
                string where = "";
                foreach (PropertyInfo p in props)
                    if (((KeyAttribute)Attribute.GetCustomAttribute(p, typeof(KeyAttribute))) != null)
                    {
                        where = " WHERE " + p.Name + "=" + id;
                        break;
                    }
                string sql = "SELECT * FROM " + tableName + where;
                d = sqlConnection.QueryFirstOrDefault<T>(sql);
                if (d == null)
                    d = Activator.CreateInstance<T>();
                return d;
            }
        }

        public  T Deserialize<T>(string sql) where T : class
        {
            using (var sqlConnection = new OracleConnection(connectionString))
            {
                T d;
                d = sqlConnection.Query<T>(sql).ToList().FirstOrDefault<T>();
                if (d == null)
                    d = Activator.CreateInstance<T>();
                return d;

            }
        }

        public  List<T> DeserializeListPROC<T>(string sql, OracleDynamicParameters parameters) where T : class
        {
            using (var sqlConnection = new OracleConnection(connectionString))
            {
                return sqlConnection.Query<T>(sql, param: parameters, commandType: CommandType.StoredProcedure).ToList();

            }
        } 

        public  List<T> DeserializeList<T>(string sql) where T : class
        {
            using (var sqlConnection = new OracleConnection(connectionString))
            {
                return sqlConnection.Query<T>(sql).ToList();
            }
        }
       
        public  int ExecSql(string sqlText)
        {
            using (OracleConnection sqlConnection = new OracleConnection(connectionString))
            {
                sqlConnection.Open();
                OracleCommand sc = new OracleCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteNonQuery();
            }
        }

        public  object GetSQLValue(string sqlText)
        {
            using (OracleConnection sqlConnection = new OracleConnection(connectionString))
            {
                sqlConnection.Open();
                OracleCommand sc = new OracleCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteScalar();
            }
        }

        public  int ReadInteger(string sql)
        {
            object obj = GetSQLValue(sql);
            if ((obj == null) || (obj.ToString() == ""))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        public  bool ReadBoolean(string sql)
        {
            object obj = GetSQLValue(sql);
            if ((obj == null) || (obj.ToString() == ""))
                return false;
            else

                return Boolean.Parse(obj.ToString());
        }

        public  string ReadString(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public  double ReadDouble(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return 0;
            else
                return (double)obj;
        }

        public  decimal ReadDecimal(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null || obj.GetType().Name == "DBNull")
                return 0;
            else
                return (decimal)obj;
        }

        public  DateTime ReadDateTime(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return new DateTime();
            else
                return (DateTime)obj;
        }
    }
}
