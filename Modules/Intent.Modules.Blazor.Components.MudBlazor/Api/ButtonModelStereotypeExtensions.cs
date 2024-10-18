using System;
using System.Collections.Generic;
using System.Linq;
using Intent.Metadata.Models;
using Intent.Modelers.UI.Core.Api;
using Intent.Modules.Common;
using Intent.RoslynWeaver.Attributes;

[assembly: DefaultIntentManaged(Mode.Fully)]
[assembly: IntentTemplate("Intent.ModuleBuilder.Templates.Api.ApiElementModelExtensions", Version = "1.0")]

namespace Intent.Blazor.Components.MudBlazor.Api
{
    public static class ButtonModelStereotypeExtensions
    {
        public static Appearance GetAppearance(this ButtonModel model)
        {
            var stereotype = model.GetStereotype(Appearance.DefinitionId);
            return stereotype != null ? new Appearance(stereotype) : null;
        }


        public static bool HasAppearance(this ButtonModel model)
        {
            return model.HasStereotype(Appearance.DefinitionId);
        }

        public static bool TryGetAppearance(this ButtonModel model, out Appearance stereotype)
        {
            if (!HasAppearance(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new Appearance(model.GetStereotype(Appearance.DefinitionId));
            return true;
        }

        public static Icon GetIcon(this ButtonModel model)
        {
            var stereotype = model.GetStereotype(Icon.DefinitionId);
            return stereotype != null ? new Icon(stereotype) : null;
        }


        public static bool HasIcon(this ButtonModel model)
        {
            return model.HasStereotype(Icon.DefinitionId);
        }

        public static bool TryGetIcon(this ButtonModel model, out Icon stereotype)
        {
            if (!HasIcon(model))
            {
                stereotype = null;
                return false;
            }

            stereotype = new Icon(model.GetStereotype(Icon.DefinitionId));
            return true;
        }

        public class Appearance
        {
            private IStereotype _stereotype;
            public const string DefinitionId = "b218f7cb-d150-401d-a17a-9e22fadf863f";

            public Appearance(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string Name => _stereotype.Name;

            public bool IconOnly()
            {
                return _stereotype.GetProperty<bool>("Icon Only");
            }

            public IElement Variant()
            {
                return _stereotype.GetProperty<IElement>("Variant");
            }

            public IElement Color()
            {
                return _stereotype.GetProperty<IElement>("Color");
            }

            public string Class()
            {
                return _stereotype.GetProperty<string>("Class");
            }

            public class VariantOptions
            {
                public readonly string Value;

                public VariantOptions(string value)
                {
                    Value = value;
                }

                public VariantOptionsEnum AsEnum()
                {
                    switch (Value)
                    {
                        case "Default":
                            return VariantOptionsEnum.Default;
                        case "Primary":
                            return VariantOptionsEnum.Primary;
                        case "Secondary":
                            return VariantOptionsEnum.Secondary;
                        case "Tertiary":
                            return VariantOptionsEnum.Tertiary;
                        case "Info":
                            return VariantOptionsEnum.Info;
                        case "Success":
                            return VariantOptionsEnum.Success;
                        case "Warning":
                            return VariantOptionsEnum.Warning;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                public bool IsDefault()
                {
                    return Value == "Default";
                }
                public bool IsPrimary()
                {
                    return Value == "Primary";
                }
                public bool IsSecondary()
                {
                    return Value == "Secondary";
                }
                public bool IsTertiary()
                {
                    return Value == "Tertiary";
                }
                public bool IsInfo()
                {
                    return Value == "Info";
                }
                public bool IsSuccess()
                {
                    return Value == "Success";
                }
                public bool IsWarning()
                {
                    return Value == "Warning";
                }
            }

            public enum VariantOptionsEnum
            {
                Default,
                Primary,
                Secondary,
                Tertiary,
                Info,
                Success,
                Warning
            }

            public class ColorOptions
            {
                public readonly string Value;

                public ColorOptions(string value)
                {
                    Value = value;
                }

                public ColorOptionsEnum AsEnum()
                {
                    switch (Value)
                    {
                        case "Default":
                            return ColorOptionsEnum.Default;
                        case "Primary":
                            return ColorOptionsEnum.Primary;
                        case "Secondary":
                            return ColorOptionsEnum.Secondary;
                        case "Tertiary":
                            return ColorOptionsEnum.Tertiary;
                        case "Info":
                            return ColorOptionsEnum.Info;
                        case "Success":
                            return ColorOptionsEnum.Success;
                        case "Warning":
                            return ColorOptionsEnum.Warning;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                public bool IsDefault()
                {
                    return Value == "Default";
                }
                public bool IsPrimary()
                {
                    return Value == "Primary";
                }
                public bool IsSecondary()
                {
                    return Value == "Secondary";
                }
                public bool IsTertiary()
                {
                    return Value == "Tertiary";
                }
                public bool IsInfo()
                {
                    return Value == "Info";
                }
                public bool IsSuccess()
                {
                    return Value == "Success";
                }
                public bool IsWarning()
                {
                    return Value == "Warning";
                }
            }

            public enum ColorOptionsEnum
            {
                Default,
                Primary,
                Secondary,
                Tertiary,
                Info,
                Success,
                Warning
            }

        }

        public class Icon
        {
            private IStereotype _stereotype;
            public const string DefinitionId = "8e1b7033-fd27-495a-a2a7-36b5168f04f5";

            public Icon(IStereotype stereotype)
            {
                _stereotype = stereotype;
            }

            public string Name => _stereotype.Name;

            public IElement Variant()
            {
                return _stereotype.GetProperty<IElement>("Variant");
            }

            public IElement IconValue()
            {
                return _stereotype.GetProperty<IElement>("Icon Value");
            }

            public IElement IconColor()
            {
                return _stereotype.GetProperty<IElement>("Icon Color");
            }

            public class VariantOptions
            {
                public readonly string Value;

                public VariantOptions(string value)
                {
                    Value = value;
                }

                public VariantOptionsEnum AsEnum()
                {
                    switch (Value)
                    {
                        case "Default":
                            return VariantOptionsEnum.Default;
                        case "Primary":
                            return VariantOptionsEnum.Primary;
                        case "Secondary":
                            return VariantOptionsEnum.Secondary;
                        case "Tertiary":
                            return VariantOptionsEnum.Tertiary;
                        case "Info":
                            return VariantOptionsEnum.Info;
                        case "Success":
                            return VariantOptionsEnum.Success;
                        case "Warning":
                            return VariantOptionsEnum.Warning;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                public bool IsDefault()
                {
                    return Value == "Default";
                }
                public bool IsPrimary()
                {
                    return Value == "Primary";
                }
                public bool IsSecondary()
                {
                    return Value == "Secondary";
                }
                public bool IsTertiary()
                {
                    return Value == "Tertiary";
                }
                public bool IsInfo()
                {
                    return Value == "Info";
                }
                public bool IsSuccess()
                {
                    return Value == "Success";
                }
                public bool IsWarning()
                {
                    return Value == "Warning";
                }
            }

            public enum VariantOptionsEnum
            {
                Default,
                Primary,
                Secondary,
                Tertiary,
                Info,
                Success,
                Warning
            }
            public class IconValueOptions
            {
                public readonly string Value;

                public IconValueOptions(string value)
                {
                    Value = value;
                }

                public IconValueOptionsEnum AsEnum()
                {
                    switch (Value)
                    {
                        case "Default":
                            return IconValueOptionsEnum.Default;
                        case "Primary":
                            return IconValueOptionsEnum.Primary;
                        case "Secondary":
                            return IconValueOptionsEnum.Secondary;
                        case "Tertiary":
                            return IconValueOptionsEnum.Tertiary;
                        case "Info":
                            return IconValueOptionsEnum.Info;
                        case "Success":
                            return IconValueOptionsEnum.Success;
                        case "Warning":
                            return IconValueOptionsEnum.Warning;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                public bool IsDefault()
                {
                    return Value == "Default";
                }
                public bool IsPrimary()
                {
                    return Value == "Primary";
                }
                public bool IsSecondary()
                {
                    return Value == "Secondary";
                }
                public bool IsTertiary()
                {
                    return Value == "Tertiary";
                }
                public bool IsInfo()
                {
                    return Value == "Info";
                }
                public bool IsSuccess()
                {
                    return Value == "Success";
                }
                public bool IsWarning()
                {
                    return Value == "Warning";
                }
            }

            public enum IconValueOptionsEnum
            {
                Default,
                Primary,
                Secondary,
                Tertiary,
                Info,
                Success,
                Warning
            }
            public class IconColorOptions
            {
                public readonly string Value;

                public IconColorOptions(string value)
                {
                    Value = value;
                }

                public IconColorOptionsEnum AsEnum()
                {
                    switch (Value)
                    {
                        case "Default":
                            return IconColorOptionsEnum.Default;
                        case "Primary":
                            return IconColorOptionsEnum.Primary;
                        case "Secondary":
                            return IconColorOptionsEnum.Secondary;
                        case "Tertiary":
                            return IconColorOptionsEnum.Tertiary;
                        case "Info":
                            return IconColorOptionsEnum.Info;
                        case "Success":
                            return IconColorOptionsEnum.Success;
                        case "Warning":
                            return IconColorOptionsEnum.Warning;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                public bool IsDefault()
                {
                    return Value == "Default";
                }
                public bool IsPrimary()
                {
                    return Value == "Primary";
                }
                public bool IsSecondary()
                {
                    return Value == "Secondary";
                }
                public bool IsTertiary()
                {
                    return Value == "Tertiary";
                }
                public bool IsInfo()
                {
                    return Value == "Info";
                }
                public bool IsSuccess()
                {
                    return Value == "Success";
                }
                public bool IsWarning()
                {
                    return Value == "Warning";
                }
            }

            public enum IconColorOptionsEnum
            {
                Default,
                Primary,
                Secondary,
                Tertiary,
                Info,
                Success,
                Warning
            }
        }

    }
}