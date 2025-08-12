using Common.Domain;

namespace Shop.Domain.OrderAgg.ValueObjects
{
    public class OrderDiscount:ValueObject
    {
        public OrderDiscount(string discountTitle, int disconuntAmount)
        {
            DiscountTitle = discountTitle;
            DisconuntAmount = disconuntAmount;
        }

        public string DiscountTitle { get; private set; }
        public int DisconuntAmount { get; private set; }
    }
}
