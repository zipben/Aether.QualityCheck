namespace Aether.Interfaces.Themis
{
    public interface ISystemRestriction : IRestriction
    {
        public string SystemId { get; set; }
        public string SystemName { get; set; }
    }
}