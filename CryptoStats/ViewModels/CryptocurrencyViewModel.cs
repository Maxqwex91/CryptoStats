using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoStats.Models;

namespace CryptoStats.ViewModels
{
    public class CryptocurrencyViewModel : ViewModelBase
    {
        private readonly Cryptocurrency _cryptocurrency;
        public string Name => _cryptocurrency.Name;
    }
}
