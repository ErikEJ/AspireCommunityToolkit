using Microsoft.Data.SqlClient;

namespace CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects;

internal static class AdsParameterBuilder
{
    public static string CreateParameters(string connectionString)
    {
        var builder = new SqlConnectionStringBuilder(connectionString);

        //ads    -c connect -P MSSQL -T SqlLogin -U sa -S 127.0.0.1,64198 -D TargetDatabase -Z "{\"TrustServerCertificate\":\"True\",\"Password\":\"+HaXbF_wfT4-9W*fa2Pn4R\"}"
        return $"-c connect -P MSSQL -T SqlLogin -U {builder.UserID} -S {builder.DataSource} -D {builder.InitialCatalog} -Z \"{{\\\"TrustServerCertificate\\\":\\\"{builder.TrustServerCertificate}\\\",\\\"Password\\\":\\\"{builder.Password}\\\"}}\"";
    }
}
