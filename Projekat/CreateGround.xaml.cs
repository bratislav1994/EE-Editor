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
    /// Interaction logic for CreateGround.xaml
    /// </summary>
    public partial class CreateGround : Window
    {
        public CreateGround()
        {
            InitializeComponent();

            if (File.Exists("../../files/substations.xml"))
            {
                foreach (Substation s in Singleton.Instance().Substations)
                {
                    combo_box3.Items.Add(s);
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

            if (combo_box3.SelectedItem == null)
            {
                combo_box3.BorderBrush = Brushes.Red;
                combo_box3.BorderThickness = new Thickness(1);
                isOk = false;
            }

            if (isOk)
            {
                String subID = (combo_box3.SelectedItem as Substation).mRID;

                AddGroundCommand agc = new AddGroundCommand(text_box1.Text.Trim(), text_box2.Text.Trim(), text_box4.Text.Trim(), subID);
                Singleton.Instance().comInvoker.AddAndExecute(agc);
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

        private void combo_box3_GotFocus(object sender, RoutedEventArgs e)
        {
            combo_box3.BorderBrush = null;
            combo_box3.BorderThickness = new Thickness(0);
        }
    }
}
