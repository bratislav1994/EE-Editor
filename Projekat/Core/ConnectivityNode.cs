///////////////////////////////////////////////////////////
//  ConnectivityNode.cs
//  Implementation of the Class ConnectivityNode
//  Generated by Enterprise Architect
//  Created on:      09-Jul-2016 12:00:25 AM
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using CIM.IEC61970.Base.Core;
using Projekat;
using System.ComponentModel;

namespace CIM.IEC61970.Base.Core {
    [Serializable]
    /// <summary>
    /// Connectivity nodes are points where terminals of AC conducting equipment are
    /// connected together with zero impedance.
    /// </summary>
    public class ConnectivityNode : IdentifiedObject, IPrototype, INotifyPropertyChanged
    {

        public double x { get; set; }
        public double y { get; set; }
        public double x1;
        public double y1;
        public double X
        { get { return x1; } set { x1 = value; NotifyPropertyChanged("X"); } }
        public double Y
        { get { return y1; } set { y1 = value; NotifyPropertyChanged("Y"); } }
        public CIM.IEC61970.Base.Core.BaseVoltage m_BaseVoltage { get; set; }// = new BaseVoltage();
        /// <summary>
        /// Container of this connectivity node.
        /// </summary>
        //public CIM.IEC61970.Base.Core.ConnectivityNodeContainer ConnectivityNodeContainer { get; set; }// = new ConnectivityNodeContainer();

        public ConnectivityNode(){
            m_BaseVoltage = new BaseVoltage();
            
        }

        public ConnectivityNode(string id)
        {
            this.mRID = id;
        }

		~ConnectivityNode(){

		}

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public object Clone()
        {
            ConnectivityNode cn = new ConnectivityNode();

            foreach (ConnectivityNode conNode in Singleton.Instance().Nodes)
            {
                if (this.mRID.Equals(conNode.mRID))
                {
                    cn.aliasName = conNode.aliasName;
                    cn.description = "clone_" + conNode.mRID;

                    cn.name = conNode.name;
                    
                    cn.x = -1;
                    cn.y = -1;

                    cn.m_BaseVoltage = conNode.m_BaseVoltage;
                    cn.m_BaseVoltage.ConductingEquipment = new List<ConductingEquipment>(conNode.m_BaseVoltage.ConductingEquipment);

                    for (int i = 0; i < Singleton.Instance().Substations.Count; i++)
                    {
                        for (int j = 0; j < Singleton.Instance().Substations[i].connectivityNodes.Count; j++)
                        {
                            if (Singleton.Instance().Substations[i].connectivityNodes[j].mRID.Equals(conNode.mRID))
                            {
                                Singleton.Instance().Substations[i].connectivityNodes.Add(cn);
                                break;
                            }
                        }
                    }
                    
                    break;
                }
            }

            return cn;
        }

        public override string ToString()
        {
            return "id: " + mRID + ", name: " + name;
        }
    }//end ConnectivityNode

}//end namespace Core