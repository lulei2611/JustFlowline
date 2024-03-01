using JustFlowline.Models;
using System;
using System.Collections.Generic;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineRegistry
    {
        void RegisterFlowline(IFlowline flowline);

        void RegisterFlowline<TData>(IFlowline<TData> flowline);

        void RegisterFlowline(FlowlineDefinition definition);

        void UnregisterFlowline(string id, Version version);

        bool IsRegistered(string id, Version version);

        FlowlineDefinition GetDefinition(string id, Version version);

        IEnumerable<FlowlineDefinition> GetAllDefinitions();
    }
}
