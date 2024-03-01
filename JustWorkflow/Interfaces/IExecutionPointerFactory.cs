using JustFlowline.Models;

namespace JustFlowline.Interfaces
{
    public interface IExecutionPointerFactory
    {
        ExecutionPointer CreateGenesisPointer(FlowlineDefinition definition);

        ExecutionPointer CreateNextPointer(FlowlineDefinition definition, ExecutionPointer pointer, IUnitOutcome unitOutcome);
    }
}
