using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using EPM.Fason.Repository.Extensions;
using EPM.Tools.Helpers;

namespace EPM.Fason.Repository.Repository
{
    public class FasonRepository : IFasonRepository
    {
         string connectionString = @"";

        public FasonRepository()
        {
            if (EPMServiceConfiguration.IsDevelopment())
                connectionString = EPM.Tools.Helpers.EPMServiceConfiguration.GetConfig().GetSection("SQLSERVERDEV").Value;
            else connectionString = EPM.Tools.Helpers.EPMServiceConfiguration.GetConfig().GetSection("SQLSERVERAPP").Value;
        }
        public T Deserialize<T>(int id) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
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

        public T Deserialize<T>(string sql) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                T d;
                d = sqlConnection.Query<T>(sql).ToList().FirstOrDefault<T>();
                if (d == null)
                    d = Activator.CreateInstance<T>();
                return d;

            }
        }

        public List<T> DeserializeList<T>(string sql) where T : class
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                return sqlConnection.Query<T>(sql).ToList();
            }
        }

        public int ExecSql(string sqlText)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sc = new SqlCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteNonQuery();
            }
        }

        public IEnumerable<object> GetResult<T>(IEnumerable<T> list, string propName)
        {
            var retList = new List<object>();

            var prop = typeof(T).GetProperty(propName);
            if (prop == null)
                throw new Exception("Property not found");

            retList = list.Select(c => prop.GetValue(c)).ToList();
            return retList;
        }

        public object GetSQLValue(string sqlText)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                SqlCommand sc = new SqlCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteScalar();
            }
        }

        public DataTable QueryFill(DataTable dt, string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                new SqlDataAdapter(new SqlCommand(sql, connection)).Fill(dt);
                return dt;
            }
        }

        public DataTable QueryFill(string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                new SqlDataAdapter(new SqlCommand(sql, connection)).Fill(dt);
                return dt;
            }
        }

        public bool ReadBoolean(string sql)
        {
            object obj = GetSQLValue(sql);
            if ((obj == null) || (obj.ToString() == ""))
                return false;
            else

                return Boolean.Parse(obj.ToString());
        }

        public DateTime ReadDateTime(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return new DateTime();
            else
                return (DateTime)obj;
        }

        public decimal ReadDecimal(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null || obj.GetType().Name == "DBNull")
                return 0;
            else
                return (decimal)obj;
        }

        public double ReadDouble(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return 0;
            else
                return (double)obj;
        }

        public int ReadInteger(string sql)
        {
            object obj = GetSQLValue(sql);
            if ((obj == null) || (obj.ToString() == ""))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        public string ReadString(string sqlText)
        {
            object obj = GetSQLValue(sqlText);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }
        public string DateTimeString(DateTime datetime)
        {
            return "{ ts '" + datetime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'}";
        }
        public T Serialize<T>(T obj)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
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
                    if (!pi[i].PropertyType.Name.StartsWith("List"))
                    {
                        if (((KeyAttribute)Attribute.GetCustomAttribute(pi[i], typeof(KeyAttribute))) == null)
                        {

                            if (!(pi[i].PropertyType.DbType() == System.Data.DbType.DateTime && (pi[i].GetValue(obj, null) == null || ((DateTime)pi[i].GetValue(obj, null)).Year == 1)))
                            {
                                fieldNames += (fieldNames == string.Empty ? string.Empty : ",") + pi[i].Name;
                                insertParameters += (insertParameters == string.Empty ? string.Empty : ",") + "@" + pi[i].Name;
                                updateParameters += (updateParameters == string.Empty ? string.Empty : ",") + pi[i].Name + "=@" + pi[i].Name;
                                parameters.Add(pi[i].Name, pi[i].GetValue(obj, null));
                            }
                        }
                        else
                        {
                            keyName = pi[i].Name;
                            keyValue = pi[i].GetValue(obj, null);
                            keyInfo = pi[i];
                        }
                    }

                }
                if (Convert.ToInt64(keyValue) > 0)
                    sqlConnection.Execute(string.Format("UPDATE {0} SET {1} WHERE {2} = {3}", tableName, updateParameters, keyName, keyValue), parameters, null, commandType: System.Data.CommandType.Text);
                else
                {
                    if (keyInfo != null)
                    {
                        parameters.Add(name: "@TABLE_INSERT_IDENTITY", dbType: keyInfo.PropertyType.DbType(), direction: ParameterDirection.Output);
                        sqlConnection.Execute(string.Format("INSERT INTO {0} ({1}) VALUES ({2}); SET @TABLE_INSERT_IDENTITY = SCOPE_IDENTITY()", tableName, fieldNames, insertParameters, keyName), parameters, null, commandType: System.Data.CommandType.Text);
                        object identity = parameters.Get<object>("@TABLE_INSERT_IDENTITY");
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

        
    }
}
