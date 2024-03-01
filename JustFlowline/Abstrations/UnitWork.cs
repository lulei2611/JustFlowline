using JustFlowline.Interfaces;
using JustFlowline.Models;
using System.Threading.Tasks;

namespace JustFlowline.Abstrations
{
    public abstract class UnitWork : IUnitWork
    {
        public abstract UnitExecutionResult Make(IUnitExecutionContext context);

        public Task<UnitExecutionResult> MakeAsync(IUnitExecutionContext context)
        {
            return Task.FromResult(Make(context));
        }
    }
}
