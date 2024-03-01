using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;

namespace JustFlowline.Primitives
{
    public class FreeUnitWork : UnitWork
    {
        private readonly Func<IUnitExecutionContext, UnitExecutionResult> _work = null;

        public FreeUnitWork(Func<IUnitExecutionContext, UnitExecutionResult> work)
        {
            _work = work;
        }

        public override UnitExecutionResult Make(IUnitExecutionContext context)
        {
            return _work(context);
        }
    }
}
