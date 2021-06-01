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

using System.Data.SqlClient;
using BolshayaPachka.Classes;
using BolshayaPachka.Use_conrols;
using System.IO;

namespace BolshayaPachka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        internal void Load_data(string a)
        {
           
            using (SqlConnection connection = new SqlConnection(connect.String))
            {
                connection.Open(); // открытие соединения с БД
                SqlCommand command = new SqlCommand($@"
                                                       SELECT [ID]
                                                       ,[Title]
                                                       ,[CountInPack]
                                                       ,[Unit]
                                                       ,[CountInStock]
                                                       ,[MinCount]
                                                       ,[Description]
                                                       ,[Cost]
                                                       ,[Image]
                                                       ,[MaterialTypeID]
                                                            FROM [dbo].[Material]
                                                         " + a, connection);
                                                           
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) //проверка наличия строк
                {
                    
                    while (reader.Read()) //цикл для чтения строк и перенос данных 
                    {
                        UserControl material = new UserControl();
                        UserControl.ID.Content = reader[0];
                        UserControl.Title.Content = reader[1];
                        UserControl.CountInPack.Content = reader[2];
                        UserControl.Unit.Content = reader[3];
                        UserControl.CountInStock.Content = reader[4];
                        UserControl.MinCount.Content = reader[5];
                        UserControl.Description.Content = reader[6];
                        UserControl.Cost.Content = reader[7];
                        UserControl.Image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[8]));
                        UserControl.MaterialTypeID_and_Title.Content = $"{reader[9]} | {reader[10]}";

                    }
                      
                }
            }
        }
    }


}
