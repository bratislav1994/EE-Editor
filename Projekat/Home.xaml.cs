using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Home : Window
    {
        private Database serializer = new Database();
        private List<User> users = new List<User>();
        public Home()
        {
            InitializeComponent();
        }

        private void Prijava(object sender, RoutedEventArgs e)
        {
            bool isOk = true;
            bool isExist = false;

            if (textBox1.Text.Trim().Equals(""))
            {
                textBox1.BorderBrush = Brushes.Red;
                textBox1.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (textBox2.Password.Trim().Equals(""))
            {
                textBox2.BorderBrush = Brushes.Red;
                textBox2.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (isOk)
            {
                users = serializer.DeSerializeObject<List<User>>("../../files/users.xml");
                if (users != null)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].username.Equals(textBox1.Text)
                            && users[i].password.Equals(textBox2.Password))
                        {
                            isExist = true;
                        }
                    }
                }
                else
                {
                    isOk = false;
                    MessageBox.Show("User doesn't exist.");
                }
            }

            if (isOk && isExist)
            {
                if (Singleton.Instance().Name.Equals(string.Empty))
                {
                    Singleton.Instance().Name = textBox1.Text.Trim();
                    this.Close();
                }
            }

        }

        private void text_box1_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox1.BorderBrush = null;
            textBox1.BorderThickness = new Thickness(0);
        }

        private void text_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox2.BorderBrush = null;
            textBox2.BorderThickness = new Thickness(0);
        }

        private void Registracija(object sender, RoutedEventArgs e)
        {
            bool isOk = true;
            bool isExist = false;

            if (textBox3.Text.Trim().Equals(""))
            {
                textBox3.BorderBrush = Brushes.Red;
                textBox3.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (textBox4.Password.Trim().Equals(""))
            {
                textBox4.BorderBrush = Brushes.Red;
                textBox4.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (isOk)
            {
                users = serializer.DeSerializeObject<List<User>>("../../files/users.xml");
                if (users != null)
                {
                    for (int i = 0; i < users.Count; i++)
                    {
                        if (users[i].username.Equals(textBox3.Text))
                        {
                            isExist = true;
                        }
                    }
                }
                else
                {
                    users = new List<User>();
                }
                
            }

            if (isOk && !isExist)
            {
                User user = new User();
                user.username = textBox3.Text;
                user.password = textBox4.Password;

                users.Add(user);
                serializer.SerializeObject<List<User>>(users, "../../files/users.xml");

                if (Singleton.Instance().Name.Equals(string.Empty))
                {
                    Singleton.Instance().Name = textBox1.Text.Trim();
                    this.Close();
                }
            }

            if (isExist)
            {
                MessageBox.Show("User with this username already exists.");
            }
        }

        private void text_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox3.BorderBrush = null;
            textBox3.BorderThickness = new Thickness(0);
        }

        private void text_box4_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox4.BorderBrush = null;
            textBox4.BorderThickness = new Thickness(0);
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            //((MainWindow)this.Owner).Close();
            App.Current.Shutdown();
            //this.Close();
        }
    }
}
