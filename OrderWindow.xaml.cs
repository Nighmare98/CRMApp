using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CRMApp
{
    public partial class OrderWindow : Window
    {
        private ObservableCollection<Order> orders;
        private const string DataFilePath = "orders.json";

        public OrderWindow()
        {
            InitializeComponent();
            LoadOrders();
            DataContext = orders;
            OrderListView.ItemsSource = orders;
        }

        private void LoadOrders()
        {
            if (File.Exists(DataFilePath))
            {
                string json = File.ReadAllText(DataFilePath);
                orders = JsonConvert.DeserializeObject<ObservableCollection<Order>>(json);
            }
            else
            {
                orders = new ObservableCollection<Order>
                {
                    new Order { ClientName = "Иван Иванов", ServiceName = "Услуга 1", Status = "Новый" },
                    new Order { ClientName = "Петр Петров", ServiceName = "Услуга 2", Status = "В обработке" }
                };
            }
        }

        private void SaveOrders()
        {
            string json = JsonConvert.SerializeObject(orders, Formatting.Indented);
            File.WriteAllText(DataFilePath, json);
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var newOrder = new Order { ClientName = "Новый клиент", ServiceName = "Новая услуга", Status = "Новый" };
            orders.Add(newOrder);
            OrderListView.SelectedItem = newOrder;
            OrderListView.ScrollIntoView(newOrder);
        }

        private void EditOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListView.SelectedItem is Order selectedOrder)
            {
                selectedOrder.ClientName = ClientNameTextBox.Text;
                selectedOrder.ServiceName = ServiceNameTextBox.Text;
                selectedOrder.Status = StatusComboBox.Text;
                OrderListView.Items.Refresh();
            }
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (OrderListView.SelectedItem is Order selectedOrder)
            {
                orders.Remove(selectedOrder);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OrderListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OrderListView.SelectedItem is Order selectedOrder)
            {
                ClientNameTextBox.Text = selectedOrder.ClientName;
                ServiceNameTextBox.Text = selectedOrder.ServiceName;
                StatusComboBox.Text = selectedOrder.Status;
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();
            var filteredOrders = orders.Where(o => o.ClientName.ToLower().Contains(searchText) || o.ServiceName.ToLower().Contains(searchText) || o.Status.ToLower().Contains(searchText)).ToList();
            OrderListView.ItemsSource = filteredOrders;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveOrders();
        }
    }
}
