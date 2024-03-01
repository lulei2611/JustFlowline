using JustFlowline.Models;
using System.Threading.Tasks;

namespace JustFlowline.Interfaces
{
    public interface IPersistenceProvider : IFlowlineRepository
    {
        Task<string> CreateNewFlowline(FlowlineInstance flowline);
    }
}
