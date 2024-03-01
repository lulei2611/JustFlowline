using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;

namespace JustFlowline.Primitives
{
    public class FlowlineUnitFree : FlowlineUnit<FreeUnitWork>
    {
        public Func<IUnitExecutionContext,UnitExecutionResult> Work {  get; set; }

        public override IUnitWork ConstructUnitWork(IServiceProvider serviceProvider)
        {
            return new FreeUnitWork(Work);
        }
    }
}
