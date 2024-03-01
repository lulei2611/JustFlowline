using JustFlowline.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace JustFlowline.Primitives
{
    public class MemberMapMaterial : IUnitMaterial
    {
        private readonly LambdaExpression _source;
        private readonly LambdaExpression _target;

        public MemberMapMaterial(LambdaExpression source,LambdaExpression target)
        {
            if (target.Body.NodeType != ExpressionType.MemberAccess)
                throw new NotSupportedException();
            _source = source;
            _target = target;
        }

        public void FeedIn(object data, IUnitWork unitWork)
        {
            Feed(data, _source, unitWork, _target);
        }

        public void FeedOut(object data, IUnitWork unitWork)
        {
            Feed(unitWork, _source, data, _target);
        }

        private void Feed(object sourceObject, LambdaExpression sourceExpr, object targetObject, LambdaExpression targetExpr)
        {
            object resolvedValue = sourceExpr.Compile().DynamicInvoke(sourceObject);

            if (resolvedValue == null)
            {
                var defaultAssign = Expression.Lambda(Expression.Assign(targetExpr.Body, Expression.Default(targetExpr.ReturnType)), targetExpr.Parameters.Single());
                defaultAssign.Compile().DynamicInvoke(targetObject);
                return;
            }

            var valueExpr = Expression.Convert(Expression.Constant(resolvedValue), targetExpr.ReturnType);
            var assign = Expression.Lambda(Expression.Assign(targetExpr.Body, valueExpr), targetExpr.Parameters.Single());
            assign.Compile().DynamicInvoke(targetObject);
        }
    }
}
