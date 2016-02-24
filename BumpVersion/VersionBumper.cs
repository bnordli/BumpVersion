using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BumpVersion
{
    public sealed class VersionBumper
    {
        public const string VersionFile = "version.txt";

        public void Run(Version version, bool prune)
        {
            var previousVersions = VersionReader.PreviousVersions();
            if (prune)
            {
                previousVersions = PruneOldRevisions(previousVersions);
            }

            var nextVersion = new VersionInfo(version);
            SaveVersions(previousVersions.Concat(new[] { nextVersion }));
            AssemblyInfoUpdater.UpdateAll(nextVersion.VersionString());
        }

        private static IEnumerable<VersionInfo> PruneOldRevisions(IEnumerable<VersionInfo> previousVersions)
        {
            return previousVersions.Where(v => v.Version.Revision == 0);
        }

        private static void SaveVersions(IEnumerable<VersionInfo> version)
        {
            File.WriteAllLines(VersionFile, version.Select(v => v.ToString()));
        }
    }
}