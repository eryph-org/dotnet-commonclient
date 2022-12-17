using System;
using System.Management.Automation;
using System.Text;
using Eryph.ClientRuntime;
using Eryph.CommonClient.Models;
using JetBrains.Annotations;

namespace Eryph.CommonClient.Commands
{
    [PublicAPI]
    [Cmdlet(VerbsCommon.New, "EryphProject")]
    [OutputType(typeof(Operation), typeof(Project))]
    public class NewProjectCommand : ApiCmdLet
    {
        [Parameter(
            Position = 0,
            ValueFromPipeline = true,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        [Parameter]
        public SwitchParameter Wait
        {
            get => _wait;
            set => _wait = value;
        }

        private bool _wait;


        protected override void ProcessRecord()
        {
            WaitForProject(CommonClient.Projects.Create(new NewProjectRequest
            {
                CorrelationId = Guid.NewGuid(),
                Name = Name
            }), _wait, true);


        }


    }



}