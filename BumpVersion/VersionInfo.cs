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

        public Version Version { get; }

        public DateTime Timestamp { get; }

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
                $"Could not parse version line. Expected 'Major.Minor.Revision Timestamp', but got '{line}'");
        }

        public string VersionString() => Version.ToString(3);

        public override string ToString() => $"{VersionString()} {Timestamp.ToString("s")}";

        public static VersionInfo Default => new VersionInfo(new Version(0, 0, 0));
    }
}