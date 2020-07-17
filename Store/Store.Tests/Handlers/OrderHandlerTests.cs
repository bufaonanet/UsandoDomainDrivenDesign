using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customer;
        private readonly IDeliveryFeeRepository _delivery;
        private readonly IDiscountRepository _discount;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _product;

        public OrderHandlerTests()
        {
            _customer = new FakeCustomerRepository();
            _delivery = new FakeDeliveryFeeRepository();
            _discount = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _product = new FakeProductRepository(); ;
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_nao_deve_gerar_o_pedido()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "12345678";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        // [TestMethod]
        // [TestCategory("Handlers")]
        // public void Dado_um_comando_valido_deve_gerar_o_pedido()
        // {
        //     var command = new CreateOrderCommand();
        //     command.Customer = "123456789";
        //     command.ZipCode = "12345678";
        //     command.PromoCode = "12345678";
        //     command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
        //     command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            
        //     var handler = new OrderHandler()



        //     Assert.AreEqual(command.Valid, false);
        // }
    }
}