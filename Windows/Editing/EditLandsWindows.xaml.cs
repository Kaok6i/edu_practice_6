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
using static System.Collections.Specialized.BitVector32;

namespace RealEstateProject.Windows.Editing
{
    /// <summary>
    /// Логика взаимодействия для EditLandsWindows.xaml
    /// </summary>
    public partial class EditLandsWindows : Window
    {
        Entities entity = new Entities();
        Lands land;
        string action;
        Regex regex = new Regex("^[0-9]+");
        public EditLandsWindows(Lands landInfo)
        {
            InitializeComponent();
            if (landInfo != null)
            {
                land = landInfo;
                this.Title = "Eesoft | Агенство недвижимости | Редактирование дома";
                action = "изменена";

            }
            else
            {
                this.Title = "Eesoft | Агенство недвижимости | Добавление дома";
                action = "добавлена";
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
        }
        private void SaveBttn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckTextBoxInput();
                land.CityId = Convert.ToInt32(cityCB.SelectedValue);
                land.StreetId = Convert.ToInt32(streetCB.SelectedValue);
                land.AddressHouse = Convert.ToInt32(addressTB.Text);
                land.AddressNumber = Convert.ToInt32(addressNumberTB.Text);
                land.CoordinateLatitude = Convert.ToInt32(latitudeTB.Text);
                land.CoordinateLongitude = Convert.ToInt32(longitudeTB.Text);
                land.TotalArea = Convert.ToInt32(totalAreaTB.Text);
                entity.Lands.AddOrUpdate(land);
                entity.SaveChanges();
                MessageBox.Show($"Земля успешно {action}", "Уведомление");
                BackBttn_Click(sender, e);
            }
            catch (Exception error)
            {
                MessageBox.Show
               ($"Возникла ошибка при добавлений или изменений квартиры\n{error}"
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cityCB.ItemsSource = entity.Cities.ToList();
            streetCB.ItemsSource = entity.Streets.ToList();
            if (land != null)
            {
                cityCB.SelectedValue = land.CityId;
                streetCB.SelectedValue = land.StreetId;
                addressTB.Text = land.AddressHouse.ToString();
                addressNumberTB.Text = land.AddressNumber.ToString();
                latitudeTB.Text = land.CoordinateLatitude.ToString();
                longitudeTB.Text = land.CoordinateLongitude.ToString();
                totalAreaTB.Text = land.TotalArea.ToString();
            }
            else
            {
                land = new Lands();
                land.Id = entity.Houses.Max(x => x.Id) + 1;
            }
        }
    }
}
