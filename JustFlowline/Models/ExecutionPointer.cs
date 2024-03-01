using System;

namespace JustFlowline.Models
{
    public class ExecutionPointer
    {
        public string Id {  get; set; }

        public int PredecessorId {  get; set; }

        public int UnitId {  get; set; }

        public string UnitName {  get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool Active {  get; set; }

        public ExecutionPointerStatus Status { get; set; }
    }

    public enum ExecutionPointerStatus
    {
        Pending,
        Running,
        Failed,
        Canceled,
        Complete,
        Sleep,
        WaitForEvent,
    }
}
