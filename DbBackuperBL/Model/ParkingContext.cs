using System.Data.Entity;

namespace DbBackuperBL.Model
{
    public class ParkingContext : DbContext
    {
        public ParkingContext() : base("ParkingConnection") { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Car> Cars { get; set; }
    }
}
