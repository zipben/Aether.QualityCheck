using System.Collections.Generic;
using Aether.Interfaces;

namespace Aether.Models
{
    ///<summary>
    ///The response for a Right to Access request *from* a given system.
    ///</summary>
    public class EntityEnforcementResponse : IEnforcementMessage
    {
        ///<summary>
        ///The VCR ID that was sent with the initial Right to Access request.
        ///</summary>
        public string EnforcementRequestId { get; set; }

        ///<summary>
        ///This should map 1-1 to the system name provided in the original Right To Access Request.
        ///Note that this field is REQUIRED to confirm a right to access is invoked.
        ///</summary>
        public string SendingSystemName { get; set; }

        ///<summary>
        ///request a mapping of field names to 
        ///lists of potential values. remember, that there could be multiple values
        ///for a single person from one of these systems, to accomodate as many as needed.
        ///</summary>
        public Dictionary<string, List<string>> OwnedData { get; set; }
    }
}
