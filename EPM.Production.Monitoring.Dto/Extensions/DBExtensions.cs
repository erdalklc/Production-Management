using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EPM.Production.Monitoring.Dto.Extensions
{
    public static class DBExtensions
    {
        public static DbType DbType(this Type type)
        {

            if (type.FullName.Contains("System.Integer"))
            {
                return System.Data.DbType.Int32;
            }
            else if (type.FullName.Contains("System.Int16"))
            {
                return System.Data.DbType.Int32;
            }
            else if (type.FullName.Contains("System.Int32"))
            {
                return System.Data.DbType.Int32;
            }
            else if (type.FullName.Contains("System.Int64"))
            {
                return System.Data.DbType.Int64;
            }
            else if (type.FullName.Contains("System.Single"))
            {
                return System.Data.DbType.Currency;
            }
            else if (type.FullName.Contains("System.Float"))
            {
                return System.Data.DbType.Currency;
            }
            else if (type.FullName.Contains("System.Decimal"))
            {
                return System.Data.DbType.Decimal;
            }
            else if (type.FullName.Contains("System.Double"))
            {
                return System.Data.DbType.Decimal;
            }
            else if (type.FullName.Contains("System.DateTime") || type.FullName.Contains("System.Nullable`1[[System.DateTime"))
            {
                return System.Data.DbType.DateTime;
            }
            else if (type.FullName.Contains("System.String"))
            {
                return System.Data.DbType.String;
            }
            else if (type.FullName.Contains("System.Boolean"))
            {
                return System.Data.DbType.Boolean;
            }
            else
            {
                return System.Data.DbType.String;
            }
        }
    }
}
