using System.Threading.Tasks;

namespace Aether.Interfaces
{
    public interface IQualityCheck
    {
        string LogName { get; }
        Task<bool> Run();
    }
}
