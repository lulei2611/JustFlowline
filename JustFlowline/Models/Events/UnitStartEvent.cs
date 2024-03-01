namespace JustFlowline.Models.Events
{
    public class UnitStartEvent : FlowlineCycleEvent
    {
        public string ExecutionPointerId { get; set; }

        public int UnitId { get; set; }
    }
}
