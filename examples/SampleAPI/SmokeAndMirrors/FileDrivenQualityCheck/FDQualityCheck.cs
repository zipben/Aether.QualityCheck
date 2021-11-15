using Aether.QualityChecks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeAndMirrors.FileDrivenQualityCheck
{
    public class FDQualityCheck : IFileDrivenQualityCheck
    {
        public async Task LoadFile(byte[] fileContents)
        {
            string str = Encoding.Default.GetString(fileContents);

            Console.Write(str);
        }
    }
}
