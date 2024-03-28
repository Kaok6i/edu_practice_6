using RealEstateProject.Database;
using RealEstateProject.Windows.Agent;
using RealEstateProject.Windows.Client;
using RealEstateProject.Windows.Demands;
using RealEstateProject.Windows.Estate;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RealEstateProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entity = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = new ClientWindow();
            client.Show();
            this.Close();
        }

        private void AgentBtn_Click(object sender, RoutedEventArgs e)
        {
            var agent = new AgentWindow();
            agent.Show();
            this.Close();
        }

        private void OfferBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Не реализованный функционал\nЖдите обновления программы!","Уведомление | Нет доступа");
        }

        private void DemandsBtn_Click(object sender, RoutedEventArgs e)
        {
            var demands = new DemandsWindow();
            demands.Show();
            this.Close();
        }

        private void EstateBtn_Click(object sender, RoutedEventArgs e)
        {
            var estate = new EstateWindow();
            estate.Show();
            this.Close();
        }
    }
}
