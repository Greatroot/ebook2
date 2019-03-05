using System;
using System.Collections.Generic;

namespace ebook2.Models
{
    public partial class Redemption
    {
        public Redemption()
        {
            Issurance = new HashSet<Issurance>();
        }

        public int RedemptionId { get; set; }
        public string Code { get; set; }
        public string BookTitle { get; set; }
        public string Isbn { get; set; }

        public ICollection<Issurance> Issurance { get; set; }
    }
}
