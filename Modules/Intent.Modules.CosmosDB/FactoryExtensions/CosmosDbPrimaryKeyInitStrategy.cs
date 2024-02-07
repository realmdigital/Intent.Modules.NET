﻿using Intent.Modelers.Domain.Api;
using Intent.Modules.Common.CSharp.Templates;
using Intent.Modules.DocumentDB.Shared;

namespace Intent.Modules.CosmosDB.FactoryExtensions;

public class CosmosDbPrimaryKeyInitStrategy : IPrimaryKeyInitStrategy
{
    public bool CanExecute(ClassModel model)
    {
        return true;
    }

    public string GetGetterInitExpression(ICSharpTemplate template, string fieldName, string fieldTypeName)
    {
        return fieldTypeName switch
        {
            "string" => $"{fieldName} ??= {template.UseType("System.Guid")}.NewGuid().ToString()",
            "guid" => $"{fieldName} ??= {template.UseType("System.Guid")}.NewGuid()",
            "int" or "long" => $"{fieldName} ?? throw new {template.UseType("System.NullReferenceException")}(\"{fieldName} has not been set\")",
            _ => fieldName
        };
    }
}