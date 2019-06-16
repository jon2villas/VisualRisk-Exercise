using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace NPVCalculator.Api.Models
{
    public class Parameter : BaseEntity
    {
        [Range(double.Epsilon, 100)]
        public double LowerBoundDiscountRate { get; set; }
        
        [Range(double.Epsilon, 100)]
        public double UpperBoundDiscountRate { get; set; }
        
        [Range(0, 100)]
        public double DiscountRateIncrement { get; set; }

        public decimal InitialValue { get; set; }

        [Required]
        public ICollection<CashFlow> CashFlows { get; set; }
    }
}