using JustFlowline.Interfaces;
using System;
using System.Collections.Generic;

namespace JustFlowline.Abstrations
{
    public abstract class FlowlineUnit
    {
        public abstract Type WorkType { get; }

        public virtual int Id {  get; set; }

        public virtual string Name { get; set; }

        public List<IUnitMaterial> Inputs { get; set; } = new List<IUnitMaterial>();

        public List<IUnitMaterial> Outputs { get; set; } = new List<IUnitMaterial>();

        public List<IUnitOutcome> Outcomes { get; set; } = new List<IUnitOutcome>();

        public virtual ExecutionPipelineCommandResult BeforeExecute(IUnitExecutionContext context)
        {
            return ExecutionPipelineCommandResult.Next;
        }

        public virtual void AfterExecute(IUnitExecutionContext context) { }

        public virtual IUnitWork ConstructUnitWork(IServiceProvider serviceProvider)
        {
            IUnitWork unitWork = null;
            try
            {
                unitWork = serviceProvider.GetService(WorkType) as IUnitWork;
            }
            catch
            {
                if (unitWork == null)
                {
                    var stepCtor = WorkType.GetConstructor(new Type[] { });
                    if (stepCtor != null)
                        unitWork = stepCtor.Invoke(null) as IUnitWork;
                }
            }
            return unitWork;
        }
    }

    public class FlowlineUnit<TUnitWork> : FlowlineUnit where TUnitWork : IUnitWork
    {
        public override Type WorkType => typeof(TUnitWork);
    }

    public enum ExecutionPipelineCommandResult
    {
        Next,
        Defer,
        End,
    }
}
