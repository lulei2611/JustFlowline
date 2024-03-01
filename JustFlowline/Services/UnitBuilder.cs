using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Primitives;
using System;
using System.Linq.Expressions;

namespace JustFlowline.Services
{
    public class UnitBuilder<TData, TUnitWork> : IUnitBuilder<TData, TUnitWork> where TUnitWork : IUnitWork
    {
        protected IFlowlineBuilder<TData> FlowlineBuilder { get; private set; }

        protected FlowlineUnit<TUnitWork> Unit { get; private set; }

        public UnitBuilder(IFlowlineBuilder<TData> flowlineBuilder, FlowlineUnit<TUnitWork> unit)
        {
            this.FlowlineBuilder = flowlineBuilder;
            this.Unit = unit;
        }

        public IUnitBuilder<TData, TUnitWork> FeedIn<TIn>(Expression<Func<TUnitWork, TIn>> unitProperty, Expression<Func<TData, TIn>> value)
        {
            this.Unit.Inputs.Add(new MemberMapMaterial(value, unitProperty));
            return this;
        }

        public IUnitBuilder<TData, TUnitWork> FeedOut<TOut>(Expression<Func<TData, TOut>> dataProperty, Expression<Func<TUnitWork, TOut>> value)
        {
            this.Unit.Outputs.Add(new MemberMapMaterial(value, dataProperty));
            return this;
        }

        public IUnitBuilder<TData, TUnit> Then<TUnit>() where TUnit : IUnitWork
        {
            FlowlineUnit<TUnit> nextUnit = new FlowlineUnit<TUnit>();
            this.FlowlineBuilder.AddUnit(nextUnit);
            var unitBuilder = new UnitBuilder<TData, TUnit>(this.FlowlineBuilder, nextUnit);
            this.Unit.Outcomes.Add(new ValueOutcome() { NextUnitId = nextUnit.Id, });
            return unitBuilder;
        }

        public IUnitBuilder<TData, TUnitWork> FeedIn<TIn, TUnitMaterial>(Expression<Func<TUnitWork, TIn>> unitProperty, TUnitMaterial unitMaterial) where TUnitMaterial : IUnitMaterial<TData>
        {
            unitMaterial.PropertyExp = unitProperty;
            this.Unit.Inputs.Add(unitMaterial);
            return this;
        }

        public IUnitBuilder<TData, TUnitWork> FeedOut<TOut, TUnitMaterial>(Expression<Func<TUnitWork, TOut>> value, TUnitMaterial unitMaterial) where TUnitMaterial : IUnitMaterial<TData>
        {
            unitMaterial.PropertyExp = value;
            this.Unit.Outputs.Add(unitMaterial);
            return this;
        }
    }
}
