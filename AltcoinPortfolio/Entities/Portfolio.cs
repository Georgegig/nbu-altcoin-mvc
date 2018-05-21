using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltcoinPortfolio.Entities
{
    public class Portfolio
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}