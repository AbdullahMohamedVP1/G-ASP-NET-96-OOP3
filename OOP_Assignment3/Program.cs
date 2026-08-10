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

    public class StandardShipment : Shipment
    {
        public override string ShipmentType => "Standard Shipment";

        // Constructor Chaining - بينادي على constructor الأب
        public StandardShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
        }
        public override void PrintShipment()
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

    public class ExpressShipment : Shipment
    {
        private decimal extraFee;
        public decimal ExtraFee
        {
            get { return extraFee; }
            set
            {
                if (value >= 0)
                    extraFee = value;
            }
        }

        public override string ShipmentType => "Express Shipment";

        public ExpressShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination,
            decimal extraFee)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
            ExtraFee = extraFee;
        }
        public override decimal EstimatedCost
        {
            get { return DeliveryFee + (Weight * 5) + ExtraFee; }
        }

        public override void PrintShipment()
        {
            Console.WriteLine(ShipmentType);
            Console.WriteLine();
            Console.WriteLine($"Tracking Code : {TrackingCode}");
            Console.WriteLine($"Description   : {Description}");
            Console.WriteLine($"Weight        : {Weight} KG");
            Console.WriteLine($"Delivery Fee  : {DeliveryFee} EGP");
            Console.WriteLine($"Extra Fee     : {ExtraFee} EGP");
            Console.WriteLine($"Estimated Cost: {EstimatedCost} EGP");
        }
    }

    public class InternationalShipment : Shipment
    {
        private string destinationCountry;
        public string DestinationCountry
        {
            get { return destinationCountry; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    destinationCountry = value;
            }
        }

        private decimal customsFee;
        public decimal CustomsFee
        {
            get { return customsFee; }
            set
            {
                if (value >= 0)
                    customsFee = value;
            }
        }

        public override string ShipmentType => "International Shipment";
        public InternationalShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination,
            string destinationCountry,
            decimal customsFee)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
            DestinationCountry = destinationCountry;
            CustomsFee = customsFee;
        }
        public virtual string GenerateCustomsReport()
        {
            return $"Customs Report - Country: {DestinationCountry}, Fee: {CustomsFee} EGP";
        }
        public override decimal EstimatedCost
        {
            get { return DeliveryFee + (Weight * 5) + CustomsFee; }
        }

        public override void PrintShipment()
        {
            Console.WriteLine(ShipmentType);
            Console.WriteLine();
            Console.WriteLine($"Tracking Code       : {TrackingCode}");
            Console.WriteLine($"Description         : {Description}");
            Console.WriteLine($"Weight              : {Weight} KG");
            Console.WriteLine($"Delivery Fee        : {DeliveryFee} EGP");
            Console.WriteLine($"Destination Country : {DestinationCountry}");
            Console.WriteLine($"Customs Fee         : {CustomsFee} EGP");
            Console.WriteLine($"Estimated Cost      : {EstimatedCost} EGP");
        }
    }

    public class PriorityInternationalShipment : InternationalShipment
    {
        public PriorityInternationalShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination,
            string destinationCountry,
            decimal customsFee)
            : base(trackingCode, description, weight, deliveryFee, destination, destinationCountry, customsFee)
        {
        }

        // sealed override محدش يقدر يعمل override تاني على الميثود دي في أي كلاس هيرث منها
        public sealed override string GenerateCustomsReport()
        {
            return $"[PRIORITY] {base.GenerateCustomsReport()}";
        }
    }

    public sealed class CompletedShipment : Shipment
    {
        public override string ShipmentType => "Completed Shipment";

        public CompletedShipment(
            string trackingCode,
            string description,
            decimal weight,
            decimal deliveryFee,
            DeliveryAddress destination)
            : base(trackingCode, description, weight, deliveryFee, destination)
        {
        }

        public override void PrintShipment()
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

    public class Driver
    {
        public string DriverId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public Driver(string driverId, string fullName, string phoneNumber)
        {
            DriverId = driverId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }

    internal class DeliveryCenter
    {
        public string CenterName { get; set; }
        public Driver Driver { get; set; }
        private Shipment[] shipments;
        public DeliveryCenter()
        {
            shipments = new Shipment[20];}

        public Shipment this[int index]
        {
            get
            {if (index >= 0 && index < shipments.Length)
                    return shipments[index];
                return default;}
            set
            {
                if (index >= 0 && index < shipments.Length)
                    shipments[index] = value;}
        }
        public Shipment this[string trackingCode]
        {
            get
            {
                for (int i = 0; i < shipments.Length; i++)
                {
                    if (shipments[i] != null && shipments[i].TrackingCode == trackingCode)
                        return shipments[i];
                }
                return default;
            }
        }

        public bool AddShipment(Shipment shipment)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] == null)
                {
                    shipments[i] = shipment;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveShipment(string trackingCode)
        {
            for (int i = 0; i < shipments.Length; i++)
            {
                if (shipments[i] != null && shipments[i].TrackingCode == trackingCode)
                {
                    shipments[i] = null;
                    return true;
                }
            }
            return false;
        }

        public void PrintAllShipments()
        {
            Console.WriteLine(new string('=', 50));
            Console.WriteLine($"Delivery Center : {CenterName}");
            Console.WriteLine(new string('=', 50));

            foreach (Shipment s in shipments)
            {
                if (s != null)
                {
                    Console.WriteLine();
                    s.PrintShipment();
                    Console.WriteLine();
                    Console.WriteLine(new string('-', 50));
                }
            }
        }
    }

    public static class DeliveryHelper
    {
        public static void PrintShipmentDetails(Shipment shipment)
        {
            shipment.PrintShipment();
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

            // a) Create a Driver
            Console.WriteLine("Enter Driver Data:");
            Console.Write("Driver ID: ");
            string driverId = Console.ReadLine();
            Console.Write("Full Name: ");
            string driverName = Console.ReadLine();
            Console.Write("Phone Number: ");
            string driverPhone = Console.ReadLine();
            Driver driver = new Driver(driverId, driverName, driverPhone);

            // b) Create a DeliveryCenter
            Console.Write("\nEnter Delivery Center Name: ");
            string centerName = Console.ReadLine();
            DeliveryCenter center = new DeliveryCenter();
            center.CenterName = centerName;

            // c) Assign the Driver to the DeliveryCenter
            center.Driver = driver;
            Console.WriteLine($"\nDriver Assigned: {center.Driver.FullName}");
            
            DeliveryAddress fixedAddress = new DeliveryAddress("Cairo", "Main St", 1); //علشان ميسألش عنهم كل مره 

            // d) Create one StandardShipment
            Console.WriteLine("\nEnter Standard Shipment Data:");
            Console.Write("Tracking Code: ");
            string standardTrackingCode = Console.ReadLine();
            Console.Write("Description: ");
            string standardDescription = Console.ReadLine();
            Console.Write("Weight: ");
            decimal standardWeight = decimal.Parse(Console.ReadLine());
            Console.Write("Delivery Fee: ");
            decimal standardDeliveryFee = decimal.Parse(Console.ReadLine());
            StandardShipment standard = new StandardShipment(
                standardTrackingCode, standardDescription, standardWeight, standardDeliveryFee, fixedAddress);

            // e) Create one ExpressShipment
            Console.WriteLine("\nEnter Express Shipment Data:");
            Console.Write("Tracking Code: ");
            string expressTrackingCode = Console.ReadLine();
            Console.Write("Description: ");
            string expressDescription = Console.ReadLine();
            Console.Write("Weight: ");
            decimal expressWeight = decimal.Parse(Console.ReadLine());
            Console.Write("Delivery Fee: ");
            decimal expressDeliveryFee = decimal.Parse(Console.ReadLine());
            Console.Write("Extra Fee: ");
            decimal extraFee = decimal.Parse(Console.ReadLine());
            ExpressShipment express = new ExpressShipment(
                expressTrackingCode, expressDescription, expressWeight, expressDeliveryFee, fixedAddress, extraFee);


            // f) Create one InternationalShipment
            Console.WriteLine("\nEnter International Shipment Data:");
            Console.Write("Tracking Code: ");
            string intlTrackingCode = Console.ReadLine();
            Console.Write("Description: ");
            string intlDescription = Console.ReadLine();
            Console.Write("Weight: ");
            decimal intlWeight = decimal.Parse(Console.ReadLine());
            Console.Write("Delivery Fee: ");
            decimal intlDeliveryFee = decimal.Parse(Console.ReadLine());
            Console.Write("Destination Country: ");
            string destinationCountry = Console.ReadLine();
            Console.Write("Customs Fee: ");
            decimal customsFee = decimal.Parse(Console.ReadLine());
            InternationalShipment international = new InternationalShipment(
                intlTrackingCode, intlDescription, intlWeight, intlDeliveryFee, fixedAddress, destinationCountry, customsFee);

            // g) Add all shipments to the DeliveryCenter
            center.AddShipment(standard);
            center.AddShipment(express);
            center.AddShipment(international);

            // h) Print all shipments using PrintAllShipments()
            Console.WriteLine();
            center.PrintAllShipments();

            // i) Call DeliveryHelper.PrintShipmentDetails() for each shipment
            Console.WriteLine("\nPrinting Using DeliveryHelper...\n");
            DeliveryHelper.PrintShipmentDetails(standard);
            Console.WriteLine("Standard Shipment Printed Successfully.\n");
            DeliveryHelper.PrintShipmentDetails(express);
            Console.WriteLine("Express Shipment Printed Successfully.\n");
            DeliveryHelper.PrintShipmentDetails(international);
            Console.WriteLine("International Shipment Printed Successfully.");
            Console.WriteLine(new string('=', 40));

            // j) Demonstrate both versions of UpdateWeight()
            Console.WriteLine("\nUpdating Weight...\n");
            Console.WriteLine($"Original Weight : {standard.Weight} KG");
            Console.Write("Enter new weight: ");
            decimal newWeight = decimal.Parse(Console.ReadLine());
            standard.UpdateWeight(newWeight);
            Console.WriteLine($"Updated Weight : {standard.Weight} KG");
            Console.Write("Enter new weight again: ");
            decimal newWeight2 = decimal.Parse(Console.ReadLine());
            Console.Write("Enter extra packing weight: ");
            decimal extraPacking = decimal.Parse(Console.ReadLine());
            standard.UpdateWeight(newWeight2, extraPacking);
            Console.WriteLine($"Updated Weight After Packing : {standard.Weight} KG");
            Console.WriteLine(new string('=', 40));

            // k) Build a Shipment[] holding mixed types and print all of them in a loop
            Console.WriteLine("\nPrinting Using Shipment[]...\n");
            Shipment[] mixedShipments = { standard, express, international };

            foreach (Shipment s in mixedShipments)
            {
                Console.WriteLine($"{s.ShipmentType}...");
            }
            Console.WriteLine(new string('=', 40));

            
        }
    }
}