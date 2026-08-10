namespace OOP_Assignment3
{
    public struct DeliveryAddress
    {
        public string City;
        public string Street;
        public int BuildingNumber;

        public DeliveryAddress(string city, string street, int buldingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buldingNumber;
        }
        public string GetFullAddress()
        {
            return $"city: {City}, street: {Street}, building: {BuildingNumber}";
        }
    }

    public class Shipment
    {
        private string trackingCode;
        private string description;
        private decimal weight;
        private decimal deliveryFee;

        public DeliveryAddress Destination { get; set; }

        public string TrackingCode
        {
            get { return trackingCode; }
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    trackingCode = value;
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    description = value;
            }
        }

        public decimal Weight
        {
            get { return weight; }
            set
            {
                if (value > 0)
                    weight = value;
            }
        }

        public decimal DeliveryFee
        {
            get { return deliveryFee; }
            private set
            {
                if (value > 0) { deliveryFee = value; }
            }
        }

        public virtual decimal EstimatedCost
        {
            get { return DeliveryFee + (Weight * 5); }
        }
        public virtual string ShipmentType => "Shipment";

        public Shipment(string trackingCode)
        {
            this.trackingCode = trackingCode;
            description = "Unknown";
            weight = 1;
            deliveryFee = 50;

            Destination = new DeliveryAddress("Cairo", "Unknown", 0);
        }

        public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee, DeliveryAddress destination)
        {
            this.trackingCode = trackingCode;
            this.description = description;
            this.weight = weight;
            this.deliveryFee = deliveryFee;
            Destination = destination;
        }

        public void UpdateDeliveryFee(decimal newFee)
        {
            if (newFee > 0)
            {
                DeliveryFee = newFee;
            }
        }

        // ==== الإضافة الجديدة بتاعة Part 02 ====

        // 1) updates the shipment weight
        public void UpdateWeight(decimal newWeight)
        {
            Weight = newWeight;
        }

        // 2) updates the shipment weight after adding extra packing weight
        public void UpdateWeight(decimal newWeight, decimal extraPackingWeight)
        {
            Weight = newWeight + extraPackingWeight;
        }

        public virtual void PrintShipment()
        {
            Console.WriteLine(ShipmentType);
            Console.WriteLine();
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description   : {Description}");
            Console.WriteLine($"Weight        : {Weight} KG");
            Console.WriteLine($"Delivery Fee  : {DeliveryFee} EGP");
            Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Question1
            //a)  What is the difference between Method Overloading and Method Overriding?

            //sol :   overloading it means having one orm ore methods with same name but in differnent parameters or different data type
            // overriding it means having one method that already exist in in the parent class inside the child class and it happens when there's inheritance

            //b)  What is the difference between Static Binding and Dynamic Binding?

            //sol : Binding>> 1  static : the method that will be called is decided at compile time
            // Binding>> 2  dynamic : the method that will be called is decided at run time and it happens with overriding
            #endregion

            #region Question2
            //a) What is the purpose of the sealed keyword when applied to a class?
            //sol : Sealed keyword when used on class means no other class can inherit from it

            //b) What is the difference between a sealed class and a sealed method?
            // Sealed class: stops any class from inheriting from it
            // Sealed method: prevents a method from being overridden again in any
            // child classes but the class itself can still be inherited

            //c) Can a sealed method be overridden? Why?
            // No because sealing the method means we stop it from being overridden
            // again in any child class so it stays the same everywhere
            #endregion

        }
    }
}
