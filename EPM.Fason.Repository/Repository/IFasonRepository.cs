using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Fason.Repository.Repository
{
    public interface IFasonRepository
    { 
        public DataTable QueryFill(DataTable dt, string sql);

        public DataTable QueryFill(string sql);

        public IEnumerable<object> GetResult<T>(IEnumerable<T> list, string propName);

        public  T Serialize<T>(T obj); 

        public T Deserialize<T>(int id) where T : class;

        public T Deserialize<T>(string sql) where T : class;

        public List<T> DeserializeList<T>(string sql) where T : class; 

        public int ExecSql(string sqlText);

        public object GetSQLValue(string sqlText);

        public int ReadInteger(string sql);

        public bool ReadBoolean(string sql);

        public string ReadString(string sqlText);

        public double ReadDouble(string sqlText);

        public decimal ReadDecimal(string sqlText);

        public DateTime ReadDateTime(string sqlText);
    }
}
