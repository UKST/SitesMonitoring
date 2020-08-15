namespace SitesMonitoring.BLL.Data
{
    internal sealed class EmptyFilterSpecification<T> : Specification<T>
    {
        public EmptyFilterSpecification() : base(i => true)
        {
        }
    }
}