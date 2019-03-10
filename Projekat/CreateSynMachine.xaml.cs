using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
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
    /// Interaction logic for CreateSynMachine.xaml
    /// </summary>
    public partial class CreateSynMachine : Window
    {
        public CreateSynMachine()
        {
            InitializeComponent();

            if (File.Exists("../../files/substations.xml"))
            {
                foreach (Substation s in Singleton.Instance().Substations)
                {
                    combo_box14.Items.Add(s);
                }
            }

            foreach (var item in Enum.GetValues(typeof(SynchronousMachineKind)))
            {
                combo_box9.Items.Add(item);
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

            if (text_box6.Text.Trim().Equals(""))
            {
                text_box6.BorderBrush = Brushes.Red;
                text_box6.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box6.Text.Trim());

                    if (float.Parse(text_box6.Text.Trim()) < 1)
                    {
                        text_box6.BorderBrush = Brushes.Red;
                        text_box6.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box6.BorderBrush = Brushes.Red;
                    text_box6.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (text_box7.Text.Trim().Equals(""))
            {
                text_box7.BorderBrush = Brushes.Red;
                text_box7.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box7.Text.Trim());

                    if (float.Parse(text_box7.Text.Trim()) < 1)
                    {
                        text_box7.BorderBrush = Brushes.Red;
                        text_box7.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box7.BorderBrush = Brushes.Red;
                    text_box7.BorderThickness = new Thickness(1);
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

            if (text_box10.Text.Trim().Equals(""))
            {
                text_box10.BorderBrush = Brushes.Red;
                text_box10.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box10.Text.Trim());

                    if (float.Parse(text_box10.Text.Trim()) < 1)
                    {
                        text_box10.BorderBrush = Brushes.Red;
                        text_box10.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box10.BorderBrush = Brushes.Red;
                    text_box10.BorderThickness = new Thickness(1);
                    isOk = false;
                }

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

            if (text_box12.Text.Trim().Equals(""))
            {
                text_box12.BorderBrush = Brushes.Red;
                text_box12.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box12.Text.Trim());

                    if (float.Parse(text_box12.Text.Trim()) < 1)
                    {
                        text_box12.BorderBrush = Brushes.Red;
                        text_box12.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box12.BorderBrush = Brushes.Red;
                    text_box12.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (text_box13.Text.Trim().Equals(""))
            {
                text_box13.BorderBrush = Brushes.Red;
                text_box13.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box13.Text.Trim());

                    if (float.Parse(text_box13.Text.Trim()) < 1)
                    {
                        text_box13.BorderBrush = Brushes.Red;
                        text_box13.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box13.BorderBrush = Brushes.Red;
                    text_box13.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (combo_box9.SelectedItem == null)
            {
                combo_box9.BorderBrush = Brushes.Red;
                combo_box9.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (combo_box14.SelectedItem == null)
            {
                combo_box14.BorderBrush = Brushes.Red;
                combo_box14.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (isOk)
            {
                String subID = (combo_box14.SelectedItem as Substation).mRID;
                SynchronousMachineKind smk = (SynchronousMachineKind)combo_box9.SelectedItem;

                AddSynMachineCommand asmc = new AddSynMachineCommand(text_box1.Text.Trim(), text_box2.Text.Trim(), text_box3.Text.Trim(), float.Parse(text_box4.Text.Trim()), float.Parse(text_box5.Text.Trim()), float.Parse(text_box6.Text.Trim()), float.Parse(text_box7.Text.Trim()), float.Parse(text_box8.Text.Trim()), smk, float.Parse(text_box10.Text.Trim()), float.Parse(text_box11.Text.Trim()), float.Parse(text_box12.Text.Trim()), float.Parse(text_box13.Text.Trim()), subID);
                Singleton.Instance().comInvoker.AddAndExecute(asmc);
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

        private void text_box5_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box5.BorderBrush = null;
            text_box5.BorderThickness = new Thickness(0);
        }

        private void text_box6_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box6.BorderBrush = null;
            text_box6.BorderThickness = new Thickness(0);
        }

        private void text_box7_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box7.BorderBrush = null;
            text_box7.BorderThickness = new Thickness(0);
        }

        private void text_box8_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box8.BorderBrush = null;
            text_box8.BorderThickness = new Thickness(0);
        }

        private void text_box10_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box10.BorderBrush = null;
            text_box10.BorderThickness = new Thickness(0);
        }

        private void text_box11_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box11.BorderBrush = null;
            text_box11.BorderThickness = new Thickness(0);
        }

        private void text_box12_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box12.BorderBrush = null;
            text_box12.BorderThickness = new Thickness(0);
        }

        private void text_box13_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box13.BorderBrush = null;
            text_box13.BorderThickness = new Thickness(0);
        }

        private void combo_box9_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box9.BorderBrush = null;
            combo_box9.BorderThickness = new Thickness(0);
        }

        private void combo_box14_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box14.BorderBrush = null;
            combo_box14.BorderThickness = new Thickness(0);
        }
    }
}
