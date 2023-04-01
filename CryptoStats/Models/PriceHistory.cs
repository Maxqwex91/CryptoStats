using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoStats.Models
{
    public class PriceHistory
    {
        public double PriceUsd { get; private set; }
        public DateTime Time { get; private set; }

        public PriceHistory(double priceUsd, DateTime time)
        {
            PriceUsd = priceUsd;
            Time = time;
        }
    }
}
