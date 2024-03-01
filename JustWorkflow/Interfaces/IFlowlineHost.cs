using JustFlowline.Abstrations;
using JustFlowline.Models;
using System;

namespace JustFlowline.Interfaces
{
    public interface IFlowlineHost : IFlowlineController
    {
        void Start();

        void ReportUnitError(FlowlineInstance flowline, FlowlineUnit unit, Exception exception);
    }

    public delegate void OnUnitErrorHandler(FlowlineInstance flowline, FlowlineUnit unit, Exception exception);
}
