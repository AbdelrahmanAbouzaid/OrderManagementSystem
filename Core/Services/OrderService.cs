
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.DTOs;
using System;

namespace Services
{
    public class OrderService(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService) : IOrderService
    {
        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            //TODO:: Get customer by Id for validation


            // Validate products and stock
            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in createOrderDto.OrderItems)
            {
                var product = await unitOfWork.GetRepository<Product>().GetByIdAsync(item.ProductId);

                if (product == null || product.Stock < item.Quantity)
                    throw new OrderBadRequestException($"Product with ID {item.ProductId} is not available or insufficient stock.");

                decimal itemTotal = product.Price * item.Quantity;
                decimal discount = CalculateDiscount(itemTotal);
                var unitPrice = product.Price - discount;

                product.Stock -= item.Quantity;

                orderItems.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    Discount = discount
                });

                totalAmount += unitPrice * item.Quantity;

            }

            if(!Enum.TryParse<PaymentMethod>(createOrderDto.PaymentMethod, true, out var paymentMethod))
                 throw new OrderBadRequestException("Invalid payment method");

            var order = new Order
            {
                CustomerId = createOrderDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                PaymentMethod = paymentMethod,
                Status = OrderStatus.Pending,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            await unitOfWork.GetRepository<Order>().AddAsync(order);

            var invoice = new Invoice
            {
                Order = order,
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = totalAmount
            };

            await unitOfWork.GetRepository<Invoice>().AddAsync(invoice);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var spec = new OrderSpecification();
            var orders = await unitOfWork.GetRepository<Order>().GetAllAsync(spec);
            if (orders == null || !orders.Any())
                throw new OrderNotFoundException();

            return mapper.Map<IEnumerable<OrderDto>>(orders);

        }
        public async Task<OrderDto> GetOrderByIdAsync(int orderId)
        {
            var spec = new OrderSpecification(orderId);
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(spec);
            if (order == null)
                throw new OrderNotFoundException();
            return mapper.Map<OrderDto>(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var spec = new OrderSpecification(orderId);
            var order = await unitOfWork.GetRepository<Order>().GetByIdAsync(spec);
            if (order == null)
                throw new OrderNotFoundException();
            if(!Enum.TryParse<OrderStatus>(status, true, out var newStatus))
                throw new OrderBadRequestException("Invalid order status");

            order.Status = newStatus;

            unitOfWork.GetRepository<Order>().Update(order);

            var email = new Email()
            {
                To = order.Customer.Email,
                Subject = "Order Status",
                Body = $"Your Order Status Updated To {status}"
            };

            return await unitOfWork.SaveChangesAsync() >0 
                && mailService.SendEmail(email);
            
        }


        private decimal CalculateDiscount(decimal total)
        {
            if (total > 200) return total * 0.10m;
            if (total > 100) return total * 0.05m;
            return 0m;
        }

    }
}
