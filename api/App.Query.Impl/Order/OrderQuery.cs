﻿namespace App.Query.Impl.Order
{
    using System;
    using App.Common.Data;
    using App.Common.Data.MongoDB;
    using App.Query.Order;
    using App.Query.Entity.Order;
    using System.Linq;
    using ValueObject.Order;

    public class OrderQuery: BaseQueryRepository<Order>, IOrderQuery
    {
        public OrderQuery():base(new MongoDbContext()){}

        public void ActivateOrder(Guid orderId)
        {
            App.Query.Entity.Order.Order order = this.DbSet.AsQueryable().FirstOrDefault(item => item.OrderId == orderId);
            order.IsActivated = true;
            this.DbSet.Update(order);
        }

        public void AddOrderLineItem(Guid orderId, decimal price)
        {
            App.Query.Entity.Order.Order order = this.DbSet.AsQueryable().FirstOrDefault(item => item.OrderId == orderId);
            order.OrderLines.Add(new OrderLine(price));
            this.DbSet.Update(order);
        }
        public void CreateOrder(Guid orderId)
        {
             this.DbSet.Add(new App.Query.Entity.Order.Order(orderId));
        }

        public void UpdateCustomerDetail(Guid orderId, string customerName)
        {
            App.Query.Entity.Order.Order order = this.DbSet.AsQueryable().FirstOrDefault(item => item.OrderId == orderId);
            order.Customer = new OrderCustomerDetail(customerName);
            this.DbSet.Update(order);
        }
    }
}
