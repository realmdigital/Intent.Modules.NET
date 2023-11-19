﻿using System;
using Intent.Engine;
using Intent.Modules.Common.CSharp.VisualStudio;
using Intent.Modules.Common.VisualStudio;

namespace Intent.Modules.AspNetCore.MultiTenancy
{
    public static class NugetPackages
    {
        public static NugetPackageInfo EntityFrameworkCoreInMemory(IOutputTarget outputTarget) => new("Microsoft.EntityFrameworkCore.InMemory", GetVersion(outputTarget.GetProject()));
        public static readonly NugetPackageInfo FinbuckleMultiTenant = new NugetPackageInfo("Finbuckle.MultiTenant", "6.12.0");
        public static readonly NugetPackageInfo FinbuckleMultiTenantAspNetCore = new NugetPackageInfo("Finbuckle.MultiTenant.AspNetCore", "6.12.0");
        public static readonly NugetPackageInfo FinbuckleMultiTenantEntityFrameworkCore = new NugetPackageInfo("Finbuckle.MultiTenant.EntityFrameworkCore", "6.12.0");

        private static string GetVersion(ICSharpProject project)
        {
            return project switch
            {
                _ when project.IsNetCore2App() => throw new Exception(".NET Core 2.x is no longer supported."),
                _ when project.IsNetCore3App() => "3.1.24",
                _ when project.IsNetApp(5) => "5.0.17",
                _ when project.IsNetApp(6) => "6.0.20",
                _ when project.IsNetApp(7) => "7.0.9",
                _ when project.IsNetApp(8) => "8.0.0",
                _ => "6.0.20"
            };
        }
    }
}
