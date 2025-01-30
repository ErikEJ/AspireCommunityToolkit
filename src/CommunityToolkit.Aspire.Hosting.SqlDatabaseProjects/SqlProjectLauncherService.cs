using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CommunityToolkit.Aspire.Hosting.SqlDatabaseProjects;

internal class SqlProjectLauncherService(ResourceLoggerService resourceLoggerService)
{
    private readonly bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public async Task LaunchAzureDataStudio(IResourceWithDacpac resource, IResourceWithConnectionString target, string? targetDatabaseName, CancellationToken cancellationToken)
    {
        var logger = resourceLoggerService.GetLogger(resource);

        try
        {
            var connectionString = await target.ConnectionStringExpression.GetValueAsync(cancellationToken);
            if (connectionString is null)
            {
                logger.LogError("Failed to retrieve connection string for target database {TargetDatabaseResourceName}.", target.Name);
                return;
            }

            var parameters = AdsParameterBuilder.CreateParameters(connectionString);

            Process.Start(new ProcessStartInfo()
            {
                FileName = isWindows ? "cmd" : "azuredatastudio",
                Arguments = isWindows
                    ? "/k azuredatastudio " + parameters
                    : parameters,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to launch Azure Data Studio, do you have it installed ?");
        }
    }
}
