using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace NPVCalculator.Api.Models
{
    public class CashFlow : BaseEntity
    {
        public decimal Value { get; set; }

        [JsonIgnore]
        [ForeignKey("ParameterId")]
        public Parameter Parameter { get; set; }
        public long ParameterId { get; set; }

        public CashFlow()
        {

        }

        public CashFlow(decimal value)
        {
            this.Value = value;
        }
    }
}