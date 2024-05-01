﻿using Intent.Metadata.Models;
using Intent.Modelers.UI.Core.Api;
using Intent.Modules.Blazor.Api;

namespace Intent.Modules.Blazor.Components.Core.Templates.ComponentRenderer;

public class TextInputComponentBuilder : IRazorComponentBuilder
{
    private readonly IRazorComponentBuilderProvider _componentResolver;
    private readonly IRazorComponentTemplate _componentTemplate;

    public TextInputComponentBuilder(IRazorComponentBuilderProvider componentResolver, IRazorComponentTemplate template)
    {
        _componentResolver = componentResolver;
        _componentTemplate = template;
    }

    public void BuildComponent(IElement component, IRazorFileNode node)
    {
        var textInput = new TextInputModel(component);
        var htmlElement = new HtmlElement("label", _componentTemplate.RazorFile)
            .WithText(textInput.GetLabelAddon()?.Label())
            .AddHtmlElement("InputText", inputText =>
            {
                inputText.AddAttribute("@bind-Value", textInput.Value.Trim('{', '}'));
            });
        node.AddChildNode(htmlElement);
    }
}
