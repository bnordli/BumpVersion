using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BumpVersion
{
    public class Program
    {
        private static string InformationalVersion
            => typeof (Program).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        private static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    Main(new[] { VersionType.Build.ToString() });
                }
                else if (args.Length == 1)
                {
                    OneArgument(args);
                }
                else if (args.Length == 2)
                {
                    TwoArguments(args);
                }
                else
                {
                    Usage();
                }
            }
            catch (MessageException e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }

        private static void OneArgument(string[] args)
        {
            VersionType versionType = DetermineVersionType(args[0]);
            if (versionType == VersionType.Unknown || versionType == VersionType.Set)
            {
                Usage();
            }
            else
            {
                var nextVersion = DetermineVersion.Next(versionType);
                new VersionBumper().Run(nextVersion);
                Console.WriteLine("Version updated to version {0}", nextVersion);
            }
        }

        private static void TwoArguments(string[] args)
        {
            VersionType versionType = DetermineVersionType(args[0]);
            if (versionType != VersionType.Set)
            {
                Usage();
            }
            else
            {
                var nextVersion = DetermineVersion.Set(args[1]);
                new VersionBumper().Run(nextVersion);
                Console.WriteLine("Version manually set to version {0}", nextVersion);
            }
        }

        private static VersionType DetermineVersionType(string arg)
        {
            return Enum.GetNames(typeof (VersionType))
                .Where(type => string.Compare(arg, type, StringComparison.InvariantCultureIgnoreCase) == 0)
                .Select(versionType => (VersionType) Enum.Parse(typeof (VersionType), versionType))
                .FirstOrDefault();
        }

        private static void Usage()
        {
            Console.WriteLine("BumpVersion: version " + InformationalVersion);
            Console.WriteLine(
                "Usage: BumpVersion [ " +
                string.Join(" | ", Enum.GetNames(typeof (VersionType)).Skip(1)) + " version ]");
        }
    }
}