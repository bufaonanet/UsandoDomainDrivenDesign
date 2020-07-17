using System.Linq;
using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;

namespace Store.Domain.Handlers
{
    public class CommandHandler :
    Notifiable,
    IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customer;
        private readonly IDeliveryFeeRepository _delivery;
        private readonly IDiscountRepository _discount;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _product;

        public CommandHandler(
            ICustomerRepository customer,
            IDeliveryFeeRepository delivery,
            IDiscountRepository discount,
            IOrderRepository order,
            IProductRepository product)
        {
            _customer = customer;
            _delivery = delivery;
            _discount = discount;
            _orderRepository = order;
            _product = product;
        }

        public ICommandResult Handler(CreateOrderCommand command)
        {
            //Fail Fast Validate
            command.Validate();

            if (!command.Valid)
                return new GenericCommandResult(false, "Pedido Inválido", command.Notifications);

            // 1º Recupera Cliente
            var customer = _customer.Get(command.Customer);

            // 2º Calcula Taxa de entrega
            var delivery = _delivery.Get(command.ZipCode);

            // 3º Obtém cupom de desconto 
            var discount = _discount.Get(command.PromoCode);

            var products = _product.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, delivery, discount);

            //4º Gera o pedido
            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            //5º Agrupa as notificações
            AddNotifications(order.Notifications);

            //6º Valida se deu certo
            if (Invalid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", order.Notifications);

            //7º retorna o resultado
            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso!", order);

        }
    }
}