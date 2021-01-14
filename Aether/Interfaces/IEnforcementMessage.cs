using Aether.Enums;

namespace Aether.Interfaces
{
    public interface IEnforcementMessage
    {
        public string EnforcementRequestId { get; set; }
        public EnforcementType EnforcementType { get; set; }
    }
}
