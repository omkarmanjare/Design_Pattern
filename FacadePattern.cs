using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    /// <summary>
    /// A complex system involves multiple subsystems that interact with each other ,
    /// touches multiple datanbases at its own and manages to fulfiull the end to end flow.
    /// In order to not let the client/ consumer of this complex systme get idea of entire subsystems,how components or modules 
    /// communicate and how they work, 
    /// we provide a limited interface/single point of communication to the client and hide all those internal details
    /// within. This is facade  
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
    public class ShippingDetails
    {
        public string Address { get; set; }
    }
    public class OrderItemDetails
    {
        public int ItemId { get; set; }
        public double PricePerUnit { get; set; }
        public int  Quantity { get; set; }
    }
    public class Order
    {
        public string OrderID { get; set; }
        public User UserDetails { get; set; }
        public List<OrderItemDetails>  Items{ get; set; }
        public ShippingDetails Shipping { get; set; }
    }

    public class OrderPlacedResponse {
        public string OrderId { get; set; }
        public DateTime DeliveryTime { get; set; }

    }


    public class TotalAmmount
    {
        public double CalculeFinalAmountWithPromoCode(int noOfItems,double perItemPrice, string promoCode)
        {
            return string.IsNullOrEmpty(promoCode)? noOfItems * perItemPrice : noOfItems * perItemPrice * 0.1;
        }
    }

    public class OrderDetailsRepository
    {
        public void SaveOrderDetailsInDB(Order order)
        {
            Console.WriteLine("Called sp and stored all order details");
        }
    }

    public class CheckPossibleDeliveryDate
    {
        public DateTime CheckPossibleDelivery()
        {
            return DateTime.Now.AddDays(5);
        }
    }

    public class CheckItemInventory
    {
        public bool IsItemAvailable() { return true; }
    }
    

    public class OrderPlacingSystem 
    {
        private string username;
        private int userId;
        private string shippingAddress;
        private int itemId;
        public double pricePerUnit;
        public int quantity;
        public string promocode;

        private TotalAmmount totalAmmount;
        private OrderDetailsRepository detailsRepository;
        private CheckPossibleDeliveryDate checkPossibleDeliveryDate;
        private CheckItemInventory itemInventory;
        private Order order;

        public OrderPlacingSystem(string username,int userId,string shippingAddress, int itemId,double pricePerUnit,
            int quantity,string promocode)
        {
            this.username = username;
            this.userId = userId;
            this.shippingAddress = shippingAddress;
            this.itemId = itemId;
            this.pricePerUnit = pricePerUnit;
            this.quantity = quantity;
            this.promocode = promocode;

            totalAmmount = new TotalAmmount();
            detailsRepository = new OrderDetailsRepository();
            checkPossibleDeliveryDate = new CheckPossibleDeliveryDate();
            itemInventory = new CheckItemInventory();
        }

        public OrderPlacedResponse ProcessOrder()
        {
            if (itemInventory.IsItemAvailable())
            {
                var finalAmount = totalAmmount.CalculeFinalAmountWithPromoCode(quantity, pricePerUnit, promocode);
                var possibleDeliveryDate = checkPossibleDeliveryDate.CheckPossibleDelivery();

                order = new Order();
                order.OrderID = new Random(12).Next(121211,9898989).ToString();
                order.UserDetails = new User() { UserId = userId, UserName = username };
                order.Shipping = new ShippingDetails() { Address = shippingAddress };
                order.Items = new List<OrderItemDetails>() {  new OrderItemDetails()
                                                                { ItemId = itemId,PricePerUnit = pricePerUnit, Quantity = quantity} };

                detailsRepository.SaveOrderDetailsInDB(order);


                return new OrderPlacedResponse() { OrderId = order.OrderID, DeliveryTime = possibleDeliveryDate };
            }
            else return new OrderPlacedResponse();

        }



    }


}
