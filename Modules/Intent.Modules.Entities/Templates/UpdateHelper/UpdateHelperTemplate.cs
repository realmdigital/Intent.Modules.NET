// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Intent.Modules.Entities.Templates.UpdateHelper
{
    using System.Collections.Generic;
    using System.Linq;
    using Intent.Modules.Common;
    using Intent.Modules.Common.Templates;
    using Intent.Modules.Common.CSharp.Templates;
    using Intent.Templates;
    using Intent.Metadata.Models;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Entities\Templates\UpdateHelper\UpdateHelperTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class UpdateHelperTemplate : CSharpTemplateBase<object>
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public override string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\n\r\n[assembly: DefaultIntentManag" +
                    "ed(Mode.Fully)]\r\n\r\nnamespace ");
            
            #line 15 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Entities\Templates\UpdateHelper\UpdateHelperTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Namespace));
            
            #line default
            #line hidden
            this.Write("\r\n{\r\n    /// <summary>\r\n    /// Provides utility methods for updating collections" +
                    ".\r\n    /// </summary>\r\n    public static class ");
            
            #line 20 "C:\Dev\Intent.Modules.NET\Modules\Intent.Modules.Entities\Templates\UpdateHelper\UpdateHelperTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ClassName));
            
            #line default
            #line hidden
            this.Write("\r\n    {\r\n        /// <summary>\r\n        /// Performs mutations to synchronize the" +
                    " baseCollection to end up the same as the changedCollection.\r\n        /// </summ" +
                    "ary>\r\n        /// <typeparam name=\"TChanged\">The type of items in the changed co" +
                    "llection.</typeparam>\r\n        /// <typeparam name=\"TOriginal\">The type of items" +
                    " in the base collection.</typeparam>\r\n        /// <param name=\"baseCollection\">T" +
                    "he base collection to be updated.</param>\r\n        /// <param name=\"changedColle" +
                    "ction\">The collection containing the changes.</param>\r\n        /// <param name=\"" +
                    "equalityCheck\">A predicate that determines if an item from the base collection m" +
                    "atches an item from the changed collection.</param>\r\n        /// <param name=\"as" +
                    "signmentAction\">A delegate that defines how to update an item from the base coll" +
                    "ection using an item from the changed collection.</param>\r\n        /// <returns>" +
                    "The updated base collection.</returns>\r\n        /// <remarks>\r\n        /// If th" +
                    "e changed collection is <see langword=\"null\" />, an empty list of type <see cref" +
                    "=\"TOriginal\"/> will be returned.\r\n        /// If the base collection is <see lan" +
                    "gword=\"null\" />, a new list of type <see cref=\"TOriginal\"/> will be created and " +
                    "used.\r\n        /// </remarks>\r\n        public static ICollection<TOriginal> Crea" +
                    "teOrUpdateCollection<TChanged, TOriginal>(\r\n            ICollection<TOriginal> b" +
                    "aseCollection,\r\n            ICollection<TChanged>? changedCollection,\r\n         " +
                    "   Func<TOriginal, TChanged, bool> equalityCheck,\r\n            Func<TOriginal?, " +
                    "TChanged, TOriginal> assignmentAction)\r\n        {\r\n            if (changedCollec" +
                    "tion == null)\r\n            {\r\n                return new List<TOriginal>();\r\n   " +
                    "         }\r\n\r\n            baseCollection ??= new List<TOriginal>()!;\r\n\r\n        " +
                    "    var result = baseCollection.CompareCollections(changedCollection, equalityCh" +
                    "eck);\r\n            foreach (var elementToAdd in result.ToAdd)\r\n            {\r\n  " +
                    "              var newEntity = assignmentAction(default, elementToAdd);\r\n        " +
                    "        baseCollection.Add(newEntity);\r\n            }\r\n\r\n            foreach (va" +
                    "r elementToRemove in result.ToRemove)\r\n            {\r\n                baseCollec" +
                    "tion.Remove(elementToRemove);\r\n            }\r\n\r\n            foreach (var element" +
                    "ToEdit in result.PossibleEdits)\r\n            {\r\n                assignmentAction" +
                    "(elementToEdit.Original, elementToEdit.Changed);\r\n            }\r\n\r\n            r" +
                    "eturn baseCollection;\r\n        }\r\n    }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
}
