using System;
using System.Collections.Generic;

namespace NPVCalculator.Web.Models
{
    public class Calculation
    {
        public long Id { get; set; }
        public double DiscountRate { get; set; }
        public decimal NetPresentValue { get; set; }
        public long ParameterId { get; set; }
        public Parameter Parameter { get; set; }
    }
}