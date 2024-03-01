using System.Collections.Generic;

namespace JustFlowline.Models
{
    public class FlowlineExecutionResult
    {
        public List<ExecutionError> Errors { get; set; } = new List<ExecutionError>();

        public List<FlowlineSubscription> Subscriptions { get; set; } = new List<FlowlineSubscription>();
    }
}
