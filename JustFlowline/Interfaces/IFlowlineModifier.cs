namespace JustFlowline.Interfaces
{
    public interface IFlowlineModifier<TData, TUnitWork> where TUnitWork : IUnitWork
    {
        IUnitBuilder<TData, TUnit> Then<TUnit>() where TUnit : IUnitWork;
    }
}
