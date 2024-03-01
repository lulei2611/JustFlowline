using JustFlowline.Interfaces;
using System.Linq.Expressions;

namespace JustFlowline.Models
{
    public class ValueOutcome : IUnitOutcome
    {
        public LambdaExpression Expression { get; set; } = null;

        public int NextUnitId { get; set; }

        public bool Match(UnitExecutionResult executionResult, object data)
        {
            return object.Equals(GetValue(data), executionResult.Outcome) || GetValue(data) == null;
        }

        private object GetValue(object data)
        {
            if(this.Expression == null)
            {
                return null;
            }
            return this.Expression.Compile().DynamicInvoke(data);
        }
    }
}
