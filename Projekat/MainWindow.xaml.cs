using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
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
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database serializer = new Database();

        public MainWindow()
        {
           
            InitializeComponent();
            ManageOthersVisibility();

            Observer observer = new Observer(canvas6, this);
            Singleton.Instance().Register(observer);

            DataContext = Singleton.Instance();

            Singleton.Instance().Nodes = serializer.DeSerializeObject<BindingList<ConnectivityNode>>("../../files/nodes.xml");
            if (Singleton.Instance().Nodes == null)
            {
                Singleton.Instance().Nodes = new BindingList<ConnectivityNode>();
            }
            nodesDataGrid.ItemsSource = Singleton.Instance().Nodes;

            Singleton.Instance().Substations = serializer.DeSerializeObject<BindingList<Substation>>("../../files/substations.xml");
            if (Singleton.Instance().Substations == null)
            {
                Singleton.Instance().Substations = new BindingList<Substation>();
            }
            substationsDataGrid.ItemsSource = Singleton.Instance().Substations;

            Singleton.Instance().Lines = serializer.DeSerializeObject<BindingList<ACLineSegment>>("../../files/lines.xml");
            if (Singleton.Instance().Lines == null)
            {
                Singleton.Instance().Lines = new BindingList<ACLineSegment>();
            }
            ACLinesDataGrid.ItemsSource = Singleton.Instance().Lines;

            Singleton.Instance().SynMachines = serializer.DeSerializeObject<BindingList<SynchronousMachine>>("../../files/synMachines.xml");
            if (Singleton.Instance().SynMachines == null)
            {
                Singleton.Instance().SynMachines = new BindingList<SynchronousMachine>();
            }
            SynMachineDataGrid.ItemsSource = Singleton.Instance().SynMachines;

            Singleton.Instance().PowerTransformers = serializer.DeSerializeObject<BindingList<PowerTransformer>>("../../files/powerTransformers.xml");
            if (Singleton.Instance().PowerTransformers == null)
            {
                Singleton.Instance().PowerTransformers = new BindingList<PowerTransformer>();
            }
            PowerTransformerDataGrid.ItemsSource = Singleton.Instance().PowerTransformers;

            Singleton.Instance().Consumers = serializer.DeSerializeObject<BindingList<EnergyConsumer>>("../../files/consumers.xml");
            if (Singleton.Instance().Consumers == null)
            {
                Singleton.Instance().Consumers = new BindingList<EnergyConsumer>();
            }
            ConsumerDataGrid.ItemsSource = Singleton.Instance().Consumers;

            Singleton.Instance().Ground = serializer.DeSerializeObject<BindingList<Ground>>("../../files/ground.xml");
            if (Singleton.Instance().Ground == null)
            {
                Singleton.Instance().Ground = new BindingList<Ground>();
            }
            GroundDataGrid.ItemsSource = Singleton.Instance().Ground;

            Singleton.Instance().Disconnectors = serializer.DeSerializeObject<BindingList<Disconnector>>("../../files/disconnectors.xml");
            if (Singleton.Instance().Disconnectors == null)
            {
                Singleton.Instance().Disconnectors = new BindingList<Disconnector>();
            }
            DisconnectorDataGrid.ItemsSource = Singleton.Instance().Disconnectors;

            Singleton.Instance().Breakers = serializer.DeSerializeObject<BindingList<Breaker>>("../../files/breakers.xml");
            if (Singleton.Instance().Breakers == null)
            {
                Singleton.Instance().Breakers = new BindingList<Breaker>();
            }
            BreakerDataGrid.ItemsSource = Singleton.Instance().Breakers;

            Singleton.Instance().LoadBreakers = serializer.DeSerializeObject<BindingList<LoadBreakSwitch>>("../../files/loadBreakers.xml");
            if (Singleton.Instance().LoadBreakers == null)
            {
                Singleton.Instance().LoadBreakers = new BindingList<LoadBreakSwitch>();
            }
            LoadBreakDataGrid.ItemsSource = Singleton.Instance().LoadBreakers;

            Singleton.Instance().Terminals = serializer.DeSerializeObject<BindingList<Terminal>>("../../files/terminals.xml");
            if (Singleton.Instance().Terminals == null)
            {
                Singleton.Instance().Terminals = new BindingList<Terminal>();
            }

            Home win1 = new Home();
            this.Show();
            win1.ShowDialog();
        }
        
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ManageOthersVisibility()
        {
            CanvasBorder.Visibility = Visibility.Hidden;
            CanvasBorder2.Visibility = Visibility.Hidden;
            CanvasBorder3.Visibility = Visibility.Hidden;
            CanvasBorder4.Visibility = Visibility.Hidden;
            CanvasBorder5.Visibility = Visibility.Hidden;
            CanvasBorder6.Visibility = Visibility.Hidden;
        }

        private void OpenTables(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder.Visibility = Visibility.Visible;
        }

        private void AddSubstation(object sender, RoutedEventArgs e)
        {
            CreateSubstation win1 = new CreateSubstation();
            win1.Owner = this;
            win1.ShowDialog();
            serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../files/substations.xml");
            Singleton.Instance().NotifyObservers();
        }

        private void DeleteSubstation(object sender, RoutedEventArgs e)
        {
            if (substationsDataGrid.SelectedItem != null)
            {
                Substation s = substationsDataGrid.SelectedItem as Substation;
                RemoveSubCommand rsc = new RemoveSubCommand(s.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rsc);
                serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../files/substations.xml");
                Singleton.Instance().NotifyObservers();
            }
        }

        private void AddNode(object sender, RoutedEventArgs e)
        {
            CreateNode win1 = new CreateNode();
            win1.Owner = this;
            win1.ShowDialog();
            serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../files/nodes.xml");
            Singleton.Instance().NotifyObservers();
        }

        private void DeleteNode(object sender, RoutedEventArgs e)
        {
            if (nodesDataGrid.SelectedItem != null)
            {
                ConnectivityNode cn = nodesDataGrid.SelectedItem as ConnectivityNode;
                RemoveNodeCommand rnc = new RemoveNodeCommand(cn.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rnc);
                serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../files/nodes.xml");
                Singleton.Instance().NotifyObservers();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            CanvasBorder.Visibility = Visibility.Hidden;
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().comInvoker.Undo();
            Singleton.Instance().NotifyObservers();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            Singleton.Instance().comInvoker.Redo();
            Singleton.Instance().NotifyObservers();
        }

        private void CloneSubstation(object sender, RoutedEventArgs e)
        {
            if (substationsDataGrid.SelectedItem != null)
            {
                Substation s = substationsDataGrid.SelectedItem as Substation;
                CloneSubCommand csc = new CloneSubCommand(s.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(csc);
                serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../files/substations.xml");
                Singleton.Instance().NotifyObservers();
            }
        }

        private void CloneNode(object sender, RoutedEventArgs e)
        {
            if (nodesDataGrid.SelectedItem != null)
            {
                ConnectivityNode cn = nodesDataGrid.SelectedItem as ConnectivityNode;
                CloneNodeCommand cnc = new CloneNodeCommand(cn.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cnc);
                serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../files/nodes.xml");
                Singleton.Instance().NotifyObservers();
            }
        }

        private void OpenTables2(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder2.Visibility = Visibility.Visible;
        }

        private void Exit2(object sender, RoutedEventArgs e)
        {
            CanvasBorder2.Visibility = Visibility.Hidden;
        }

        private void AddACLine(object sender, RoutedEventArgs e)
        {
            CreateACLine win1 = new CreateACLine();
            win1.Owner = this;
            win1.ShowDialog();
            Singleton.Instance().NotifyObservers();
        }

        private void DeleteACLine(object sender, RoutedEventArgs e)
        {
            if (ACLinesDataGrid.SelectedItem != null)
            {
                ACLineSegment acls = ACLinesDataGrid.SelectedItem as ACLineSegment;
                RemoveACLineCommand raclc = new RemoveACLineCommand(acls.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(raclc);
                Singleton.Instance().NotifyObservers();
            }
        }

        private void AddSynMachine(object sender, RoutedEventArgs e)
        {
            CreateSynMachine win1 = new CreateSynMachine();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteSynMachine(object sender, RoutedEventArgs e)
        {
            if (SynMachineDataGrid.SelectedItem != null)
            {
                SynchronousMachine sm = SynMachineDataGrid.SelectedItem as SynchronousMachine;
                RemoveSynMachineCommand rnc = new RemoveSynMachineCommand(sm.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rnc);
            }
        }

        private void CloneACLine(object sender, RoutedEventArgs e)
        {
            if (ACLinesDataGrid.SelectedItem != null)
            {
                ACLineSegment acls = ACLinesDataGrid.SelectedItem as ACLineSegment;
                CloneACLineCommand caclc = new CloneACLineCommand(acls.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(caclc);
                Singleton.Instance().NotifyObservers();
            }
        }

        private void CloneSynMachine(object sender, RoutedEventArgs e)
        {
            if (SynMachineDataGrid.SelectedItem != null)
            {
                SynchronousMachine sm = SynMachineDataGrid.SelectedItem as SynchronousMachine;
                CloneSynMachineCommand csmc = new CloneSynMachineCommand(sm.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(csmc);
            }
        }

        private void Exit3(object sender, RoutedEventArgs e)
        {
            CanvasBorder3.Visibility = Visibility.Hidden;
        }

        private void AddPowerTransformer(object sender, RoutedEventArgs e)
        {
            CreatePowerTransformer win1 = new CreatePowerTransformer();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeletePowerTransformer(object sender, RoutedEventArgs e)
        {
            if (PowerTransformerDataGrid.SelectedItem != null)
            {
                PowerTransformer pt = PowerTransformerDataGrid.SelectedItem as PowerTransformer;
                RemovePowerTransformerCommand rptc = new RemovePowerTransformerCommand(pt.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rptc);
            }
        }

        private void ClonePowerTransformer(object sender, RoutedEventArgs e)
        {
            if (PowerTransformerDataGrid.SelectedItem != null)
            {
                PowerTransformer pt = PowerTransformerDataGrid.SelectedItem as PowerTransformer;
                ClonePowerTransformerCommand cpt = new ClonePowerTransformerCommand(pt.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cpt);
            }
        }

        private void AddConsumer(object sender, RoutedEventArgs e)
        {
            CreateConsumer win1 = new CreateConsumer();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteConsumer(object sender, RoutedEventArgs e)
        {
            if (ConsumerDataGrid.SelectedItem != null)
            {
                EnergyConsumer ec = ConsumerDataGrid.SelectedItem as EnergyConsumer;
                RemoveConsumerCommand rcc = new RemoveConsumerCommand(ec.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rcc);
            }
        }

        private void CloneConsumer(object sender, RoutedEventArgs e)
        {
            if (ConsumerDataGrid.SelectedItem != null)
            {
                EnergyConsumer ec = ConsumerDataGrid.SelectedItem as EnergyConsumer;
                CloneConsumerCommand ccc = new CloneConsumerCommand(ec.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(ccc);
            }
        }

        private void OpenTables3(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder3.Visibility = Visibility.Visible;
        }

        private void Exit4(object sender, RoutedEventArgs e)
        {
            CanvasBorder4.Visibility = Visibility.Hidden;
        }

        private void AddGround(object sender, RoutedEventArgs e)
        {
            CreateGround win1 = new CreateGround();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteGround(object sender, RoutedEventArgs e)
        {
            if (GroundDataGrid.SelectedItem != null)
            {
                Ground g = GroundDataGrid.SelectedItem as Ground;
                RemoveGroundCommand rgc = new RemoveGroundCommand(g.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rgc);
            }
        }

        private void CloneGround(object sender, RoutedEventArgs e)
        {
            if (GroundDataGrid.SelectedItem != null)
            {
                Ground g = GroundDataGrid.SelectedItem as Ground;
                CloneGroundCommand cgc = new CloneGroundCommand(g.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cgc);
            }
        }

        private void AddDisconnector(object sender, RoutedEventArgs e)
        {
            CreateDisconnector win1 = new CreateDisconnector();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteDisconnector(object sender, RoutedEventArgs e)
        {
            if (DisconnectorDataGrid.SelectedItem != null)
            {
                Disconnector d = DisconnectorDataGrid.SelectedItem as Disconnector;
                RemoveDisconnectorCommand rdc = new RemoveDisconnectorCommand(d.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rdc);
            }
        }

        private void CloneDisconnector(object sender, RoutedEventArgs e)
        {
            if (DisconnectorDataGrid.SelectedItem != null)
            {
                Disconnector d = DisconnectorDataGrid.SelectedItem as Disconnector;
                CloneDisconnectorCommand cdc = new CloneDisconnectorCommand(d.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cdc);
            }
        }

        private void OpenTables4(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder4.Visibility = Visibility.Visible;
        }

        private void Exit5(object sender, RoutedEventArgs e)
        {
            CanvasBorder5.Visibility = Visibility.Hidden;
        }

        private void AddBreaker(object sender, RoutedEventArgs e)
        {
            CreateBreaker win1 = new CreateBreaker();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteBreaker(object sender, RoutedEventArgs e)
        {
            if (BreakerDataGrid.SelectedItem != null)
            {
                Breaker b = BreakerDataGrid.SelectedItem as Breaker;
                RemoveBreakerCommand rbc = new RemoveBreakerCommand(b.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rbc);
            }
        }

        private void CloneBreaker(object sender, RoutedEventArgs e)
        {
            if (BreakerDataGrid.SelectedItem != null)
            {
                Breaker b = BreakerDataGrid.SelectedItem as Breaker;
                CloneBreakerCommand cbc = new CloneBreakerCommand(b.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cbc);
            }
        }

        private void AddLoadBreaker(object sender, RoutedEventArgs e)
        {
            CreateLoadBreaker win1 = new CreateLoadBreaker();
            win1.Owner = this;
            win1.ShowDialog();
        }

        private void DeleteLoadBreaker(object sender, RoutedEventArgs e)
        {
            if (LoadBreakDataGrid.SelectedItem != null)
            {
                LoadBreakSwitch lbs = LoadBreakDataGrid.SelectedItem as LoadBreakSwitch;
                RemoveLoadBreakerCommand rlbc = new RemoveLoadBreakerCommand(lbs.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(rlbc);
            }
        }

        private void CloneLoadBreaker(object sender, RoutedEventArgs e)
        {
            if (LoadBreakDataGrid.SelectedItem != null)
            {
                LoadBreakSwitch lbs = LoadBreakDataGrid.SelectedItem as LoadBreakSwitch;
                CloneLoadBreakerCommand cbc = new CloneLoadBreakerCommand(lbs.mRID);
                Singleton.Instance().comInvoker.AddAndExecute(cbc);
            }
        }

        private void OpenTables5(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder5.Visibility = Visibility.Visible;
        }

        private void Save(object sender, EventArgs e)
        {
            serializer.SerializeObject<BindingList<Substation>>(Singleton.Instance().Substations, "../../files/substations.xml");
            serializer.SerializeObject<BindingList<ConnectivityNode>>(Singleton.Instance().Nodes, "../../files/nodes.xml");
            serializer.SerializeObject<BindingList<ACLineSegment>>(Singleton.Instance().Lines, "../../files/lines.xml");
            serializer.SerializeObject<BindingList<SynchronousMachine>>(Singleton.Instance().SynMachines, "../../files/synMachines.xml");
            serializer.SerializeObject<BindingList<PowerTransformer>>(Singleton.Instance().PowerTransformers, "../../files/powerTransformers.xml");
            serializer.SerializeObject<BindingList<EnergyConsumer>>(Singleton.Instance().Consumers, "../../files/consumers.xml");
            serializer.SerializeObject<BindingList<Ground>>(Singleton.Instance().Ground, "../../files/ground.xml");
            serializer.SerializeObject<BindingList<Disconnector>>(Singleton.Instance().Disconnectors, "../../files/disconnectors.xml");
            serializer.SerializeObject<BindingList<Breaker>>(Singleton.Instance().Breakers, "../../files/breakers.xml");
            serializer.SerializeObject<BindingList<LoadBreakSwitch>>(Singleton.Instance().LoadBreakers, "../../files/loadBreakers.xml");
            serializer.SerializeObject<BindingList<Terminal>>(Singleton.Instance().Terminals, "../../files/terminals.xml");
        }

        private void OpenCanvas(object sender, RoutedEventArgs e)
        {
            ManageOthersVisibility();
            CanvasBorder6.Visibility = Visibility.Visible;
            Singleton.Instance().NotifyObservers();
        }
    }
}
