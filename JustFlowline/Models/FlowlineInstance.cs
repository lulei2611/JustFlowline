using System;

namespace JustFlowline.Models
{
    public class FlowlineInstance
    {
        public string Id { get; set; }

        public string FlowlineId { get; set; }

        public Version Version { get; set; }

        public string Description {  get; set; }

        public string Reference {  get; set; }

        public object Data {  get; set; }

        public FlowlineStatus Status { get; set; }

        public long? NextExecution { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? CompleteTime { get; set; }

        public ExecutionPointerCollection ExecutionPointers { get; set; } = new ExecutionPointerCollection();
    }

    public enum FlowlineStatus
    {
        Runnable,
        Running,
        Complete,
    }
}
