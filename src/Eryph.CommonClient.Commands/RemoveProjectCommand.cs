using System;
using System.Management.Automation;
using Eryph.ClientRuntime;
using Eryph.CommonClient.Models;
using JetBrains.Annotations;

namespace Eryph.CommonClient.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.Remove, "EryphProject")]
    [OutputType(typeof(Operation))]
    public class RemoveProjectCommand : ApiCmdLet
    {
        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public string[] Id { get; set; }

        [Parameter]
        public SwitchParameter Wait
        {
            get => _wait;
            set => _wait = value;
        }
        
        private bool _wait;


        protected override void ProcessRecord()
        {
            foreach (var id in Id)
            {
                WaitForProject(CommonClient.Projects.Delete(id)
                    , _wait, false, id);
            }

        }

    }
}