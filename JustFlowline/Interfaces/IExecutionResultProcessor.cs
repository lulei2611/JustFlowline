using JustFlowline.Abstrations;
using JustFlowline.Models;
using System;

namespace JustFlowline.Interfaces
{
    public interface IExecutionResultProcessor
    {
        void ProcessExecutionResult(FlowlineInstance flowline, FlowlineDefinition definition, ExecutionPointer executionPointer, FlowlineUnit unit, UnitExecutionResult executionResult, FlowlineExecutionResult flExecutionResult);

        void HandleException(FlowlineInstance flowline, FlowlineDefinition definition, ExecutionPointer executionPointer, FlowlineUnit unit, Exception exception);
    }
}
