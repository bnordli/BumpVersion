using System;

namespace BumpVersion
{
    public sealed class VersionInfo
    {
        public VersionInfo(Version version) : this(version, DateTime.UtcNow)
        {
        }

        private VersionInfo(Version version, DateTime timestamp)
        {
            Timestamp = timestamp;
            Version = version;
        }

        public Version Version { get; private set; }

        public DateTime Timestamp { get; private set; }

        public static VersionInfo Parse(string line)
        {
            var parts = line.Split(' ');
            try
            {
                return new VersionInfo(Version.Parse(parts[0]), DateTime.Parse(parts[1]));
            }
            catch (ArgumentException)
            {
            }
            catch (FormatException)
            {
            }
            catch (IndexOutOfRangeException)
            {
            }

            throw new MessageException(
                string.Format(
                    "Could not parse version line. Expected 'Major.Minor.Revision.Build Timestamp', but got '{0}'",
                    line));
        }

        public string VersionString()
        {
            return Version.ToString(4);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", VersionString(), Timestamp.ToString("s"));
        }
    }
}