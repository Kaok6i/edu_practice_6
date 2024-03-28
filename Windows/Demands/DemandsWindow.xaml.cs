using RealEstateProject.Database;
using RealEstateProject.Windows.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealEstateProject.Windows.Demands
{
    /// <summary>
    /// Логика взаимодействия для DemandsWindow.xaml
    /// </summary>
    public partial class DemandsWindow : Window
    {
        public DemandsWindow()
        {
            InitializeComponent();
        }
        Entities entity = new Entities();
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (typeEstateCB.SelectedIndex != -1)
            {
                switch (typeEstateCB.SelectedIndex)
                {
                    // Квартира
                    case 0:
                        var apartmentDemants = new EditApartmentDemantsWindows(null,null, null,0);
                        apartmentDemants.Show();
                        break;
                    // Дом
                    case 1:
                        var houseDemants = new EditApartmentDemantsWindows(null, null, null,1);
                        houseDemants.Show();
                        break;
                    // Земля
                    case 2:
                        var landDemants = new EditApartmentDemantsWindows(null, null, null,2);
                        landDemants.Show();
                        this.Close();
                        break;
                }
                this.Close();
            }
            else
            {
                MessageBox.Show
                    ("Вы не выбрали тип недвижимости для добавления!",
                    "Уведомление");
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (typeEstateCB.SelectedIndex != -1)
            {
                var selected = demandsDG.SelectedItem;
                if (selected != null)
                {
                    switch (typeEstateCB.SelectedIndex)
                    {
                        // Квартира
                        case 0:
                            selected = demandsDG.SelectedItem as ApartmentDemands;
                            var apartment = new EditApartmentDemantsWindows((ApartmentDemands)selected, null,null,0);
                            apartment.Show();
                            break;
                        // Дом
                        case 1:
                            selected = demandsDG.SelectedItem as HouseDemands;
                            var house = new EditApartmentDemantsWindows(null,(HouseDemands)selected,null,1);
                            house.Show();
                            break;
                        // Земля
                        case 2:
                            selected = demandsDG.SelectedItem as LandDemands;
                            var land = new EditApartmentDemantsWindows(null,null,(LandDemands)selected,2);
                            land.Show();
                            this.Close();
                            break;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show
                        ("Вы не выбрали строку для удаления!", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Вы не выбрали тип недвижимости для редактирования!", "Уведомление");
            }
        }
        /// <summary>
        /// Обновление полей в DataGrid
        /// </summary>
        private void RefreshTable()
        {
            switch (typeEstateCB.SelectedIndex)
            {
                // Квартира
                case 0:
                    demandsDG.ItemsSource = entity.ApartmentDemands.ToList();
                    break;
                // Дом
                case 1:
                    demandsDG.ItemsSource = entity.HouseDemands.ToList();
                    break;
                // Земля
                case 2:
                    demandsDG.ItemsSource = entity.LandDemands.ToList();
                    break;
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var message = MessageBox.Show
                ("Вы точно хотите удалить данные?", "Уведомление",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (message.Equals(MessageBoxResult.Yes))
            {
                var selected = demandsDG.SelectedItem;
                switch (typeEstateCB.SelectedIndex)
                {
                    // Квартира
                    case 0:
                        selected = demandsDG.SelectedItem as ApartmentDemands;
                        break;
                    // Дом
                    case 1:
                        selected = demandsDG.SelectedItem as HouseDemands;
                        break;
                    // Земля
                    case 2:
                        selected = demandsDG.SelectedItem as LandDemands;
                        break;
                }
                if (selected != null)
                {
                    switch (typeEstateCB.SelectedIndex)
                    {
                        case 0:
                            entity.ApartmentDemands.Remove((ApartmentDemands)selected);
                            break;
                        case 1:
                            entity.HouseDemands.Remove((HouseDemands)selected);
                            break;
                        case 2:
                            entity.LandDemands.Remove((LandDemands)selected);
                            break;
                    }
                    entity.SaveChanges();
                    MessageBox.Show
                        ("Данные были успешно удалены", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshTable();
                }
                else
                {
                    MessageBox.Show
                        ("Вы не выбрали строку для удаления!", "Уведомление",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void typeEstateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            demandsDG.Columns.Clear();
            CreateBasicDataGrid();
            switch (typeEstateCB.SelectedIndex)
            {
                case 0:
                    demandsDG.ItemsSource = entity.ApartmentDemands.ToList();
                    break;
                case 1:
                    demandsDG.ItemsSource = entity.HouseDemands.ToList();
                    break;
                case 2:
                    demandsDG.ItemsSource = entity.LandDemands.ToList();
                    break;
            }
        }
        /// <summary>
        /// Создание общих полей для отображения в DataGrid
        /// </summary>
        private void CreateBasicDataGrid()
        {
            var textCity = new DataGridTextColumn();
            textCity.Header = "Город";
            textCity.Binding = new Binding("CityName");
            demandsDG.Columns.Add(textCity);
            var streetCity = new DataGridTextColumn();
            streetCity.Header = "Улица";
            streetCity.Binding = new Binding("StreetName");
            demandsDG.Columns.Add(streetCity);
            var address = new DataGridTextColumn();
            address.Header = "Адрес";
            address.Binding = new Binding("AddressHouse");
            demandsDG.Columns.Add(address);
            var addressNumber = new DataGridTextColumn();
            addressNumber.Header = "Номер адреса";
            addressNumber.Binding = new Binding("AddressNumber");
            demandsDG.Columns.Add(addressNumber);
            var minPrice = new DataGridTextColumn();
            minPrice.Header = "Минимальная цена";
            minPrice.Binding = new Binding("MinPrice");
            demandsDG.Columns.Add(minPrice);
            var maxPrice = new DataGridTextColumn();
            maxPrice.Header = "Максимальная цена";
            maxPrice.Binding = new Binding("MaxPrice");
            demandsDG.Columns.Add(maxPrice);
            var agentId = new DataGridTextColumn();
            agentId.Header = "Id агента";
            agentId.Binding = new Binding("AgentId");
            demandsDG.Columns.Add(agentId);
            var clientId = new DataGridTextColumn();
            clientId.Header = "Id клиента";
            clientId.Binding = new Binding("ClientId");
            demandsDG.Columns.Add(clientId);
            var minArea = new DataGridTextColumn();
            minArea.Header = "Минимальная область";
            minArea.Binding = new Binding("MinArea");
            demandsDG.Columns.Add(minArea);
            var maxArea = new DataGridTextColumn();
            maxArea.Header = "Максимальная область";
            maxArea.Binding = new Binding("MaxArea");
            demandsDG.Columns.Add(maxArea);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }    
}
