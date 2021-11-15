using System.Threading.Tasks;

namespace Aether.QualityChecks.Interfaces
{
    public interface IFileDrivenQualityCheck<T> : IFileDrivenQualityCheck { }

    public interface IFileDrivenQualityCheck 
    {
        public Task LoadFile(byte[] fileContents);
    }
}