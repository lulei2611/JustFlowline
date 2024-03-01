using System;

namespace JustFlowline.Interfaces
{
    public interface IFlowline<TData>
    {
        string Id { get;}

        Version Version {  get; }

        void Build(IFlowlineBuilder<TData> builder);
    }

    public interface IFlowline : IFlowline<object>
    {

    }
}
