using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace netCryptWebApi
{
    public class UsersDAO
    {
        private IConfiguration _Configuration;

        public UsersDAO(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public User Find(string userToken)
        {
            using(SqlConnection conn = new SqlConnection(_Configuration.GetConnectionString("SANDBOX")))
            {
                return conn.QueryFirstOrDefault<User>(
                    "SELECT TOK_USER as UserToken, TOK_KEY as UserKey, TOK_ATIVO as Active " +
                    "FROM SEC_USER_TOKEN " +
                    "WHERE TOK_USER = @UserToken", new { UserToken = userToken });
            }
        }
    }
}