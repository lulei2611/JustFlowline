using System;

namespace JustFlowline.Models
{
    public class FlowlineCycleEvent
    {
        public DateTime EventTime { get; set; }

        public string FlowlineId {  get; set; }

        public Version FlowlineVersion { get; set; }

        public string FlowlineInstanceId {  get; set; }

        public string Reference {  get; set; }
    }
}
