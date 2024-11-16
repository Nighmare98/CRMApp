using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CRMApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Client> Clients { get; set; }

        public MainViewModel()
        {
            Clients = new ObservableCollection<Client>
            {
                new Client { Name = "Иван Иванов", Phone = "+79991234567" },
                new Client { Name = "Петр Петров", Phone = "+79997654321" }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
