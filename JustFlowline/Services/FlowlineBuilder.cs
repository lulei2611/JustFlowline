using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Primitives;
using System;
using System.Collections.Generic;

namespace JustFlowline.Services
{
    public class FlowlineBuilder : IFlowlineBuilder
    {
        public List<FlowlineUnit> Units { get; private set; } = new List<FlowlineUnit>();

        public int LastUnit => this.Units.Count - 1;

        public void AddUnit(FlowlineUnit unit)
        {
            unit.Id = Units.Count;
            Units.Add(unit);
        }

        public IFlowlineBuilder<TData> UseData<TData>()
        {
            FlowlineBuilder<TData> builder = new FlowlineBuilder<TData>(this.Units);
            return builder;
        }

        public virtual FlowlineDefinition Build(string id, Version version)
        {
            return new FlowlineDefinition()
            {
                Id = id,
                Version = version,
                Units = new FlowlineUnitCollection(this.Units),
            };
        }
    }

    public class FlowlineBuilder<TData> : FlowlineBuilder, IFlowlineBuilder<TData>
    {
        public FlowlineBuilder() { }

        public FlowlineBuilder(IEnumerable<FlowlineUnit> units)
        {
            this.Units.AddRange(units);
        }

        public IUnitBuilder<TData, FreeUnitWork> TractWith(Func<IUnitExecutionContext, UnitExecutionResult> work)
        {
            FlowlineUnitFree unit = new FlowlineUnitFree();
            unit.Work = work;
            UnitBuilder<TData, FreeUnitWork> unitBuilder = new UnitBuilder<TData, FreeUnitWork>(this, unit);
            AddUnit(unit);
            return unitBuilder;
        }

        public IUnitBuilder<TData, TUnitWork> TractWith<TUnitWork>(Action<IUnitBuilder<TData, TUnitWork>> action = null) where TUnitWork : IUnitWork
        {
            FlowlineUnit<TUnitWork> unit = new FlowlineUnit<TUnitWork>();
            UnitBuilder<TData, TUnitWork> unitBuilder = new UnitBuilder<TData, TUnitWork>(this, unit);
            AddUnit(unit);
            return unitBuilder;
        }

        public override FlowlineDefinition Build(string id, Version version)
        {
            var definition = base.Build(id, version);
            definition.DataType = typeof(TData);
            return definition;
        }
    }
}
