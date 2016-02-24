using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BumpVersion
{
    public sealed class VersionBumper
    {
        public const string VersionFile = "version.txt";

        public void Run(Version version)
        {
            var nextVersion = new VersionInfo(version);
            SaveVersions(VersionReader.PreviousVersions().Concat(new[] { nextVersion }));
            AssemblyInfoUpdater.UpdateAll(nextVersion.VersionString());
        }

        private static void SaveVersions(IEnumerable<VersionInfo> version)
            => File.WriteAllLines(VersionFile, version.Select(v => v.ToString()));
    }
}
