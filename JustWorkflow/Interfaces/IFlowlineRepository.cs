using JustFlowline.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineRepository
    {
        Task PersistFlowline(FlowlineInstance flowline, CancellationToken cancellationToken = default);

        Task PersistFlowline(FlowlineInstance flowline, List<FlowlineSubscription> subscriptions, CancellationToken cancellationToken = default);

        Task<FlowlineInstance> GetFlowline(string flowlineId, CancellationToken cancellationToken = default);
    }
}
