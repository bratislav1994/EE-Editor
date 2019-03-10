using CIM.IEC61970.Base.Core;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CreateDisconnector.xaml
    /// </summary>
    public partial class CreateDisconnector : Window
    {
        public CreateDisconnector()
        {
            InitializeComponent();

            bool[] array = new bool[2];
            array[0] = true;
            array[1] = false;

            for (int i = 0; i < 2; i++)
            {
                combo_box6.Items.Add(array[i]);
                combo_box7.Items.Add(array[i]);
                combo_box9.Items.Add(array[i]);
            }

            if (File.Exists("../../files/substations.xml"))
            {
                foreach (Substation s in Singleton.Instance().Substations)
                {
                    combo_box8.Items.Add(s);
                }
            }
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

            if (text_box8.Text.Trim().Equals(""))
            {
                text_box8.BorderBrush = Brushes.Red;
                text_box8.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box8.Text.Trim());

                    if (float.Parse(text_box8.Text.Trim()) < 1)
                    {
                        text_box8.BorderBrush = Brushes.Red;
                        text_box8.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box8.BorderBrush = Brushes.Red;
                    text_box8.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (combo_box6.SelectedItem == null)
            {
                combo_box6.BorderBrush = Brushes.Red;
                combo_box6.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (combo_box7.SelectedItem == null)
            {
                combo_box7.BorderBrush = Brushes.Red;
                combo_box7.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (combo_box9.SelectedItem == null)
            {
                combo_box9.BorderBrush = Brushes.Red;
                combo_box9.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (combo_box8.SelectedItem == null)
            {
                combo_box8.BorderBrush = Brushes.Red;
                combo_box8.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (isOk)
            {
                String subID = (combo_box8.SelectedItem as Substation).mRID;
                bool normal = (bool)combo_box6.SelectedItem;
                bool open = (bool)combo_box7.SelectedItem;
                bool retained = (bool)combo_box9.SelectedItem;

                AddDisconnectorCommand adc = new AddDisconnectorCommand(text_box1.Text.Trim(), text_box2.Text.Trim(), text_box4.Text.Trim(), normal, open, float.Parse(text_box8.Text.Trim()), retained, subID);
                Singleton.Instance().comInvoker.AddAndExecute(adc);
                this.Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        private void text_box8_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box8.BorderBrush = null;
            text_box8.BorderThickness = new Thickness(0);
        }

        private void combo_box6_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box6.BorderBrush = null;
            combo_box6.BorderThickness = new Thickness(0);
        }

        private void combo_box7_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box7.BorderBrush = null;
            combo_box7.BorderThickness = new Thickness(0);
        }

        private void combo_box9_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box9.BorderBrush = null;
            combo_box9.BorderThickness = new Thickness(0);
        }

        private void combo_box8_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box8.BorderBrush = null;
            combo_box8.BorderThickness = new Thickness(0);
        }
    }
}
