using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltcoinPortfolio.Entities
{
    public class Coin
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public int Rank { get; set; }
        public decimal Price_USD { get; set; }
        public double Amount { get; set; }

        public Guid PortfolioId { get; set; }
        public virtual Portfolio Portfolio { get; set; }
    }
}