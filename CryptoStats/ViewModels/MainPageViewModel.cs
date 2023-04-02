using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using CryptoStats.Models;
using CryptoStats.Services;

namespace CryptoStats.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<string> MyItems { get; set; }
        public ObservableCollection<Cryptocurrency> CryptocurrenciesCollection { get; set; }
        public MainPageViewModel()
        {
            MyItems = new ObservableCollection<string>();
            CryptocurrenciesCollection = new ObservableCollection<Cryptocurrency>();
            CryptocurrenciesCollection.CollectionChanged += CryptocurrenciesCollectionChanged;
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Start();
        }

        async void timer_Tick(object? sender, EventArgs e)
        {
            var gridData = await GetDataGridInfo();
            CryptocurrenciesCollection.Clear();
            foreach (var element in gridData)
            {
                CryptocurrenciesCollection.Add(element);
            }
        }

        public async Task<IList<Cryptocurrency>> GetDataGridInfo()
        {
            var coincapApiService = new CoincapApiService();
            var collection = await coincapApiService.RequestCryptocurrenciesAsync();
            var dataGridInfo = (collection ?? throw new InvalidOperationException()).Take(10).ToList();
            return dataGridInfo;
        }
        void CryptocurrenciesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs? e)
        {
            MyItems.Clear();
            List<string> temp = new List<string>();
            foreach (Cryptocurrency cryptocurrency in CryptocurrenciesCollection)
            {
                temp.Add(cryptocurrency.Name);
                temp.Add(cryptocurrency.Symbol);
            }
            foreach (string s in temp.Distinct())
            {
                MyItems.Add(s);
            }
            OnPropertyChanged(nameof(MyItems));
            OnPropertyChanged(nameof(CryptocurrenciesCollection));
        }
        private string? _mySelectedItem;
        public string? MySelectedItem
        {
            get => _mySelectedItem;
            set
            {
                if (value != _mySelectedItem)
                {
                    _mySelectedItem = value;
                    OnPropertyChanged("MySelectedItem");
                }
            }
        }
        
    }
}
