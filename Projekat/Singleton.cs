using CIM.IEC61970.Base.Core;
using CIM.IEC61970.Base.Wires;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat
{
    public class Singleton
    {
        private Database serializer = new Database();
        private static Singleton instance;
        private BindingList<Substation> substations;
        private BindingList<ConnectivityNode> nodes;
        private BindingList<ACLineSegment> lines;
        private BindingList<SynchronousMachine> synMachines;
        private BindingList<PowerTransformer> powerTransformers;
        private BindingList<EnergyConsumer> consumers;
        private BindingList<Ground> ground;
        private BindingList<Disconnector> disconnectors;
        private BindingList<Breaker> breakers;
        private BindingList<LoadBreakSwitch> loadBreakers;
        private BindingList<Terminal> terminals;
        private string name;
        public CommandInvoker comInvoker;
        private List<IObserver> observers;

        private Singleton()
        {
            comInvoker = new CommandInvoker();
            Nodes = new BindingList<ConnectivityNode>();
            Substations = new BindingList<Substation>();
            Lines = new BindingList<ACLineSegment>();
            SynMachines = new BindingList<SynchronousMachine>();
            PowerTransformers = new BindingList<PowerTransformer>();
            Consumers = new BindingList<EnergyConsumer>();
            Ground = new BindingList<CIM.IEC61970.Base.Wires.Ground>();
            Disconnectors = new BindingList<Disconnector>();
            Breakers = new BindingList<Breaker>();
            LoadBreakers = new BindingList<LoadBreakSwitch>();
            Name = string.Empty;
            observers = new List<IObserver>();
        }

        public void Register(IObserver observer)
        {
            observers.Add(observer);
        }

        public void UnRegister(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.NotifyObservers();
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public BindingList<Substation> Substations
        {
            get
            {
                return substations;
            }
            set
            {
                substations = value;
            }
        }

        public BindingList<ConnectivityNode> Nodes
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
            }
        }

        public BindingList<ACLineSegment> Lines
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }

        public BindingList<SynchronousMachine> SynMachines
        {
            get
            {
                return synMachines;
            }
            set
            {
                synMachines = value;
            }
        }

        public BindingList<PowerTransformer> PowerTransformers
        {
            get
            {
                return powerTransformers;
            }
            set
            {
                powerTransformers = value;
            }
        }

        public BindingList<EnergyConsumer> Consumers
        {
            get
            {
                return consumers;
            }
            set
            {
                consumers = value;
            }
        }

        public BindingList<Ground> Ground
        {
            get
            {
                return ground;
            }
            set
            {
                ground = value;
            }
        }

        public BindingList<Disconnector> Disconnectors
        {
            get
            {
                return disconnectors;
            }
            set
            {
                disconnectors = value;
            }
        }

        public BindingList<Breaker> Breakers
        {
            get
            {
                return breakers;
            }
            set
            {
                breakers = value;
            }
        }

        public BindingList<LoadBreakSwitch> LoadBreakers
        {
            get
            {
                return loadBreakers;
            }
            set
            {
                loadBreakers = value;
            }
        }

        public BindingList<Terminal> Terminals
        {
            get
            {
                return terminals;
            }
            set
            {
                terminals = value;
            }
        }

        public CommandInvoker ComInvoker
        {
            get
            {
                return comInvoker;
            }
            set
            {
                comInvoker = value;
            }
        }

        public static Singleton Instance()
        {
            if (instance == null)
                instance = new Singleton();

            return instance;
        }
    }
}
