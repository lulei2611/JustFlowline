using JustFlowline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JustFlowline.Test
{
    internal class DicMaterial : IUnitMaterial<Dictionary<string, object>>
    {
        public LambdaExpression PropertyExp {  get; set; }

        public string DataKey { get; private set; }

        public DicMaterial(string dataKey)
        {
            DataKey = dataKey;
        }

        public void FeedIn(object data, IUnitWork unitWork)
        {
            var value = (data as Dictionary<string, object>)[DataKey];
            var valueExpr = Expression.Convert(Expression.Constant(value), PropertyExp.ReturnType);
            var assign = Expression.Lambda(Expression.Assign(PropertyExp.Body, valueExpr), PropertyExp.Parameters.Single());
            assign.Compile().DynamicInvoke(unitWork);
        }

        public void FeedOut(object data, IUnitWork unitWork)
        {
            object resolvedValue = PropertyExp.Compile().DynamicInvoke(unitWork);
            (data as Dictionary<string, object>)[DataKey] = resolvedValue;
        }
    }
}
