namespace EOS2.Web.Enums
{
    using System.ComponentModel.DataAnnotations;

    public enum CompareToOperator
    {
        [Display(Name = "greater than")]
        GreaterThan,
        [Display(Name = "greater than or equal to")]
        GreaterThanOrEqual,
        [Display(Name = "less than")]
        LessThan,
        [Display(Name = "less than or equal")]
        LessThanOrEqual,
        [Display(Name = "not equal to")]
        NotEqualTo
    }
}