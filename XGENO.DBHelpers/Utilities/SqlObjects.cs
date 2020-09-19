using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace XGENO.DBHelpers.Utilities
{
    internal static class SqlObjects
    {
        internal static SqlCommand GetCommand(SqlConnection dbConnection, string sqlStatement, bool isStoredProc, params SqlParameter[] sqlParameters)
        {
            SqlCommand _dbCommand = new SqlCommand()
            {
                CommandTimeout = 0,
                Connection = dbConnection,
                CommandType = isStoredProc ? CommandType.StoredProcedure : CommandType.Text,
                CommandText = sqlStatement
            };

            if ((bool)(sqlParameters?.Any()))
                _dbCommand.Parameters.AddRange(sqlParameters);

            return _dbCommand;
        }

        internal static async Task<DataSet> GetDataSet(SqlConnection dbConnection, string sqlStatement, bool isStoredProc, params SqlParameter[] sqlParameters)
        {
            DataSet _dsData = new DataSet();

            SqlDataAdapter _dbAdapter = new SqlDataAdapter(GetCommand(dbConnection, sqlStatement, isStoredProc, sqlParameters));
            await Task.Run(() => _dbAdapter.Fill(_dsData));

            return _dsData;
        }
    }
}
