using System.Collections.Generic;
using Aether.Enums;
using Aether.Interfaces;

namespace Aether.Models
{
    public class EntityEnforcementRequest : IEnforcementMessage
    {
        ///<summary>
        ///The unique VCR ID (Guid) associated with this rights enforcement request.
        ///</summary>
        public string EnforcementRequestId { get; set; }

        ///<summary>
        ///These are key-value pairs of identifiers, where the key is the identifier (GCID, RocketAccountId) and the value is the value for that given identifier.
        ///</summary>
        public Dictionary<string, List<string>> Identifiers { get; set; }

        /// <summary>
        /// The type of enforcement request
        /// </summary>
        public EnforcementType? EnforcementType { get; set; }

        ///<summary>
        ///This is a list of the required data points for a particular system
        ///</summary>
        public List<string> DataPointsRequired { get; set; }

        ///<summary>
        ///This is the endpoint to hit for your system to respond/complete a right to access request.
        ///</summary>
        public string ResponseEndpoint { get; set; }

        /// <summary>
        /// The name of the system that a particular message is intended to target
        /// </summary>
        public string SystemName { get; set; }
    }
}
