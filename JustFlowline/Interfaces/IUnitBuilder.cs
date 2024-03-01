using System;
using System.Linq.Expressions;

namespace JustFlowline.Interfaces
{
    public interface IUnitBuilder<TData, TUnitWork> : IFlowlineModifier<TData, TUnitWork> where TUnitWork : IUnitWork
    {
        IUnitBuilder<TData, TUnitWork> FeedIn<TIn>(Expression<Func<TUnitWork, TIn>> unitProperty, Expression<Func<TData, TIn>> value);

        IUnitBuilder<TData, TUnitWork> FeedOut<TOut>(Expression<Func<TData, TOut>> dataProperty, Expression<Func<TUnitWork, TOut>> value);


        //扩展
        IUnitBuilder<TData, TUnitWork> FeedIn<TIn, TUnitMaterial>(Expression<Func<TUnitWork, TIn>> unitProperty, TUnitMaterial unitMaterial) where TUnitMaterial : IUnitMaterial<TData>;

        IUnitBuilder<TData, TUnitWork> FeedOut<TOut, TUnitMaterial>(Expression<Func<TUnitWork, TOut>> value, TUnitMaterial unitMaterial) where TUnitMaterial : IUnitMaterial<TData>;
    }
}
