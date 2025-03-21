using System;
using Intent.RoslynWeaver.Attributes;

[assembly: IntentTemplate("Intent.Entities.DomainEntity", Version = "2.0")]

namespace EntityFrameworkCore.SqlServer.EF8.Domain.Entities.TPT.Polymorphic
{
    public class TPT_Poly_BaseClassNonAbstract : TPT_Poly_RootAbstract
    {
        public TPT_Poly_BaseClassNonAbstract()
        {
            BaseField = null!;
        }
        public string BaseField { get; set; }

        public Guid? Poly_SecondLevelId { get; set; }
    }
}