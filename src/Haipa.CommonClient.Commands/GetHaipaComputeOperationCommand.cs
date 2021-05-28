using System;
using System.Management.Automation;
using Haipa.ClientRuntime;
using JetBrains.Annotations;

namespace Haipa.CommonClient.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get,"HaipaOperation", DefaultParameterSetName = "get")]
    [OutputType(typeof(Operation))]
    public class GetHaipaOperationCommand : ApiCmdLet
    {
        [Parameter(
            ParameterSetName = "get",
            Position = 0,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public string[] Id { get; set; }

        protected override void ProcessRecord()
        {
            if (Id != null)
            {
                foreach (var id in Id)
                {
                    WriteObject(CommonClient.Operations.Get(id));
                }

                return;
            }

            var pageResponse = CommonClient.Operations.List();


            while (!Stopping)
            {
                WriteObject(pageResponse, true);

                if (string.IsNullOrWhiteSpace(pageResponse.NextPageLink))
                    break;

                pageResponse = CommonClient.Operations.ListNext(pageResponse.NextPageLink);

            }


        }


    }
}