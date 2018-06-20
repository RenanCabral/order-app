using System.Collections.Generic;

namespace OrderApp.Service.Taxations
{
    public class TaxationService : ITaxationService
    {
        public List<Taxation> GetTaxations()
        {
            List<Taxation> taxationList = new List<Taxation>();
            
            // Include others taxations here
            taxationList.Add(ObjectCreator<Taxation>.CreateInstance("ValueAddedTax"));

            return taxationList;
        }
    }
}
