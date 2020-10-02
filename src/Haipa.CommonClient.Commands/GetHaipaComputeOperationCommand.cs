﻿using System;
using System.Management.Automation;
using Haipa.ClientRuntime;
using Haipa.ClientRuntime.OData;
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
        public Guid[] Id { get; set; }

        protected override void ProcessRecord()
        {
            if (Id != null)
            {
                foreach (var id in Id)
                {
                    WriteObject(CommonClient.Operations.Get(id, null, "LogEntries,Resources,Tasks"));
                }

                return;
            }

            var query = new ODataQuery<Operation> { Expand = "LogEntries,Resources,Tasks" };

            WriteObject(CommonClient.Operations.List(query).Value, true);


        }


    }
}