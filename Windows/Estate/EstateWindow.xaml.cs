using RealEstateProject.Database;
using RealEstateProject.Windows.Editing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealEstateProject.Windows.Estate
{
    /// <summary>
    /// Логика взаимодействия для EstateWindow.xaml
    /// </summary>
    public partial class EstateWindow : Window
    {
        public EstateWindow()
        {
            InitializeComponent();
        }
        Entities entity = new Entities();
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (typeEstateCB.SelectedIndex != -1)
            {
                switch (typeEstateCB.SelectedIndex)
                {
                    // Квартира
                    case 0:
                        var apartment = new EditApartmentsWindows(null);
                        apartment.Show();
                        break;
                    // Дом
                    case 1:
                        var house = new EditHousesWindows(null);
                        house.Show();
                        break;
                    // Земля
                    case 2:
                        var land = new EditLandsWindows(null);
                        land.Show();
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
                var selected = estateDG.SelectedItem;
                if (selected != null)
                {
                    switch (typeEstateCB.SelectedIndex)
                    {
                        // Квартира
                        case 0:
                            selected = estateDG.SelectedItem as Apartments;
                            var apartment = new EditApartmentsWindows((Apartments)selected);
                            apartment.Show();
                            break;
                        // Дом
                        case 1:
                            selected = estateDG.SelectedItem as Houses;
                            var house = new EditHousesWindows((Houses)selected);
                            house.Show();
                            break;
                        // Земля
                        case 2:
                            selected = estateDG.SelectedItem as Lands;
                            var land = new EditLandsWindows((Lands)selected);
                            land.Show();
                            this.Close();
                            break;
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show
                        ("Вы не выбрали строку для редактирования!", "Уведомление",
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
                    estateDG.ItemsSource = entity.Apartments.ToList();
                    break;
                // Дом
                case 1:
                    estateDG.ItemsSource = entity.Houses.ToList();
                    break;
                // Земля
                case 2:
                    estateDG.ItemsSource = entity.Lands.ToList();
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
                var selected = estateDG.SelectedItem;
                switch (typeEstateCB.SelectedIndex)
                {
                    // Квартира
                    case 0:
                        selected = estateDG.SelectedItem as Apartments;
                        break;
                    // Дом
                    case 1:
                        selected = estateDG.SelectedItem as Houses;
                        break;
                    // Земля
                    case 2:
                        selected = estateDG.SelectedItem as Lands;
                        break;
                }
                if (selected != null)
                {
                    switch (typeEstateCB.SelectedIndex)
                    {
                        case 0:
                            entity.Apartments.Remove((Apartments)selected);
                            break;
                        case 1:
                            entity.Houses.Remove((Houses)selected);
                            break;
                        case 2:
                            entity.Lands.Remove((Lands)selected);
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void typeEstateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            estateDG.Columns.Clear();
            CreateBasicDataGrid();
            switch (typeEstateCB.SelectedIndex)
            {
                case 0:
                    var rooms = new DataGridTextColumn();
                    rooms.Header = "Количество комнат";
                    rooms.Binding = new Binding("Rooms");
                    estateDG.Columns.Add(rooms);
                    var floor = new DataGridTextColumn();
                    floor.Header = "Этаж";
                    floor.Binding = new Binding("Floor");
                    estateDG.Columns.Add(floor);
                    estateDG.ItemsSource = entity.Apartments.ToList();
                    break;
                case 1:
                    var totalFloors = new DataGridTextColumn();
                    totalFloors.Header = "Общее количество этажей";
                    totalFloors.Binding = new Binding("TotalFloors");
                    estateDG.Columns.Add(totalFloors);
                    estateDG.ItemsSource = entity.Houses.ToList();
                    break;
                case 2:
                    estateDG.ItemsSource = entity.Lands.ToList();
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
            estateDG.Columns.Add(textCity);
            var streetCity = new DataGridTextColumn();
            streetCity.Header = "Улица";
            streetCity.Binding = new Binding("StreetName");
            estateDG.Columns.Add(streetCity);
            var address = new DataGridTextColumn();
            address.Header = "Адрес";
            address.Binding = new Binding("AddressHouse");
            estateDG.Columns.Add(address);
            var addressNumber = new DataGridTextColumn();
            addressNumber.Header = "Номер адреса";
            addressNumber.Binding = new Binding("AddressNumber");
            estateDG.Columns.Add(addressNumber);
            var coordinateLatitude = new DataGridTextColumn();
            coordinateLatitude.Header = "Широта";
            coordinateLatitude.Binding = new Binding("CoordinateLatitude");
            estateDG.Columns.Add(coordinateLatitude);
            var coordinateLongitude = new DataGridTextColumn();
            coordinateLongitude.Header = "Долгота";
            coordinateLongitude.Binding = new Binding("CoordinateLongitude");
            estateDG.Columns.Add(coordinateLongitude);
            var totalArea = new DataGridTextColumn();
            totalArea.Header = "Общая площадь";
            totalArea.Binding = new Binding("TotalArea");
            estateDG.Columns.Add(totalArea);
        }
    }
}
