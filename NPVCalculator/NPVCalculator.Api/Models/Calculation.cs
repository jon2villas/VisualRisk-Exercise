using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace NPVCalculator.Api.Models
{
    public class Calculation : BaseEntity
    {
        [Range(double.Epsilon, 100)]
        public double DiscountRate { get; set; }

        public decimal NetPresentValue { get; set; }

        [ForeignKey("ParameterId")]
        public Parameter Parameter { get; set; }
        public long ParameterId { get; set; }

        public Calculation()
        {

        }

        public Calculation(long parameterId, double discountRate, decimal netPresentValue)
        {
            this.ParameterId = parameterId;
            this.DiscountRate = discountRate;
            this.NetPresentValue = netPresentValue;
        }
    }
}