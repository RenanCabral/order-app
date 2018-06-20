using OrderApp.DAL.Entities;
using System.Collections.Generic;

namespace OrderApp.DAL.CsvDataReader
{
    public interface ICsvProductItemReader
    {
        List<ProductItem> Read();
    }
}