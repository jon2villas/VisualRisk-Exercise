using System;

namespace NPVCalculator.Web.Models
{
    public class CashFlow
    {
        public long Id { get; set; }
        public decimal Value { get; set; }
        public long ParameterId { get; set; }
    }
}