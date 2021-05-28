using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using Haipa.ClientRuntime;
using Haipa.IdentityModel.Clients;
using JetBrains.Annotations;
using Microsoft.Rest;

namespace Haipa.CommonClient.Commands
{
    [PublicAPI]
    public abstract class ApiCmdLet : PSCmdlet, IDisposable
    {
        [Parameter]
        public ClientCredentials Credentials { get; set; }

        protected HaipaCommonClient CommonClient;

        protected override void BeginProcessing()
        {
            CommonClient = new HaipaCommonClient(GetCredentials("common_api"));

        }


        protected ClientCredentials GetClientCredentials()
        {
            var clientCredentials = Credentials;
            if (clientCredentials != null) return clientCredentials;


            var obj = SessionState.InvokeCommand.InvokeScript("Get-HaipaClientCredentials").FirstOrDefault();
            if (obj?.BaseObject is ClientCredentials credentials)
                clientCredentials = credentials;

            if (clientCredentials == null)
            {
                throw new InvalidOperationException(@"Could not find credentials for Haipa.
You can use the parameter Credentials to set the haipa credentials. If not set, the credentials from CmdLet Get-HaipaClientCredentials will be used.
In this case the credentials will be searched in your local configuration. 
If there is no default haipa client in your configuration the command will try to access the default system-client of a local running haipa zero or identity server.
To access the system-client you will have to run this command as Administrator (Windows) or root (Linux).
");
            }

            return clientCredentials;
        }

        protected ServiceClientCredentials GetCredentials(params string[] scopes)
        {
            return ServiceClientCredentialsCache.Instance.GetServiceCredentials(GetClientCredentials(), scopes).GetAwaiter().GetResult();
        }

        protected override void EndProcessing()
        {
            Dispose();
        }

        protected void WaitForOperation(Operation operation, Action<string,string> resourceWriterDelegate = null)
        {
            var timeStamp = DateTime.Parse("2018-01-01", CultureInfo.InvariantCulture);
            var processedLogIds = new List<string>();
            while (!Stopping)
            {
                Task.Delay(1000).GetAwaiter().GetResult();

                var currentOperation = CommonClient.Operations.Get(operation.Id, timeStamp);

                foreach (var logEntry in currentOperation.LogEntries)
                {
                    if(processedLogIds.Contains(logEntry.Id))
                        continue;

                    processedLogIds.Add(logEntry.Id);
                    
                    WriteVerbose($"Operation {currentOperation.Id}: {logEntry.Message}");
                    timeStamp = logEntry.Timestamp.GetValueOrDefault();
                    Task.Delay(100).GetAwaiter().GetResult();
                }

                switch (currentOperation.Status)
                {
                    case "Queued":
                    case "Running":
                        continue;
                    case "Failed":
                        WriteError(new ErrorRecord(new ApiServiceException(currentOperation.StatusMessage),
                            "HaipaOperationFailed", ErrorCategory.InvalidResult, operation.Id));
                        break;
                }

                break;
            }

            if (resourceWriterDelegate != null)
            {
                var resourceData =
                    CommonClient.Operations.Get(operation.Id);
                foreach (var resource in resourceData.Resources.Where(x=>!string.IsNullOrWhiteSpace(x.ResourceId)))
                {
                    resourceWriterDelegate(resource.ResourceType, resource.ResourceId);
                }

                if (resourceData.Resources.Count > 0)
                    return;
            }

            WriteObject(CommonClient.Operations.Get(operation.Id));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CommonClient?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}