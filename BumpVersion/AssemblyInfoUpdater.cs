using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BumpVersion
{
    public static class AssemblyInfoUpdater
    {
        public const string AssemblyInfoFile = "*AssemblyInfo.cs";
        public const string AssemblyVersionSearch = "[assembly: AssemblyVersion";
        public const string AssemblyVersionWrite = "[assembly: AssemblyVersion(\"{0}\")]";
        public const string AssemblyInformationalVersionSearch = "[assembly: AssemblyInformationalVersion";
        public const string AssemblyInformationalVersionWrite = "[assembly: AssemblyInformationalVersion(\"{0}\")]";

        public static void UpdateAll(string version)
        {
            foreach (var assemblyInfo in Directory.GetFiles(".", AssemblyInfoFile, SearchOption.AllDirectories))
            {
                UpdateAll(assemblyInfo, version);
            }
        }

        public static void UpdateAll(string assemblyInfo, string version)
        {
            var lines = File.ReadAllLines(assemblyInfo)
                .Select(x => SubstituteVersion(x, version))
                .Select(y => SubstituteInformationalVersion(y, version)).ToList();

            File.WriteAllLines(assemblyInfo, AddInformationalIfNotPresent(lines, version));
        }

        private static IEnumerable<string> AddInformationalIfNotPresent(ICollection<string> lines, string version)
        {
            if (lines.Any(l => l.StartsWith(AssemblyInformationalVersionSearch)))
            {
                return lines;
            }

            return lines.Concat(new[] { string.Format(AssemblyInformationalVersionWrite, version) });
        }

        public static string SubstituteVersion(string line, string version)
        {
            return !line.StartsWith(AssemblyVersionSearch)
                ? line
                : string.Format(AssemblyVersionWrite, version);
        }

        public static string SubstituteInformationalVersion(string line, string version)
        {
            return !line.StartsWith(AssemblyInformationalVersionSearch)
                ? line
                : string.Format(AssemblyInformationalVersionWrite, version);
        }
    }
}
