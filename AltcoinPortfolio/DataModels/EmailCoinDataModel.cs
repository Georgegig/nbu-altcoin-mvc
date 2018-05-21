using AltcoinPortfolio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AltcoinPortfolio.DataModels
{
    public class EmailCoinDataModel
    {
        public string UserEmail { get; set; }
        public Coin Coin { get; set; }
    }
}