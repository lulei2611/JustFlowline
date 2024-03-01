using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;

namespace JustFlowline.Test.Units
{
    internal class UnitAdd : UnitWork
    {
        public int Num1 {  get; set; }

        public int Num2 { get; set; }

        public int Result {  get; set; }

        public override UnitExecutionResult Make(IUnitExecutionContext context)
        {
            Result = Num1 + Num2;
            return UnitExecutionResult.Next();
        }
    }
}
