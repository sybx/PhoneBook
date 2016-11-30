using System.Collections.Generic;

namespace PhoneBook.Models
{
    public class PBFilterModel
    {
        public string PhoneNumber { get; set; }
        public string Name { get; set; }

        public IEnumerable<Subscriber> Subscribers { get; set; }
    }
}