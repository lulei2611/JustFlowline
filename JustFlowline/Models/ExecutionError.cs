using System;

namespace JustFlowline.Models
{
    public class ExecutionError
    {
        public DateTime ErrorTime { get; set; }

        public string FlowlineId {  get; set; }

        public string PointerId {  get; set; }

        public string Message {  get; set; }
    }
}
