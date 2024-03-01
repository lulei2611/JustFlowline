using JustFlowline.Models;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IUnitWork
    {
        Task<UnitExecutionResult> MakeAsync(IUnitExecutionContext context);
    }
}
