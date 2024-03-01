using JustFlowline.Abstrations;
using JustFlowline.Interfaces;
using JustFlowline.Models;
using System;

namespace JustFlowline.Test.Units
{
    internal class UnitConsole : UnitWork
    {
        public string Message {  get; set; }

        public override UnitExecutionResult Make(IUnitExecutionContext context)
        {
            Console.WriteLine(Message);
            return UnitExecutionResult.Next();
        }
    }
}
