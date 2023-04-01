using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoStats.Models
{
    public class ResponceCoinCapData
    {
        public List<Cryptocurrency> Data { get; set; }
        public long Timestamp { get; set; }
    }
}
