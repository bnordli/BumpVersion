using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BumpVersion
{
    public static class VersionReader
    {
        public const string VersionFile = "version.txt";

        public static IEnumerable<VersionInfo> PreviousVersions()
        {
            try
            {
                return File
                    .ReadAllLines(VersionFile)
                    .Select(VersionInfo.Parse);
            }
            catch (FileNotFoundException e)
            {
                throw new MessageException("Could not find file 'version.txt'. It must exist in the current folder.", e);
            }
        }
    }
}