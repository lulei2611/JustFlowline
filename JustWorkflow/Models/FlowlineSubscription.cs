using System;

namespace JustFlowline.Models
{
    public class FlowlineSubscription
    {
        public string Id { get; set; }

        public string FlowlineId { get; set; }

        public int UnitId { get; set; }

        public string ExecutionPointerId { get; set; }

        public string EventName { get; set; }

        public string EventKey { get; set; }

        public DateTime SubscribeAsOf { get; set; }

        public object SubscriptionData { get; set; }

        public string ExternalToken { get; set; }

        public string ExternalWorkerId { get; set; }

        public DateTime? ExternalTokenExpiry { get; set; }
    }
}
