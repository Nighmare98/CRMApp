using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CRMApp
{
    public partial class ClientWindow : Window
    {
        private ObservableCollection<Client> clients;
        private const string DataFilePath = "clients.json";

        public ClientWindow()
        {
            InitializeComponent();
            LoadClients();
            DataContext = clients;
            ClientListView.ItemsSource = clients;
        }

        private void LoadClients()
        {
            if (File.Exists(DataFilePath))
            {
                string json = File.ReadAllText(DataFilePath);
                clients = JsonConvert.DeserializeObject<ObservableCollection<Client>>(json);
            }
            else
            {
                clients = new ObservableCollection<Client>
                {
                    new Client { Name = "Иван Иванов", Phone = "+79991234567" },
                    new Client { Name = "Петр Петров", Phone = "+79997654321" }
                };
            }
        }

        private void SaveClients()
        {
            string json = JsonConvert.SerializeObject(clients, Formatting.Indented);
            File.WriteAllText(DataFilePath, json);
        }

        private void AddClientButton_Click(object sender, RoutedEventArgs e)
        {
            var newClient = new Client { Name = "Новый клиент", Phone = "" };
            clients.Add(newClient);
            ClientListView.SelectedItem = newClient;
            ClientListView.ScrollIntoView(newClient);
        }

        private void EditClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientListView.SelectedItem is Client selectedClient)
            {
                selectedClient.Name = NameTextBox.Text;
                selectedClient.Phone = PhoneTextBox.Text;
                ClientListView.Items.Refresh();
            }
        }

        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientListView.SelectedItem is Client selectedClient)
            {
                clients.Remove(selectedClient);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientListView.SelectedItem is Client selectedClient)
            {
                NameTextBox.Text = selectedClient.Name;
                PhoneTextBox.Text = selectedClient.Phone;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();
            var filteredClients = clients.Where(c => c.Name.ToLower().Contains(searchText) || c.Phone.ToLower().Contains(searchText)).ToList();
            ClientListView.ItemsSource = filteredClients;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveClients();
        }
    }
}
