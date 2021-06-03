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
using System.IO;
using System.Data.SqlClient;
using Lopushok1.Class;
using Lopushok1.Control;

namespace Lopushok1
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
            list.Children.Clear();
            using (SqlConnection connection = new SqlConnection(Connection.String))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($@"SELECT        Material.Title, MaterialType.Title AS Expr1, Product.MinCostForAgent, Product.Title AS Expr2, ProductType.Title AS Expr3, Product.ArticleNumber, Product.Image
                         FROM            Material INNER JOIN
                         MaterialType ON Material.MaterialTypeID = MaterialType.ID INNER JOIN
                         Product ON Material.ID = Product.ID INNER JOIN
                         ProductType ON Product.ProductTypeID = ProductType.ID
                         WHERE(Product.Title like '%{Search.Text}%') AND ProductType.Title like '%{(filtr.SelectedIndex == 0 ? "" : ((ComboBoxItem)filtr.SelectedItem).Content)}%'" + a, connection);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product_List product = new Product_List();
                        product.Title.Content = reader[3];
                        product.Materials1.Content = reader[0];
                        product.Product_Type.Content = reader[4];
                        product.Article.Content = reader[5];
                        product.photo.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[6].ToString().Replace(".jpg", ".jpeg")));
                        product.Materials2.Content = reader[1];
                        product.Cost.Content = reader[2];
                        product.main = this;
                        list.Children.Add(product);
                    }
                }
            }
        }
     
        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Load_data("");
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_data("");
        }

        private void Selection_filtr(object sender, SelectionChangedEventArgs e)
        {
            Load_data("");
        }
    }
}
