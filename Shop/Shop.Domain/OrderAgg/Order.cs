using Common.Domain;
using Common.Domain.Exceptions;
using Shop.Domain.OrderAgg.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.OrderAgg
{
    public class Order : AggregateRoot
    {
        private Order()
        {

        }
        public Order(long userId)
        {
            UserId = userId;
            Status = OrderStatus.Pennding;
            Items = new List<OrderItem>();
        }
        public long UserId { get; private set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime? LastUpdate { get; private set; }
        public OrderDiscount? Discount { get; private set; }
        public OrderAddress? Address { get; private set; }
        public ShippingMethod? shippingMethod { get; private set; }
        public int TotalPrice { 
            get
            {
                var totalPrice = Items.Sum(x => x.TotalPrice);
                if (shippingMethod != null)
                    totalPrice += shippingMethod.ShippingCost;

                if (Discount != null)
                    totalPrice -= Discount.DisconuntAmount;
                return totalPrice;
            }
        }
        public int ItemCount => Items.Count;


        public void AddItem(OrderItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(long itemId)
        {
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
              if(currentItem != null)
                Items.Remove(currentItem);
        }

        public void ChangeCountItem(long itemId , int newCount)
        {
            var currentItem = Items.FirstOrDefault(x => x.Id == itemId);
            if (currentItem == null)
                throw new NullOrEmptyDomainDataException();

            currentItem.ChangeCount(newCount);

        }

        public void ChangeStatus(OrderStatus status)
        {
            Status = status;
            LastUpdate = DateTime.Now;
        }

        public void Checkout(OrderAddress orderAddress)
        {
            Address = orderAddress;

        }
    }

}
