using System;
using Intent.Engine;
using Intent.Modules.Common.CSharp.Nuget;
using Intent.Modules.Common.VisualStudio;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.CSharp.Templates.NugetPackages", Version = "1.0")]

namespace Intent.Modules.EntityFrameworkCore.SecondLevelCaching
{
    public class NugetPackages : INugetPackages
    {
        public const string EFCoreSecondLevelCacheInterceptorPackageName = "EFCoreSecondLevelCacheInterceptor";
        public const string MessagePackPackageName = "MessagePack";

        public void RegisterPackages()
        {
            NugetRegistry.Register(EFCoreSecondLevelCacheInterceptorPackageName,
                (framework) => framework switch
                    {
                        ( >= 9, 0) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "9.0.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "9.0.0")
                            .WithNugetDependency("System.IO.Hashing", "9.0.0"),
                        ( >= 8, 0) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "8.0.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "8.0.0")
                            .WithNugetDependency("Microsoft.Extensions.Caching.Memory", "8.0.1")
                            .WithNugetDependency("System.IO.Hashing", "8.0.0"),
                        ( >= 7, 0) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "7.0.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "7.0.0"),
                        ( >= 6, 0) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "6.0.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "6.0.0")
                            .WithNugetDependency("Microsoft.Extensions.Caching.Memory", "6.0.2"),
                        ( >= 2, 1) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "5.0.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "5.0.0"),
                        ( >= 2, 0) => new PackageVersion("4.8.8")
                            .WithNugetDependency("AsyncKeyedLock", "7.1.3")
                            .WithNugetDependency("CacheManager.Core", "1.2.0")
                            .WithNugetDependency("EasyCaching.Core", "1.9.2")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore", "3.1.0")
                            .WithNugetDependency("Microsoft.EntityFrameworkCore.Relational", "3.1.0"),
                        _ => throw new Exception($"Unsupported Framework `{framework.Major}` for NuGet package '{EFCoreSecondLevelCacheInterceptorPackageName}'"),
                    }
                );
            NugetRegistry.Register(MessagePackPackageName,
                (framework) => framework switch
                    {
                        ( >= 6, 0) => new PackageVersion("2.5.192")
                            .WithNugetDependency("MessagePack.Annotations", "2.5.192")
                            .WithNugetDependency("Microsoft.NET.StringTools", "17.6.3"),
                        ( >= 2, 0) => new PackageVersion("2.5.192")
                            .WithNugetDependency("MessagePack.Annotations", "2.5.192")
                            .WithNugetDependency("Microsoft.Bcl.AsyncInterfaces", "6.0.0")
                            .WithNugetDependency("Microsoft.NET.StringTools", "17.6.3")
                            .WithNugetDependency("System.Collections.Immutable", "6.0.0")
                            .WithNugetDependency("System.Reflection.Emit", "4.7.0")
                            .WithNugetDependency("System.Reflection.Emit.Lightweight", "4.7.0")
                            .WithNugetDependency("System.Threading.Tasks.Extensions", "4.5.4")
                            .WithNugetDependency("System.Memory", "4.5.5")
                            .WithNugetDependency("System.Runtime.CompilerServices.Unsafe", "6.0.0"),
                        _ => throw new Exception($"Unsupported Framework `{framework.Major}` for NuGet package '{MessagePackPackageName}'"),
                    }
                );
        }

        public static NugetPackageInfo EFCoreSecondLevelCacheInterceptor(IOutputTarget outputTarget) => NugetRegistry.GetVersion(EFCoreSecondLevelCacheInterceptorPackageName, outputTarget.GetMaxNetAppVersion());

        public static NugetPackageInfo MessagePack(IOutputTarget outputTarget) => NugetRegistry.GetVersion(MessagePackPackageName, outputTarget.GetMaxNetAppVersion());
    }
}
