using JustFlowline.Models;

namespace JustFlowline.Interfaces
{
    public interface IUnitOutcome
    {
        int NextUnitId { get; set; }

        bool Match(UnitExecutionResult executionResult, object data);
    }
}
