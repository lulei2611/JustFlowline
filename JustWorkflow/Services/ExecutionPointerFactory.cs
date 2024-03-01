using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;

namespace JustFlowline.Services
{
    public class ExecutionPointerFactory : IExecutionPointerFactory
    {
        public ExecutionPointer CreateGenesisPointer(FlowlineDefinition definition)
        {
            return new ExecutionPointer()
            {
                Id = GenerateId(),
                UnitId = 0,
                Active = true,
                Status = ExecutionPointerStatus.Pending,
            };
        }

        public ExecutionPointer CreateNextPointer(FlowlineDefinition definition, ExecutionPointer pointer, IUnitOutcome unitOutcome)
        {
            return new ExecutionPointer()
            {
                Id = GenerateId(),
                UnitId = unitOutcome.NextUnitId,
                PredecessorId = pointer.UnitId,
                Active = true,
                Status = ExecutionPointerStatus.Pending,
            };
        }

        private string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
