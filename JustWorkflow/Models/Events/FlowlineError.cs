using System;

namespace JustFlowline.Models.Events
{
    public class FlowlineError : FlowlineCycleEvent
    {
        public string ExecutionPointerId {  get; set; }

        public int UnitId {  get; set; }

        public Exception Exception { get; set; }
    }
}
