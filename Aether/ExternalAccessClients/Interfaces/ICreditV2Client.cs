using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aether.ExternalAccessClients.Interfaces
{
    public interface ICreditV2Client
    {
        Task<Credit.Mismo.RESPONSE_GROUP> PullCredit(string creditGuid);
    }
}
