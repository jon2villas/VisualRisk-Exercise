using System;
using System.Collections.Generic;

namespace NPVCalculator.Web.Models
{
    public class Parameter
    {
        public long Id { get; set; }
        public decimal InitialValue { get; set; }
        public double LowerBoundDiscountRate { get; set; }
        public double UpperBoundDiscountRate { get; set; }
        public double DiscountRateIncrement { get; set; }
        public List<CashFlow> CashFlows { get; set; }
    }
}