using System;
using System.Collections.Generic;

namespace ebook2.Models
{
    public partial class Issurance
    {
        public int IssuranceId { get; set; }
        public int? StudentId { get; set; }
        public int? RedemptionId { get; set; }

        public Redemption Redemption { get; set; }
        public Student Student { get; set; }
    }
}
