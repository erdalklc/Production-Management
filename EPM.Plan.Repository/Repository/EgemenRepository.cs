using Dapper;
using EPM.Plan.Dto.Extensions;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EPM.Plan.Repository.Repository
{
    public class EgemenRepository:IEgemenRepository
    {
         
        static string LACOSTECNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.95/3050:C:\egemen\kt01\dataserver\data\lacoste\kt01data.gdb;Charset=UTF8";
        static string HAMIPLIKCNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.9/3050:C:\Egemen\eb01\DataServer\data\ErenHamIplik\EB01DATA.GDB;Charset=UTF8";
        static string KUMASBOYACNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.100/3050:D:\egemen\eb01\dataserver\data\MODERNKUMASBOYA\EB01DATA.GDB;Charset=UTF8";
        static string IPLIKBOYACNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.100/3050:D:\egemen\eb01\dataserver\data\MODERNIPLIKBOYA\EB01DATA.GDB;Charset=UTF8";
        static string TRIKOCNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.23/3050:C:\Egemen\Orme\OrmeDataServer\Data\EREN TRIKO\ORME.GDB;Charset=UTF8";
        static string ORMECNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.23/3050:C:\Egemen\Orme\OrmeDataServer\Data\EREN ORME\ORME.GDB;Charset=UTF8";
        static string TEKSTILCNN = @"User ID=sysdba;Password=masterkey;Database=192.168.2.100/3050:C:\Egemen\Orme\OrmeDataServer\Data\EREN TEKSTİL\ORME.GDB;Charset=UTF8";

        public DataTable QueryFill(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            using (var connection = new FbConnection(GetConnectionString(dB)))
            {
                DataTable dt = new DataTable();
                new FbDataAdapter(new FbCommand(sql, connection)).Fill(dt);
                return dt;
            }
        }

        private string GetConnectionString(FirebirdConnectionDB dB)
        {
            string conStr = "";
            switch (dB)
            {
                case FirebirdConnectionDB.DEVANLAY:
                    conStr = LACOSTECNN;
                    break;
                case FirebirdConnectionDB.HAMIPLIK:
                    conStr = HAMIPLIKCNN;
                    break;
                case FirebirdConnectionDB.KUMASBOYA:
                    conStr = KUMASBOYACNN;
                    break;
                case FirebirdConnectionDB.IPLIKBOYA:
                    conStr = IPLIKBOYACNN;
                    break;
                case FirebirdConnectionDB.ORME:
                    conStr = ORMECNN;
                    break;
                case FirebirdConnectionDB.TRIKO:
                    conStr = TRIKOCNN;
                    break;
                case FirebirdConnectionDB.ERENTEKSTIL:
                    conStr = TEKSTILCNN;
                    break;
                default:
                    break;
            }
            return conStr;
        }

        public int ExecSql(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            using (FbConnection sqlConnection = new FbConnection(GetConnectionString(dB)))
            {
                sqlConnection.Open();
                FbCommand sc = new FbCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteNonQuery();
            }
        }

        public object GetSQLValue(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            using (FbConnection sqlConnection = new FbConnection(GetConnectionString(dB)))
            {
                sqlConnection.Open();
                FbCommand sc = new FbCommand(sqlText, sqlConnection);
                sc.CommandTimeout = 0;
                return sc.ExecuteScalar();
            }

        }
        public List<T> DeserializeList<T>(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY) where T : class
        {
            using (var sqlConnection = new FbConnection(GetConnectionString(dB)))
            {
                return sqlConnection.Query<T>(sql).ToList();
            }
        }
        public int ReadInteger(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sql, dB);
            if ((obj == null) || (obj.ToString() == ""))
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        public bool ReadBoolean(string sql, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sql, dB);
            if ((obj == null) || (obj.ToString() == ""))
                return false;
            else

                return Boolean.Parse(obj.ToString());
        }

        public string ReadString(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sqlText, dB);
            if (obj == null)
                return "";
            else
                return obj.ToString();
        }

        public double ReadDouble(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sqlText, dB);
            if (obj == null)
                return 0;
            else
                return (double)obj;
        }

        public decimal ReadDecimal(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sqlText, dB);
            if (obj == null || obj.GetType().Name == "DBNull")
                return 0;
            else
                return (decimal)obj;
        }

        public DateTime ReadDateTime(string sqlText, FirebirdConnectionDB dB = FirebirdConnectionDB.DEVANLAY)
        {
            object obj = GetSQLValue(sqlText, dB);
            if (obj == null)
                return new DateTime();
            else
                return (DateTime)obj;
        }
    }
}
