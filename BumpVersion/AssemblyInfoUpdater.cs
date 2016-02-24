using System.IO;
using System.Linq;

namespace BumpVersion
{
    public static class AssemblyInfoUpdater
    {
        public const string AssemblyInfoFile = "*AssemblyInfo.cs";
        public const string AssemblyVersionSearch = "[assembly: AssemblyVersion";
        public const string AssemblyVersionWrite = "[assembly: AssemblyVersion(\"{0}\")]";

        public static void UpdateAll(string version)
        {
            foreach (var assemblyInfo in Directory.GetFiles(".", AssemblyInfoFile, SearchOption.AllDirectories))
            {
                UpdateAssemblyInfo(assemblyInfo, version);
            }
        }

        public static void UpdateAssemblyInfo(string assemblyInfo, string version)
        {
            File.WriteAllLines(assemblyInfo,
                File.ReadAllLines(assemblyInfo).Select(l => Substitute(l, version)).ToArray());
        }

        public static string Substitute(string line, string version)
        {
            return !line.StartsWith(AssemblyVersionSearch) ? line : string.Format(AssemblyVersionWrite, version);
        }
    }
}