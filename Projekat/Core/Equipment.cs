///////////////////////////////////////////////////////////
//  Equipment.cs
//  Implementation of the Class Equipment
//  Generated by Enterprise Architect
//  Created on:      09-Jul-2016 12:00:26 AM
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using CIM.IEC61970.Base.Core;
using System.Xml.Serialization;
using CIM.IEC61970.Base.Wires;

namespace CIM.IEC61970.Base.Core
{
    /// <summary>
    /// The parts of a power system that are physical devices, electronic or mechanical.
    /// 
    /// </summary>
    [XmlInclude(typeof(PowerTransformer))]
    [XmlInclude(typeof(SynchronousMachine))]
    [XmlInclude(typeof(EnergyConsumer))]
    [XmlInclude(typeof(Ground))]
    [XmlInclude(typeof(Disconnector))]
    [XmlInclude(typeof(Breaker))]
    [XmlInclude(typeof(LoadBreakSwitch))]
    public class Equipment : PowerSystemResource {

		public Equipment(){

		}

		~Equipment(){

		}

	}//end Equipment

}//end namespace Core