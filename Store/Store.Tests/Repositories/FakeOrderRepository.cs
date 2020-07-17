using Store.Domain.Repositories;
using Store.Domain.Entities;
using System;

namespace Store.Tests.Repositories
{
    public class FakeOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            
        }
    }
}