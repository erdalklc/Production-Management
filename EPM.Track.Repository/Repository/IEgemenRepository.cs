using EPM.Track.Dto.Extensions; 
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Track.Repository.Repository
{
    public interface IEgemenRepository
    {
        public DataTable QueryFill(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public int ExecSql(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public object GetSQLValue(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public List<T> DeserializeList<T>(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY) where T : class;

        public int ReadInteger(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public bool ReadBoolean(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public string ReadString(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public double ReadDouble(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public decimal ReadDecimal(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);

        public DateTime ReadDateTime(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY);
    }
}
