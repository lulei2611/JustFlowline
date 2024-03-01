using JustFlowline.Abstrations;
using JustFlowline.Models;
using JustFlowline.Primitives;
using System;
using System.Collections.Generic;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineBuilder
    {
        List<FlowlineUnit> Units { get; }

        int LastUnit {  get; }

        void AddUnit(FlowlineUnit unit);

        IFlowlineBuilder<TData> UseData<TData>(); 

        FlowlineDefinition Build(string id, Version version);
    }

    public interface IFlowlineBuilder<TData> : IFlowlineBuilder
    {
        IUnitBuilder<TData, FreeUnitWork> TractWith(Func<IUnitExecutionContext, UnitExecutionResult> work);

        IUnitBuilder<TData, TUnitWork> TractWith<TUnitWork>(Action<IUnitBuilder<TData, TUnitWork>> action = null) where TUnitWork : IUnitWork;
    }
}
