using System.Data.Entity;

namespace PhoneBook.Models
{
    public class PBContext : DbContext
    {
        public PBContext() : base("DefaultConnection") { }

        public virtual DbSet<Subscriber> Subscribers { get; set; }
    }
}