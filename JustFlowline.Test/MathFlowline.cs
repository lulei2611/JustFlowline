using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Test.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustFlowline.Test
{
    internal class MathFlowline : IFlowline<DicEx>
    {
        public string Id => "MathFlowline";

        public Version Version => default;

        public void Build(IFlowlineBuilder<DicEx> builder)
        {
            builder.TractWith((ct) => UnitExecutionResult.Next())
                .Then<UnitAdd>().FeedIn(unit => unit.Num1, data => data["AddNum1"]).FeedIn(unit => unit.Num2, data => data["AddNum2"])
                .FeedOut(data => data["AddResult"], unit => unit.Result)
                .Then<UnitMulti>().FeedIn(unit => unit.Num1, data => data["AddResult"]).FeedIn(unit => unit.Num2, data => data["AddNum1"])
                .FeedOut(data => data["MultiResult"], unit => unit.Result)
                .Then<UnitConsole>().FeedIn(unit => unit.Message, data => $"结果为：{data["MultiResult"]}");
        }
    }

    public class DicEx : Dictionary<string,int>
    {
    }
}
