using System;
using System.Linq;

namespace BumpVersion
{
    public static class DetermineVersion
    {
        public static Version Next(VersionType versionType)
        {
            var previousVersions = VersionReader.PreviousVersions();
            return NextVersion(previousVersions.DefaultIfEmpty(VersionInfo.Default).Last(), versionType);
        }

        public static Version Set(string versionString)
        {
            Version parsed;
            if (!Version.TryParse(versionString, out parsed))
            {
                throw new MessageException($"Could not parse '{versionString}' as a version.");
            }

            return new Version(
                parsed.Major,
                parsed.Minor,
                parsed.Build < 0 ? 0 : parsed.Build);
        }

        private static Version NextVersion(VersionInfo versionInfo, VersionType versionType)
        {
            var version = versionInfo.Version;
            switch (versionType)
            {
                case VersionType.Major:
                    return new Version(version.Major + 1, 0, 0);
                case VersionType.Minor:
                    return new Version(version.Major, version.Minor + 1, 0);
                case VersionType.Build:
                    return new Version(version.Major, version.Minor, version.Build + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(versionType));
            }
        }
    }
}