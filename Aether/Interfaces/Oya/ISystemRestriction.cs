namespace Aether.Interfaces.Oya
{
    public interface ISystemRestriction : IRestriction
    {
        public string SystemId { get; set; }
        public string SystemName { get; set; }
    }
}