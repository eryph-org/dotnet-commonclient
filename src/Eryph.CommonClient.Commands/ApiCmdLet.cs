﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using Eryph.ClientRuntime;
using Eryph.ClientRuntime.Configuration;
using Eryph.ClientRuntime.Powershell;
using JetBrains.Annotations;
using Microsoft.Rest;

namespace Eryph.CommonClient.Commands
{
    [PublicAPI]
    public abstract class ApiCmdLet : EryphCmdLet, IDisposable
    {

        protected EryphCommonClient CommonClient;

        protected override void BeginProcessing()
        {
            CommonClient = new EryphCommonClient(
                GetEndpointUri("common"), GetCredentials("common_api"));

        }


        protected Uri GetEndpointUri(string endpoint)
        {

            var credentials = GetClientCredentials();
            var endpointLookup = new EndpointLookup(new PowershellEnvironment(SessionState));
            return endpointLookup.GetEndpoint(endpoint, credentials.Configuration);

        }

        protected ServiceClientCredentials GetCredentials(params string[] scopes)
        {
            return ServiceClientCredentialsCache.Instance.GetServiceCredentials(GetClientCredentials(), scopes).GetAwaiter().GetResult();
        }

        protected override void EndProcessing()
        {
            Dispose();
        }

        protected void ResourceWriter(Operation operation, Action<string, string> resourceWriterDelegate)
        {
            var resourceData =
                CommonClient.Operations.Get(operation.Id, expand: "resources");
            foreach (var resource in resourceData.Resources.Where(x => !string.IsNullOrWhiteSpace(x.ResourceId)))
            {
                resourceWriterDelegate(resource.ResourceType, resource.ResourceId);
            }

            if (resourceData.Resources.Count > 0)
                return;

        }


        protected void WaitForOperation(Operation operation, Action<Operation> writerDelegate = null)
        {
            var timeStamp = DateTime.Parse("2018-01-01", CultureInfo.InvariantCulture);


            var processedLogIds = new List<string>();
            while (!Stopping)
            {
                Task.Delay(1000).GetAwaiter().GetResult();

                var currentOperation = CommonClient.Operations.Get(operation.Id, timeStamp, expand: "logs");

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
                            "EryphOperationFailed", ErrorCategory.InvalidResult, operation.Id));
                        break;
                }

                break;
            }


            if (writerDelegate != null)
            {
                writerDelegate(operation);
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


        protected void WaitForProject(Operation operation, bool wait, bool preferWriteProject, string knownProjectId = default)
        {
            if (!wait)
            {
                if (knownProjectId == default || !preferWriteProject)
                    WriteObject(operation);
                else
                    WriteObject(this.CommonClient.Projects.Get(knownProjectId));
                return;
            }

            WaitForOperation(operation, preferWriteProject ? (Action<Operation>) WriteProject : null);
        }


        private void WriteProject(Operation operation)
        {
            var newOp = CommonClient.Operations.Get(operation.Id, expand: "projects");
            var projectData = newOp.Projects.FirstOrDefault();
            if (projectData != null)
            {
                WriteObject(CommonClient.Projects.Get(projectData.Id));
            }

        }
    }
}