namespace CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects.Tests;

public class AdsParameterBuilderTests
{
    [Fact]
    public void Valid_Parameter_Is_Created()
    {       
        // Act
        var command = AdsParameterBuilder.CreateParameters("Server=127.0.0.1,52078;User ID=sa;Password=4)6uVx9cmUPnXqGq~FF)P.;TrustServerCertificate=true;Database=Default");
        //-c connect -P MSSQL -T SqlLogin -U sa -S 127.0.0.1,64198 -D TargetDatabase -Z "{\"TrustServerCertificate\":\"True\",\"Password\":\"+HaXbF_wfT4-9W*fa2Pn4R\"}"

        // Assert
        Assert.Equal("-c connect -P MSSQL -T SqlLogin -U sa -S 127.0.0.1,52078 -D Default -Z \"{\\\"TrustServerCertificate\\\":\\\"True\\\",\\\"Password\\\":\\\"4)6uVx9cmUPnXqGq~FF)P.\\\"}\"", command);
    }
}