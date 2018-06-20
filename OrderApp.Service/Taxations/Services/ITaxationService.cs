using System.Collections.Generic;

namespace OrderApp.Service.Taxations
{
    public interface ITaxationService
    {
        List<Taxation> GetTaxations();
    }
}