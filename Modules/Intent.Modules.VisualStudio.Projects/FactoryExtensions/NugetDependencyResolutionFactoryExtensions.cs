﻿using Intent.Modules.Common.Plugins;
using Intent.SoftwareFactory;
using Intent.Engine;
using Intent.Plugins.FactoryExtensions;
using Intent.Templates;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Linq;
using Intent.Modules.Common;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.Common.Templates;
using Intent.Modules.Common.VisualStudio;
using Intent.Modules.Constants;
using Intent.Utils;
using Intent.Modules.Common.CSharp.Nuget;
using Intent.Modules.Common.CSharp.VisualStudio;

namespace Intent.Modules.VisualStudio.Projects.FactoryExtensions
{
    [Description("Visual Studio Dependancy Resolver")]
    public class NugetDependencyResolutionFactoryExtensions : FactoryExtensionBase, IExecutionLifeCycle
    {
        public override string Id => "Intent.VSProjects.NuGetDependencyResolver";

        public override int Order => 1000;

        public void OnStep(IApplication application, string step)
        {
            if (step == ExecutionLifeCycleSteps.AfterTemplateRegistrations)
            {
                foreach (var project in application.OutputTargets.Where(x => x.IsVSProject()))
                {
                    project.InitializeVSMetadata();
                }
            }
            if (step == ExecutionLifeCycleSteps.BeforeTemplateExecution)
            {
                ResolveNuGetDependencies(application);
            }
        }

        public void ResolveNuGetDependencies(IApplication application)
        {
            // Resolve all dependencies and events
            Logging.Log.Info($"Resolving NuGet Dependencies");

            foreach (var outputTarget in application.OutputTargets)
            {
                var project = outputTarget.GetProject();
                
                project.AddNugetPackageInstalls(GetAllemplateNugetInstalls(outputTarget));

                var assemblyDependencies = outputTarget.TemplateInstances
                        .SelectMany(ti => ti.GetAllAssemblyDependencies())
                        .Distinct()
                        .ToList();

                foreach (var assemblyDependency in assemblyDependencies)
                {
                    project.AddReference(assemblyDependency);
                }
            }
        }

        private IEnumerable<NuGetInstall> GetAllemplateNugetInstalls(IOutputTarget outputTarget)
        {
            return outputTarget.TemplateInstances
                    .SelectMany(ti => ti.GetAllNuGetInstalls())
                    .Distinct()
                    .ToList();
        }
    }
}

