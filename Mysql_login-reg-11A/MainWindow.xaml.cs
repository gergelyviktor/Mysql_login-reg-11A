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
        MySqlConnection kapcs = new MySqlConnection("server = server.fh2.hu;database = v2labgwj_11a; uid = v2labgwj_11a; password = 'VGFR2GJjqudMt8Q4SA5j'");
        public MainWindow() {
            InitializeComponent();

            // listázzuk ki a felhasználókat
            kapcs.Open();

            var reader = new MySqlCommand($"SELECT * FROM gergelyv_user", kapcs).ExecuteReader();
            while (reader.Read()) {
                lbUserek.Items.Add(reader["nev"].ToString());
                // vagy reader.GetString(1);
            }
            
            kapcs.Close();

        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            // kapcsolódunk az adatbázishoz
            kapcs.Open();
            //var sql = $"SELECT * FROM gergely_user WHERE nev = '{txtNev.Text}' AND jelszo = '{txtJelszo.Text}'";
            //lbDebug.Content = sql;
            //var parancs = new MySqlCommand(sql, kapcs);
            //var reader = parancs.ExecuteReader();
            var reader = new MySqlCommand($"SELECT * FROM gergelyv_user WHERE nev = '{txtNev.Text}' AND jelszo = '{txtJelszo.Text}'", kapcs).ExecuteReader();
            if (reader.Read()) {
                MessageBox.Show("Sikeres bejelentkezés!");
            }
            else {
                MessageBox.Show("Sikertelen bejelentkezés!");
            }
            reader.Close();
            kapcs.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            // 1. ha a két jelszó nem egyezik meg, nem lehet regisztrálni
            if (txtRegJelszo1.Password != txtRegJelszo2.Password) {
                MessageBox.Show("A két jelszó nem egyezik meg!");
                return;
            }
            kapcs.Open();
            // 2. ha a felhasználónév már létezik, nem lehet regisztrálni
            var reader = new MySqlCommand($"SELECT * FROM gergelyv_user WHERE nev = '{txtRegnev.Text}'", kapcs).ExecuteReader();
            if (reader.Read()) {
                MessageBox.Show("Ez a felhasználónév már létezik!");
            }
            else {
                reader.Close();
                // nincs ilyen felhasználónév, lehet regisztrálni
                var sql = $"INSERT INTO gergelyv_user (nev, jelszo) VALUES ('{txtRegnev.Text}', '{txtRegJelszo1.Password}')";
                lbDebug.Content = sql;
                new MySqlCommand(sql, kapcs).ExecuteNonQuery();
                MessageBox.Show("Sikeres regisztráció!");
            }
            kapcs.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) {
            // módosítás itt lesz

        }

        private void lbUserek_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            txtRegiJelszo.Text = lbUserek.SelectedItem.ToString();
        }
    }
}
