///////////////////////////////////////////////////////////
//  EquipmentContainer.cs
//  Implementation of the Class EquipmentContainer
//  Generated by Enterprise Architect
//  Created on:      09-Jul-2016 12:00:26 AM
///////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



using CIM.IEC61970.Base.Core;
namespace CIM.IEC61970.Base.Core {
	/// <summary>
	/// A modeling construct to provide a root class for containing equipment.
	/// </summary>
	public class EquipmentContainer : ConnectivityNodeContainer {

		/// <summary>
		/// Contained equipment.
		/// </summary>
		public List<Equipment> Equipments;

		public EquipmentContainer(){
            Equipments = new List<Equipment>();
		}

		~EquipmentContainer(){

		}

	}//end EquipmentContainer

}//end namespace Core