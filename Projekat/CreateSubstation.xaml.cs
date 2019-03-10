using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for CreateSubstation.xaml
    /// </summary>
    public partial class CreateSubstation : Window
    {
        private Database serializer = new Database();
        public CreateSubstation()
        {
            InitializeComponent();
        }

        private void text_box1_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box1.BorderBrush = null;
            text_box1.BorderThickness = new Thickness(0);
        }

        private void text_box2_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box2.BorderBrush = null;
            text_box2.BorderThickness = new Thickness(0);
        }

        private void text_box4_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box4.BorderBrush = null;
            text_box4.BorderThickness = new Thickness(0);
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            bool isOk = true;

            if (text_box1.Text.Trim().Equals(""))
            {
                text_box1.BorderBrush = Brushes.Red;
                text_box1.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box2.Text.Trim().Equals(""))
            {
                text_box2.BorderBrush = Brushes.Red;
                text_box2.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box4.Text.Trim().Equals(""))
            {
                text_box4.BorderBrush = Brushes.Red;
                text_box4.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box5.Text.Trim().Equals(""))
            {
                text_box5.BorderBrush = Brushes.Red;
                text_box5.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box5.Text.Trim());

                    if (float.Parse(text_box5.Text.Trim()) < 1)
                    {
                        text_box5.BorderBrush = Brushes.Red;
                        text_box5.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box5.BorderBrush = Brushes.Red;
                    text_box5.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (isOk)
            {
                AddSubCommand asc = new AddSubCommand(text_box1.Text.Trim(), text_box2.Text.Trim(), text_box4.Text.Trim(), float.Parse(text_box5.Text.Trim()));
                Singleton.Instance().comInvoker.AddAndExecute(asc);
                this.Close();
            }

        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void text_box5_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box2.BorderBrush = null;
            text_box2.BorderThickness = new Thickness(0);
        }
    }
}
