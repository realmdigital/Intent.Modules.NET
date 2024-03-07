using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Intent.Engine;
using Intent.Modules.AspNetCore.Events;
using Intent.Modules.AspNetCore.Logging.Serilog.Settings;
using Intent.Modules.AspNetCore.Templates.Startup;
using Intent.Modules.Common.Templates;
using Intent.Modules.VisualStudio.Projects.Templates.CoreWeb.AppSettings;
using Intent.RoslynWeaver.Attributes;
using Newtonsoft.Json.Linq;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.TemplateDecorator", Version = "1.0")]

namespace Intent.Modules.AspNetCore.Logging.Serilog.Decorators
{
    [IntentManaged(Mode.Merge)]
    public class ConfigurationSettingsSerilogLoggingDecorator : AppSettingsDecorator
    {
        [IntentManaged(Mode.Fully)] public const string DecoratorId = "Intent.Modules.AspNetCore.Logging.Serilog.ConfigurationSettingsSerilogLoggingDecorator";

        [IntentManaged(Mode.Fully)] private readonly AppSettingsTemplate _template;
        [IntentManaged(Mode.Fully)] private readonly IApplication _application;

        [IntentManaged(Mode.Merge)]
        public ConfigurationSettingsSerilogLoggingDecorator(AppSettingsTemplate template, IApplication application)
        {
            _template = template;
            _application = application;
        }

        public override void UpdateSettings(AppSettingsEditor appSettings)
        {
            var serilog = appSettings.GetProperty<JObject>("Serilog");
            serilog = CreateDefaultSerilogSettingsObj(serilog);

            PopulateUsings(serilog);
            PopulateEnriches(serilog);
            PopulateWriteToSinks(serilog);

            appSettings.SetProperty("Serilog", serilog);
        }

        private static JObject CreateDefaultSerilogSettingsObj(JObject serilog)
        {
            serilog = serilog ?? new JObject()
            {
                {
                    "MinimumLevel", new JObject
                    {
                        { "Default", "Information" },
                        {
                            "Override", new JObject()
                            {
                                { "Microsoft", "Warning" },
                                { "System", "Warning" }
                            }
                        }
                    }
                }
            };
            return serilog;
        }

        private void PopulateWriteToSinks(JObject serilog)
        {
            var writeTo = serilog.TryGetValue("WriteTo", out var sinkEntry) ? (JArray)sinkEntry! : new JArray();
            serilog.TryAdd("WriteTo", writeTo);

            var currentSinks = writeTo.Cast<JObject>().Select(x => x.GetValue("Name")?.Value<string>()).ToHashSet();
            var selectedSinks = _application.Settings.GetSerilogSettings().Sinks().Select(sink => SerilogOptionToSectionName(sink.AsEnum())).ToHashSet();
            var managedSinks = Enum.GetValues<SerilogSettings.SinksOptionsEnum>().Select(SerilogOptionToSectionName).ToHashSet();
            
            // Remove sinks not present in the valid sinks
            foreach (var sink in currentSinks.Except(selectedSinks).ToArray())
            {
                var sinkToRemove = writeTo.FirstOrDefault(x => x["Name"]?.ToString() == sink && managedSinks.Contains(sink));
                if (sinkToRemove is not null)
                {
                    writeTo.Remove(sinkToRemove);
                }
            }

            // Add new sinks
            foreach (var serilogSink in _application.Settings.GetSerilogSettings().Sinks())
            {
                var sinkName = SerilogOptionToSectionName(serilogSink.AsEnum());
                if (currentSinks.Contains(sinkName))
                {
                    continue;
                }

                var sinkToAdd = new JObject { ["Name"] = sinkName };
                var args = new JObject();
                switch (serilogSink.AsEnum())
                {
                    case SerilogSettings.SinksOptionsEnum.Console:
                        args["outputTemplate"] = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] {Category:0} - {Message}{NewLine:1}";
                        args["restrictedToMinimumLevel"] = "Information";
                        break;
                    case SerilogSettings.SinksOptionsEnum.File:
                        args["path"] = @"Logs\.log";
                        args["rollingInterval"] = "Day";
                        args["restrictedToMinimumLevel"] = "Information";
                        break;
                    case SerilogSettings.SinksOptionsEnum.Graylog:
                        args["hostnameOrAddress"] = "localhost";
                        args["port"] = "12201";
                        args["transportType"] = "Udp";
                        break;
                    default:
                        continue; // If the sink is not recognized, skip adding it
                }

                sinkToAdd["Args"] = args;
                writeTo.Add(sinkToAdd);
            }

            return;

            static string SerilogOptionToSectionName(SerilogSettings.SinksOptionsEnum option)
            {
                return option switch
                {
                    SerilogSettings.SinksOptionsEnum.Console => "Console",
                    SerilogSettings.SinksOptionsEnum.File => "File",
                    SerilogSettings.SinksOptionsEnum.Graylog => "Graylog",
                    _ => null
                };
            }
        }

        private void PopulateUsings(JObject serilog)
        {
            var usingArr = serilog.TryGetValue("Using", out var usingSink) ? (JArray)usingSink! : new JArray();
            serilog.TryAdd("Using", usingArr);

            // Add "Using" only if it doesn't exist
            var existingUsings = new HashSet<string>(usingArr.Select(u => u.ToString()));
            var validUsings = _application.Settings.GetSerilogSettings().Sinks()
                .Select(sink => SerilogOptionToType(sink.AsEnum()))
                .ToHashSet();
            var managedSinks = Enum.GetValues<SerilogSettings.SinksOptionsEnum>().Select(SerilogOptionToType).ToHashSet();

            // Remove invalid usings
            foreach (var usingToRemove in existingUsings.Except(validUsings).ToList())
            {
                var itemToRemove = usingArr.FirstOrDefault(u => u.ToString() == usingToRemove && managedSinks.Contains(u.ToString()));
                if (itemToRemove != null)
                {
                    usingArr.Remove(itemToRemove);
                }
            }

            // Add new usings
            foreach (var serilogSink in _application.Settings.GetSerilogSettings().Sinks())
            {
                var sinkType = SerilogOptionToType(serilogSink.AsEnum());
                if (!existingUsings.Contains(sinkType))
                {
                    usingArr.Add(sinkType);
                }
            }

            return;
            
            static string SerilogOptionToType(SerilogSettings.SinksOptionsEnum option)
            {
                return option switch
                {
                    SerilogSettings.SinksOptionsEnum.Console => "Serilog.Sinks.Console",
                    SerilogSettings.SinksOptionsEnum.File => "Serilog.Sinks.File",
                    SerilogSettings.SinksOptionsEnum.Graylog => "Serilog.Sinks.Graylog",
                    _ => null // Handle default case gracefully
                };
            }
        }


        private void PopulateEnriches(JObject serilog)
        {
            JArray enrichArr;
            if (serilog.ContainsKey("Enrich"))
            {
                enrichArr = (JArray)serilog.GetValue("Enrich")!;
            }
            else
            {
                enrichArr = new JArray();
                serilog.Add(new JProperty("Enrich", enrichArr));
            }

            var expectedEnriches = new List<string> { "FromLogContext" };
            if (_application.Settings.GetSerilogSettings().Sinks().Any(x => x.IsGraylog()))
            {
                expectedEnriches.Add("WithSpan");
            }

            var existing = new HashSet<string>(enrichArr.Cast<JValue>().Select(s => s.Value?.ToString()));
            foreach (var enrich in expectedEnriches)
            {
                if (existing.Contains(enrich))
                {
                    continue;
                }

                enrichArr.Add(enrich);
            }
        }
    }
}