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
    internal class MathFlowline2 : IFlowline<Data>
    {
        public string Id => "MathFlowline2";

        public Version Version => default;

        public void Build(IFlowlineBuilder<Data> builder)
        {
            builder.TractWith((ct) => UnitExecutionResult.Next())
                .Then<UnitAdd>().FeedIn(unit => unit.Num1, data => data.AddParam.Num1).FeedIn(unit => unit.Num2, data => data.AddParam.Num2)
                .FeedOut(data => data.AddParam.Result, unit => unit.Result)
                .Then<UnitMulti>().FeedIn(unit => unit.Num1, data => data.AddParam.Num1).FeedIn(unit => unit.Num2, data => data.AddParam.Result)
                .FeedOut(data => data.MultiParam.Result, unit => unit.Result)
                .Then<UnitConsole>().FeedIn(unit => unit.Message, data => $"结果为：{data.MultiParam.Result}");
        }
    }

    public class Data
    {
        public AddParam AddParam { get; set; }

        public MultiParam MultiParam { get; set; }
    }

    public class AddParam
    {
        public int Num1 {  get; set; }

        public int Num2 { get; set; }

        public int Result {  get; set; }
    }

    public class MultiParam
    {
        public int Num1 { get; set; }

        public int Num2 { get; set; }

        public int Result { get; set; }
    }
}
