using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using XGENO.DBHelpers.Core.Database;
using XGENO.DBHelpers.Utilities;

namespace XGENO.DBHelpers.Extensions
{
    public static class SqlConnectionExtensions
    {
        //// Text Queries

        public static async Task<List<T>> ExecuteQuery<T>(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            List<T> _lstObjects;

            using (MultiReader _multiReader = await dbConnection.ExecuteMultiResultQuery(sqlStatement, sqlParameters))
            {
                _lstObjects = _multiReader.Read<T>();
            }

            return _lstObjects;
        }

        public static async Task<MultiReader> ExecuteMultiResultQuery(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            var dataSet = await SqlObjects.GetDataSet(dbConnection, sqlStatement, false, sqlParameters);

            return new MultiReader(dataSet);
        }

        //// Stored Procedure Calls

        public static async Task<List<T>> ExecuteStoredProcedure<T>(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            List<T> _lstObjects;

            using (MultiReader _multiReader = await dbConnection.ExecuteMultiResultStoredProcedure(sqlStatement, sqlParameters))
            {
                _lstObjects = _multiReader.Read<T>();
            }

            return _lstObjects;
        }

        public static async Task<MultiReader> ExecuteMultiResultStoredProcedure(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            var dataSet = await SqlObjects.GetDataSet(dbConnection, sqlStatement, true, sqlParameters);

            return new MultiReader(dataSet);
        }

        //// No result queries

        public static async Task ExecuteNonQueryText(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            using MultiReader _multiReader = await dbConnection.ExecuteMultiResultQuery(sqlStatement, sqlParameters);
        }

        public static async Task ExecuteNonQueryStoredProcedure(this SqlConnection dbConnection, string sqlStatement, params SqlParameter[] sqlParameters)
        {
            using MultiReader _multiReader = await dbConnection.ExecuteMultiResultStoredProcedure(sqlStatement, sqlParameters);
        }

    }
}
