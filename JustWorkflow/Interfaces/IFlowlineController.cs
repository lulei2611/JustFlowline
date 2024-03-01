using System;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineController
    {
        void RegisterFlowline<TFlowline>() where TFlowline : IFlowline;

        void RegisterFlowline<TFlowline, TData>() where TFlowline : IFlowline<TData> where TData : new();

        Task<string> StartFlowline(string id, object data, string reference = null);

        Task<string> StartFlowline(string id, Version version, object data, string reference = null);
    }
}
