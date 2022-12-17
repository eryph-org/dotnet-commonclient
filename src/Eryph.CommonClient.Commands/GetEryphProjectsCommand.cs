using System;
using System.Management.Automation;
using Eryph.ClientRuntime;
using JetBrains.Annotations;

namespace Eryph.CommonClient.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Get, "EryphProject", DefaultParameterSetName = "get")]
    [OutputType(typeof(Project))]
    public class GetEryphProjectCommand : ApiCmdLet
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
                    WriteObject(CommonClient.Projects.Get(id));
                }

                return;
            }

            var pageResponse = CommonClient.Projects.List();


            while (!Stopping)
            {
                WriteObject(pageResponse, true);

                if (string.IsNullOrWhiteSpace(pageResponse.NextPageLink))
                    break;

                pageResponse = CommonClient.Projects.ListNext(pageResponse.NextPageLink);

            }


        }


    }
}