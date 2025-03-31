using Mysqlx.Connection;
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
using MySql.Data.MySqlClient;

namespace Mysql_login_reg_11A {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        // 1. kapcsolati string
        MySqlConnection kapcs = new MySqlConnection("server = localhost;database = asztali_11a;uid = root;password = '';");
        public MainWindow() {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // kapcsolódunk az adatbázishoz
            kapcs.Open();
            var sql = $"SELECT * FROM user WHERE nev = '{txtNev.Text}' AND jelszo = '{txtJelszo.Text}'";
            lbDebug.Content = sql;
            var parancs = new MySqlCommand(sql, kapcs);
            var reader = parancs.ExecuteReader();
            if (reader.Read()) {
                MessageBox.Show("Sikeres bejelentkezés!");
            }
            else {
                MessageBox.Show("Sikertelen bejelentkezés!");
            }
            kapcs.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            if (txtRegJelszo1.Password == txtRegJelszo2.Password) {
                MessageBox.Show("A két jelszó megegyezik!");
            }
        }
    }
}
