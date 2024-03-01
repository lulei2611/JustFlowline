using JustFlowline.Extensions;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace JustFlowline.Services
{
    public class FlowlineRegistry : IFlowlineRegistry
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<RegisterKey, FlowlineDefinition> _registers = null;

        public FlowlineRegistry(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _registers = new ConcurrentDictionary<RegisterKey, FlowlineDefinition>();
        }

        public IEnumerable<FlowlineDefinition> GetAllDefinitions()
        {
            return _registers.Values;
        }

        public FlowlineDefinition GetDefinition(string id, Version version)
        {
            var registerKey = new RegisterKey(id, version);
            if (_registers.ContainsKey(registerKey))
            {
                return _registers[registerKey];
            }
            return null;
        }

        public bool IsRegistered(string id, Version version)
        {
            var registerKey = new RegisterKey(id, version);
            return _registers.ContainsKey(registerKey);
        }

        public void RegisterFlowline(IFlowline flowline)
        {
            RegisterFlowline<object>(flowline);
        }

        public void RegisterFlowline<TData>(IFlowline<TData> flowline)
        {
            var builder = _serviceProvider.GetService<IFlowlineBuilder>().UseData<TData>();
            flowline.Build(builder);
            var definition = builder.Build(flowline.Id, flowline.Version);
            RegisterFlowline(definition);
        }

        public void RegisterFlowline(FlowlineDefinition definition)
        {
            var registerKey = new RegisterKey(definition.Id, definition.Version);
            if (_registers.ContainsKey(registerKey))
            {
                throw new InvalidOperationException($"流程线：{definition.Id} 版本号：{definition.Version} 已经注册了！");
            }
            _registers.TryAdd(registerKey, definition);
        }

        public void UnregisterFlowline(string id, Version version)
        {
            var registerKey = new RegisterKey(id, version);
            if (!_registers.ContainsKey(registerKey))
            {
                return;
            }
            _registers.TryRemove(registerKey, out var definition);
        }

        private class RegisterKey
        {
            public string Id { get; set; }

            public Version Version {  get; set; }

            public RegisterKey(string id, Version version)
            {
                this.Id = id;
                this.Version = version;
            }

            public override bool Equals(object obj)
            {
                if(obj is RegisterKey key)
                {
                    if (this.Id == key.Id)
                    {
                        if(key.Version == null && this.Version == null)
                        {
                            return true;
                        }
                        return this.Version == key.Version;
                    }
                }
                return false;
            }

            public override int GetHashCode()
            {
                return this.ToString()?.GetHashCode() ?? 0;
            }

            public override string ToString()
            {
                return $"Id:{Id} Version:{Version}";
            }
        }
    }
}
