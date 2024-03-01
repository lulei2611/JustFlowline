using JustFlowline.Interfaces;
using JustFlowline.Models;
using JustFlowline.Primitives;
using JustFlowline.Test.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustFlowline.Test
{
    internal class MathFlowline3 : IFlowline<Dictionary<string, object>>
    {
        public string Id => "MathFlowline3";

        public Version Version => default;

        public void Build(IFlowlineBuilder<Dictionary<string, object>> builder)
        {
            builder.TractWith((ct) => { Console.WriteLine("flowline start!"); return UnitExecutionResult.Next(); })
                .Then<UnitAdd>().FeedIn(unit => unit.Num1, new DicMaterial("AddNum1")).FeedIn(unit => unit.Num2, new DicMaterial("AddNum2"))
                .FeedOut(unit => unit.Result, new DicMaterial("AddNumRst"))
                .Then<UnitAdd>().FeedIn(unit => unit.Num1, new DicMaterial("AddNumRst")).FeedIn(unit => unit.Num2, new DicMaterial("AddNum2"))
                .FeedOut(unit => unit.Result, new DicMaterial("AddNumRst2"))
                .Then<UnitMulti>().FeedIn(unit => unit.Num1, new DicMaterial("AddNumRst")).FeedIn(unit => unit.Num2, new DicMaterial("AddNumRst2"))
                .FeedOut(unit => unit.Result, new DicMaterial("MultiRst"))
                .Then<UnitConsole>().FeedIn(unit => unit.Message, data => $"结果为：{data["MultiRst"]}");
        }
    }
}
