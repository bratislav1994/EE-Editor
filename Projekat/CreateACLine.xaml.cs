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
    /// Interaction logic for CreateACLine.xaml
    /// </summary>
    public partial class CreateACLine : Window
    {
        public const int NUMBER_PARAMETERS = 8;

        public CreateACLine()
        {
            InitializeComponent();

            if (File.Exists("../../files/nodes.xml"))
            {
                foreach (ConnectivityNode cn in Singleton.Instance().Nodes)
                {
                    combo_box1.Items.Add(cn);
                    combo_box2.Items.Add(cn);
                }
            }
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

        private void text_box9_GotFocus(object sender, RoutedEventArgs e)
        {
            text_box9.BorderBrush = null;
            text_box9.BorderThickness = new Thickness(0);
        }
        
        public bool IsOk(String textBox)
        {
            if (textBox.Equals(""))
            {
                return false;
            }
            else
            {
                try
                {
                    float.Parse(textBox);

                    if (float.Parse(text_box5.Text.Trim()) < 10)
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }

            }

            return true;
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

                    if (float.Parse(text_box5.Text.Trim()) < 2)
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

            if (text_box9.Text.Trim().Equals(""))
            {
                text_box9.BorderBrush = Brushes.Red;
                text_box9.BorderThickness = new Thickness(1);
                isOk = false;
            }
            else
            {
                try
                {
                    float.Parse(text_box9.Text.Trim());

                    if (float.Parse(text_box9.Text.Trim()) < 1)
                    {
                        text_box9.BorderBrush = Brushes.Red;
                        text_box9.BorderThickness = new Thickness(1);
                        isOk = false;
                    }
                }
                catch
                {
                    text_box9.BorderBrush = Brushes.Red;
                    text_box9.BorderThickness = new Thickness(1);
                    isOk = false;
                }

            }

            if (combo_box1.SelectedItem == null)
            {
                isOk = false;
            }

            if (combo_box2.SelectedItem == null)
            {
                isOk = false;
            }

            ConnectivityNode cn = combo_box1.SelectedItem as ConnectivityNode;
            ConnectivityNode cn2 = combo_box2.SelectedItem as ConnectivityNode;

            bool sameSubstation = false;
            bool sameSubstation2 = false;

            foreach (Substation s in Singleton.Instance().Substations)
            {
                foreach (ConnectivityNode conNode in s.connectivityNodes)
                {
                    if (conNode.mRID.Equals(cn.mRID))
                    {
                        sameSubstation = true;
                    }
                    else if (conNode.mRID.Equals(cn2.mRID))
                    {
                        sameSubstation2 = true;
                    }
                }

                if (sameSubstation && sameSubstation2)
                {
                    break;
                }
                sameSubstation = false;
                sameSubstation2 = false;
            }

            if (cn.mRID.Equals(cn2.mRID))
            {
                MessageBox.Show("Nodes must be different.");
                isOk = false;
            }
            else if (cn.m_BaseVoltage.nominalVoltage != cn2.m_BaseVoltage.nominalVoltage)
            {
                MessageBox.Show("Nodes must have same nominal voltage.");
                isOk = false;
            }
            else if (sameSubstation && sameSubstation2)
            {
                MessageBox.Show("Nodes can't be from same substation.");
                isOk = false;
            }

            bool nodeLine = false;
            bool nodeLine2 = false;

            foreach (ACLineSegment acl in Singleton.Instance().Lines)
            {
                nodeLine = false;
                nodeLine2 = false;

                foreach (Terminal t in Singleton.Instance().Terminals)
                {
                    if (t.mRID.Contains(acl.mRID))
                    {
                        if (t.ConnectivityNode.mRID.Equals(cn.mRID))
                        {
                            nodeLine = true;
                        }
                        else if (t.ConnectivityNode.mRID.Equals(cn2.mRID))
                        {
                            nodeLine2 = true;
                        }
                    }
                }

                if (nodeLine && nodeLine2)
                {
                    break;
                }
            }

            if (nodeLine && nodeLine2)
            {
                MessageBox.Show("This line already exist.");
                isOk = false;
            }

            if (isOk)
            {
                string[] arrayOfParameters = new string[NUMBER_PARAMETERS];

                arrayOfParameters[0] = text_box1.Text.Trim();
                arrayOfParameters[1] = text_box2.Text.Trim();
                arrayOfParameters[2] = text_box4.Text.Trim();
                arrayOfParameters[3] = text_box5.Text.Trim();
                arrayOfParameters[4] = text_box6.Text.Trim();
                arrayOfParameters[5] = text_box7.Text.Trim();
                arrayOfParameters[6] = text_box8.Text.Trim();
                arrayOfParameters[7] = text_box9.Text.Trim();
            
                AddACLineCommand asc = new AddACLineCommand(arrayOfParameters, cn, cn2);
                Singleton.Instance().comInvoker.AddAndExecute(asc);
                this.Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
