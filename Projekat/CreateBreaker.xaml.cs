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
    /// Interaction logic for CreateBreaker.xaml
    /// </summary>
    public partial class CreateBreaker : Window
    {
        public CreateBreaker()
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
                    combo_box10.Items.Add(s);
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

            if (text_box3.Text.Trim().Equals(""))
            {
                text_box3.BorderBrush = Brushes.Red;
                text_box3.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box4.Text.Trim().Equals(""))
            {
                text_box4.BorderBrush = Brushes.Red;
                text_box4.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box4.Text.Trim());

                    if (float.Parse(text_box4.Text.Trim()) < 1)
                    {
                        text_box4.BorderBrush = Brushes.Red;
                        text_box4.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box4.BorderBrush = Brushes.Red;
                    text_box4.BorderThickness = new Thickness(1);
                    isOk = false;
                }

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

            if (combo_box10.SelectedItem == null)
            {
                combo_box10.BorderBrush = Brushes.Red;
                combo_box10.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (text_box11.Text.Trim().Equals(""))
            {
                text_box11.BorderBrush = Brushes.Red;
                text_box11.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box11.Text.Trim());

                    if (float.Parse(text_box11.Text.Trim()) < 1)
                    {
                        text_box11.BorderBrush = Brushes.Red;
                        text_box11.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box11.BorderBrush = Brushes.Red;
                    text_box11.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (isOk)
            {
                bool normal = (bool)combo_box6.SelectedItem;
                bool open = (bool)combo_box7.SelectedItem;
                bool retained = (bool)combo_box9.SelectedItem;
                string subID = (combo_box10.SelectedItem as Substation).mRID;
                Substation sub = (combo_box10.SelectedItem as Substation);

                bool isDifferentVoltage = false;

                foreach (VoltageLevel vl in sub.VoltageLevels)
                {
                    if (vl.BaseVoltage.nominalVoltage != float.Parse(text_box11.Text.Trim()))
                    {
                        isDifferentVoltage = true;
                        MessageBox.Show("Nominal voltage of substation must be equals with input base voltage.");
                        break;
                    }
                }

                if (!isDifferentVoltage)
                {
                    AddBreakerCommand abc = new AddBreakerCommand(text_box1.Text.Trim(), text_box2.Text.Trim(), text_box3.Text.Trim(), float.Parse(text_box4.Text.Trim()), normal, open, float.Parse(text_box8.Text.Trim()), retained, subID, float.Parse(text_box11.Text.Trim()));
                    Singleton.Instance().comInvoker.AddAndExecute(abc);
                    this.Close();
                }
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

        private void text_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box3.BorderBrush = null;
            text_box3.BorderThickness = new Thickness(0);
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

        private void text_box11_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box11.BorderBrush = null;
            text_box11.BorderThickness = new Thickness(0);
        }

        private void combo_box10_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box10.BorderBrush = null;
            combo_box10.BorderThickness = new Thickness(0);
        }
    }
}
