using JustFlowline.Abstrations;
using JustFlowline.Models;
using System.Threading;

namespace JustFlowline.Interfaces
{
    public interface IUnitExecutionContext
    {
        FlowlineInstance Flowline { get; set; }

        object PersistenceData {  get; set; }

        ExecutionPointer ExecutionPointer { get; set; }

        FlowlineUnit Unit { get; set; }

        CancellationToken CancellationToken { get; set; }
    }
}
