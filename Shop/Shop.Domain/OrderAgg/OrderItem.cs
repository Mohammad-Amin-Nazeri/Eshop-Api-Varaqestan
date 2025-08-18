using Common.Domain;
using Common.Domain.Exceptions;

namespace Shop.Domain.OrderAgg
{
    public class OrderItem :BaseEntity
    {
        public OrderItem(long inventoryId, int count, int price)
        {
            CountGuaard(count);
            PriceGuaard(price);
            InventoryId = inventoryId;
            Count = count;
            Price = price;
        }

        public long OrderId { get; internal set; }
        public long InventoryId { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public int TotalPrice => Price * Count;


        public void IncreaseCount(int count)
        {
            Count += count;
        }

        public void DecreaseCount(int count)
        {
            if(Count == 1)
                return;
            if(Count-count<=0)
                return;

            Count -= count;
            
        }

        public void ChangeCount(int newCount)
        {
            CountGuaard(newCount);

            Count = newCount;
        }

        public void SetPrice(int newPrice)
        {
            PriceGuaard(newPrice);
            Price = newPrice;
        }

        public void PriceGuaard(int newPrice)
        {
            if (newPrice < 1)
                throw new InvalidDomainDataException("مبلغ نامعتبر");
        }

        public void CountGuaard(int newCount)
        {
            if (newCount < 1)
                throw new InvalidDomainDataException();
        }
    }
}
