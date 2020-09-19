using System;
using System.Data.SqlClient;

namespace XGENO.DBHelpers.Extensions
{
    public static class SqlParameterExtenstions
    {
        public static SqlParameter ToSqlParam(this object paramValue, string paramName)
        {
            return new SqlParameter()
            {
                IsNullable = true,
                ParameterName = paramName.StartsWith("@") ? paramName : "@" + paramName,
                Value = (paramValue ?? DBNull.Value)
            };
        }

    }
}
