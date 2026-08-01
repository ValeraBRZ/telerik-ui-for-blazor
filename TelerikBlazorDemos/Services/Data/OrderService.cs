using System;
using System.Collections.Generic;

namespace TelerikBlazorDemos.Services.Data
{
    public class OrderService
    {
        private List<OrderDto> Data { get; set; }

        public OrderService()
        {
            List<OrderDto> data = new();

            Random rnd = Random.Shared;

            for (int i = 1; i <= 1000; i++)
            {
                data.Add(new OrderDto()
                {
                    Id = i,
                    Account = Accounts[rnd.Next(0, Accounts.Length)],
                    Bookings = rnd.Next(5_000, 500_000) * 1.23m,
                    Discount = rnd.Next(0, 50) * 0.01m,
                    Industry = Industries[rnd.Next(0, Industries.Length)],
                    LicenseType = LicenseTypes[rnd.Next(0, LicenseTypes.Length)],
                    OrderDate = DateTime.Now.AddDays(-rnd.Next(0, 365)).AddMinutes(rnd.Next(0, 60 * 24)),
                    OrderType = OrderTypes[rnd.Next(0, OrderTypes.Length)],
                    Phone = $"+1-{rnd.Next(100, 1000)}-{rnd.Next(100, 1000)}-{rnd.Next(1000, 10000)}",
                    Product = Products[rnd.Next(0, Products.Length)],
                    Rating = rnd.Next(1, 6),
                    Region = Regions[rnd.Next(0, Regions.Length)],
                    SalesPerson = SalesPeople[rnd.Next(0, SalesPeople.Length)],
                    Status = Statuses[rnd.Next(0, Statuses.Length)]
                });
            }

            Data = data;
        }

        public List<OrderDto> GetData(int count = 1000)
        {
            return Data;
        }

        private readonly string[] Accounts =
        {
            "Acme Corporation",
            "TechStart Inc",
            "Global Solutions Ltd",
            "Innovation Labs",
            "DataCore Systems",
            "CloudFirst Technologies",
            "Enterprise Software Co",
            "Digital Dynamics",
            "Quantum Systems",
            "NextGen Industries",
            "Alpha Technologies",
            "Beta Enterprises",
            "Gamma Solutions",
            "Delta Innovations",
            "Epsilon Corp"
        };

        private readonly string[] Industries =
        {
            "Technology",
            "Healthcare",
            "Finance",
            "Retail",
            "Manufacturing",
            "Education",
            "Government",
            "Entertainment"
        };

        private readonly string[] LicenseTypes =
        {
            "Perpetual",
            "Subscription"
        };

        private readonly string[] OrderTypes =
        {
            "New",
            "Existing",
            "Renewal"
        };

        private readonly string[] Products =
        {
            "Enterprise Suite",
            "Professional Edition",
            "Standard Package",
            "Premium Bundle",
            "Developer Tools",
            "Analytics Platform",
            "Cloud Services",
            "Mobile Solutions"
        };

        private readonly string[] Regions =
        {
            "Africa",
            "Asia Pacific",
            "Europe",
            "Latin America",
            "Middle East",
            "North America"
        };

        private readonly string[] SalesPeople =
        {
            "Sarah Johnson",
            "Michael Chen",
            "Emma Williams",
            "James Rodriguez",
            "Olivia Martinez",
            "David Kim",
            "Sophia Patel",
            "Daniel Brown",
            "Ava Garcia",
            "Matthew Lee"
        };

        private readonly string[] Statuses =
        {
            "Active",
            "Complete",
            "In Review",
            "Pending"
        };
    }

    public class OrderDto
    {
        public int Id { get; set; }

        public string Account { get; set; } = string.Empty;

        public decimal Bookings { get; set; }

        public decimal Discount { get; set; }

        public string Industry { get; set; } = string.Empty;

        public string LicenseType { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public string OrderType { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Product { get; set; } = string.Empty;

        public int Rating { get; set; }

        public string Region { get; set; } = string.Empty;

        public string SalesPerson { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
