using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using System.Threading;

namespace JustFlowline.Models
{
    public class UnitExecutionContext : IUnitExecutionContext
    {
        public FlowlineInstance Flowline { get; set; }

        public object PersistenceData { get; set; }

        public ExecutionPointer ExecutionPointer { get; set; }

        public FlowlineUnit Unit { get; set; }

        public CancellationToken CancellationToken { get; set; }
    }
}
