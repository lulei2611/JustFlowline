using System.Linq.Expressions;

namespace JustFlowline.Interfaces
{
    public interface IUnitMaterial
    {
        void FeedIn(object data, IUnitWork unitWork);

        void FeedOut(object data, IUnitWork unitWork);
    }

    public interface IUnitMaterial<TData> : IUnitMaterial
    {
        string DataKey { get; }

        LambdaExpression PropertyExp { get; set; }
    }
}
