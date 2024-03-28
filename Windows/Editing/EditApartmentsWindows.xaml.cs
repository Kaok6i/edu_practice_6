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
    /// Логика взаимодействия для EditApartmentsWindows.xaml
    /// </summary>
    public partial class EditApartmentsWindows : Window
    {
        Entities entity = new Entities();
        Apartments apartment;
        string action;
        Regex regex = new Regex("^[0-9]+");
        public EditApartmentsWindows(Apartments apartmentInfo)
        {
            InitializeComponent();
            if (apartmentInfo != null )
            {
                apartment = apartmentInfo;
                this.Title = "Eesoft | Агенство недвижимости | Редактирование квартиры";
                action = "изменена";
            }
            else
            {
                this.Title = "Eesoft | Агенство недвижимости | Добавление квартиры";
                action = "добавлена";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cityCB.ItemsSource = entity.Cities.ToList();
            streetCB.ItemsSource= entity.Streets.ToList();
            if (apartment != null)
            {
                cityCB.SelectedValue = apartment.CityId;
                streetCB.SelectedValue= apartment.StreetId;
                addressTB.Text = apartment.AddressHouse.ToString();
                addressNumberTB.Text = apartment.AddressNumber.ToString();
                latitudeTB.Text = apartment.CoordinateLatitude.ToString();
                longitudeTB.Text = apartment.CoordinateLongitude.ToString();
                totalAreaTB.Text = apartment.TotalArea.ToString();
                roomsTB.Text = apartment.Rooms.ToString();
                floorTB.Text = apartment.Floor.ToString();
            }
            else
            {
                apartment = new Apartments();
                apartment.Id = entity.Apartments.Max(x => x.Id) + 1;
            }
        }

        private void BackBttn_Click(object sender, RoutedEventArgs e)
        {
            var estate = new EstateWindow();
            estate.Show();
            this.Close();
        }

        private void SaveBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckTextBoxInput();
                apartment.CityId = Convert.ToInt32(cityCB.SelectedValue);
                apartment.StreetId = Convert.ToInt32(streetCB.SelectedValue);
                apartment.AddressHouse = Convert.ToInt32(addressTB.Text);
                apartment.AddressNumber = Convert.ToInt32(addressNumberTB.Text);
                apartment.CoordinateLatitude = Convert.ToInt32(latitudeTB.Text);
                apartment.CoordinateLongitude = Convert.ToInt32(longitudeTB.Text);
                apartment.TotalArea = Convert.ToInt32(totalAreaTB.Text);
                apartment.Rooms = Convert.ToInt32(roomsTB.Text);
                apartment.Floor = Convert.ToInt32(floorTB.Text);
                entity.Apartments.AddOrUpdate(apartment);
                entity.SaveChanges();
                MessageBox.Show($"Квартира успешно {action}", "Уведомление");
                BackBttn_Click(sender,e);
            }
            catch (Exception error)
            {
                MessageBox.Show
               ($"Возникла ошибка при добавлений или изменений квартиры\n{error}"
                    , "Ошибка");
            }
        }
        private void NumberFilter_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !regex.IsMatch(e.Text);
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
            if (roomsTB.Text.Length <= 0 && roomsTB.Text == "")
            {
                MessageBox.Show("Вы не вписали количество комнат!");
                return;
            }
            if (floorTB.Text.Length <= 0 && floorTB.Text == "")
            {
                MessageBox.Show("Вы не вписали этаж!");
                return;
            }
        }
    }
}
