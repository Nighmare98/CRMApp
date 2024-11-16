using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CRMApp
{
    public partial class TaskWindow : Window
    {
        private ObservableCollection<Task> tasks;
        private const string DataFilePath = "tasks.json";

        public TaskWindow()
        {
            InitializeComponent();
            LoadTasks();
            DataContext = tasks;
            TaskListView.ItemsSource = tasks;
        }

        private void LoadTasks()
        {
            if (File.Exists(DataFilePath))
            {
                string json = File.ReadAllText(DataFilePath);
                tasks = JsonConvert.DeserializeObject<ObservableCollection<Task>>(json);
            }
            else
            {
                tasks = new ObservableCollection<Task>
                {
                    new Task { Title = "Задача 1", Description = "Описание задачи 1", Status = "Новая" },
                    new Task { Title = "Задача 2", Description = "Описание задачи 2", Status = "В работе" }
                };
            }
        }

        private void SaveTasks()
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(DataFilePath, json);
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var newTask = new Task { Title = "Новая задача", Description = "", Status = "Новая" };
            tasks.Add(newTask);
            TaskListView.SelectedItem = newTask;
            TaskListView.ScrollIntoView(newTask);
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                selectedTask.Title = TitleTextBox.Text;
                selectedTask.Description = DescriptionTextBox.Text;
                selectedTask.Status = StatusComboBox.Text;
                TaskListView.Items.Refresh();
            }
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                tasks.Remove(selectedTask);
            }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void TaskListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TaskListView.SelectedItem is Task selectedTask)
            {
                TitleTextBox.Text = selectedTask.Title;
                DescriptionTextBox.Text = selectedTask.Description;
                StatusComboBox.Text = selectedTask.Status;
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = SearchTextBox.Text.ToLower();
            var filteredTasks = tasks.Where(t => t.Title.ToLower().Contains(searchText) || t.Description.ToLower().Contains(searchText) || t.Status.ToLower().Contains(searchText)).ToList();
            TaskListView.ItemsSource = filteredTasks;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            SaveTasks();
        }
    }
}
