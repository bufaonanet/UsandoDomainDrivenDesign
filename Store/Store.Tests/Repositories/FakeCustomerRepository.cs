using Store.Domain.Repositories;
using Store.Domain.Entities;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if(document == "12345678911")
                return new Customer("Douglas","douglas@email.com");

            return null;
        }
    }
}