using Microsoft.Data.SqlClient;

namespace HPRBackend.Modules.shard
{
    public class Dbconnection
    {
        static string connectionString = @"workstation id=LikitaDatabase.mssql.somee.com;packet size=4096;user id=bako_SQLLogin_5;pwd=smcccbrnzp;data source=LikitaDatabase.mssql.somee.com;persist security info=False;initial catalog=LikitaDatabase;TrustServerCertificate=True";
        //   static string connectionString = @"Data Source=DESKTOP-JC5UUA5\SQLEXPRESS;Initial Catalog=CNDOMF;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static string connectionStringFORregistre = @"Data Source=DESKTOP-JC5UUA5\SQLEXPRESS;Initial Catalog=REGISTRE;Integrated Security=True ;Connection Timeout=120";
        public static SqlConnection GetConnection()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return cn;
        }
        public static SqlConnection GetConnectionForRegistre()
        {
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection(connectionStringFORregistre);
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return cn;
        }
    }
}
