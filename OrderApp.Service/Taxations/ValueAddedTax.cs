using System;
using System.Configuration;

namespace OrderApp.Service.Taxations
{
    public class ValueAddedTax : Taxation
    {
        public override string Name
        {
            get
            {
                return "VAT";
            }
        }

        public override decimal Percentual
        {
            get
            {
                return Convert.ToDecimal(ConfigurationManager.AppSettings["ValueAddedTaxValue"]);
            }
        }

        public override decimal Apply(decimal amount)
        {
            var taxationAmount = (amount / 100) * Percentual;

            return taxationAmount;
        }
    }
}
