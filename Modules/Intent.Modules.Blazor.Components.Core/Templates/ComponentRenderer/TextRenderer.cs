using Intent.Metadata.Models;
using Intent.Modelers.UI.Core.Api;
using Intent.Modules.Blazor.Components.Core.Templates.RazorComponent;

namespace Intent.Modules.Blazor.Components.Core.Templates.ComponentRenderer;

public class TextRenderer : IComponentRenderer
{
    private readonly IComponentRendererResolver _componentResolver;
    private readonly RazorComponentTemplate _template;

    public TextRenderer(IComponentRendererResolver componentResolver, RazorComponentTemplate template)
    {
        _componentResolver = componentResolver;
        _template = template;
    }

    public void BuildComponent(IElement component, IRazorFileNode node)
    {
        var textInput = new TextModel(component);
        var valueMapping = _template.GetMappedEndFor(textInput);
        var htmlElement = new HtmlElement("label", _template.BlazorFile)
            .WithText(valueMapping != null ? _template.GetCodeDirective(valueMapping) : textInput.Value);
        node.AddNode(htmlElement);
    }
}