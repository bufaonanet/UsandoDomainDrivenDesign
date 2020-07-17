using Store.Domain.Repositories;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("produto 1",10,true));
            products.Add(new Product("produto 2",10,true));
            products.Add(new Product("produto 3",10,true));
            products.Add(new Product("produto 4",10,false));
            products.Add(new Product("produto 5",10,false));

            return products;
        }
    }
}