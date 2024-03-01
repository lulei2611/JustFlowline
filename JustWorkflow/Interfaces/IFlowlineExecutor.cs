using JustFlowline.Models;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineExecutor
    {
        Task<FlowlineExecutionResult> Execute(FlowlineInstance flowline, CancellationToken cancellationToken);
    }
}
