using RealEstateProject.Database;
using RealEstateProject.Windows.Estate;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RealEstateProject.Windows.Editing
{
    /// <summary>
    /// Логика взаимодействия для EditHousesWindows.xaml
    /// </summary>
    public partial class EditHousesWindows : Window
    {
        Entities entity = new Entities();
        Houses house;
        string action;
        Regex regex = new Regex("^[0-9]+");
        public EditHousesWindows(Houses houseInfo)
        {
            InitializeComponent();
            if (houseInfo != null)
            {
                house = houseInfo;
                this.Title = "Eesoft | Агенство недвижимости | Редактирование дома";
                action = "изменена";
                
            }
            else
            {
                this.Title = "Eesoft | Агенство недвижимости | Добавление дома";
                action = "добавлена";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cityCB.ItemsSource = entity.Cities.ToList();
            streetCB.ItemsSource = entity.Streets.ToList();
            if (house != null)
            {
                cityCB.SelectedValue = house.CityId;
                streetCB.SelectedValue = house.StreetId;
                addressTB.Text = house.AddressHouse.ToString();
                addressNumberTB.Text = house.AddressNumber.ToString();
                latitudeTB.Text = house.CoordinateLatitude.ToString();
                longitudeTB.Text = house.CoordinateLongitude.ToString();
                totalAreaTB.Text = house.TotalArea.ToString();
                totalFloorsTB.Text = house.TotalFloors.ToString();
            }
            else
            {
                house = new Houses();
                house.Id = entity.Houses.Max(x => x.Id) + 1;
            }
        }
        /// <summary>
        /// Проверка, введенных данных для сохранения или изменения данных
        /// </summary>
        private void CheckTextBoxInput()
        {
            if (cityCB.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали город!");
                return;
            }
            if (streetCB.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали улицу!");
                return;
            }
            if (addressTB.Text.Length <= 0 && addressTB.Text == "")
            {
                MessageBox.Show("Вы не вписали адрес!");
                return;
            }
            if (addressNumberTB.Text.Length <= 0 && addressNumberTB.Text == "")
            {
                MessageBox.Show("Вы не вписали номер адреса!");
                return;
            }
            if (latitudeTB.Text.Length <= 0 && latitudeTB.Text == "")
            {
                MessageBox.Show("Вы не вписали широту!");
                return;
            }
            if (longitudeTB.Text.Length <= 0 && longitudeTB.Text == "")
            {
                MessageBox.Show("Вы не вписали долготу!");
                return;
            }
            if (totalAreaTB.Text.Length <= 0 && totalAreaTB.Text == "")
            {
                MessageBox.Show("Вы не вписали общую площадь!");
                return;
            }
            if (totalFloorsTB.Text.Length <= 0 && totalFloorsTB.Text == "")
            {
                MessageBox.Show("Вы не вписали общее количество этажей!");
                return;
            }
        }
        private void SaveBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckTextBoxInput();
                house.CityId = Convert.ToInt32(cityCB.SelectedValue);
                house.StreetId = Convert.ToInt32(streetCB.SelectedValue);
                house.AddressHouse = Convert.ToInt32(addressTB.Text);
                house.AddressNumber = Convert.ToInt32(addressNumberTB.Text);
                house.CoordinateLatitude = Convert.ToInt32(latitudeTB.Text);
                house.CoordinateLongitude = Convert.ToInt32(longitudeTB.Text);
                house.TotalArea = Convert.ToInt32(totalAreaTB.Text);
                house.TotalFloors = Convert.ToInt32(totalFloorsTB.Text);
                entity.Houses.AddOrUpdate(house);
                entity.SaveChanges();
                MessageBox.Show($"Дом успешно {action}", "Уведомление");
                BackBttn_Click(sender, e);
            }
            catch (Exception error)
            {
                MessageBox.Show
               ($"Возникла ошибка при добавлений или изменений дома\n{error}"
                    , "Ошибка");
            }
        }

        private void BackBttn_Click(object sender, RoutedEventArgs e)
        {
            var estate = new EstateWindow();
            estate.Show();
            this.Close();
        }
        private void NumberFilter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regex.IsMatch(e.Text);
        }
    }
}
