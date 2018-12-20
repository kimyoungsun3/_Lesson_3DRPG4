using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item", menuName="Inventory/Equipment")]
public class Equipment : Item {
	public EquipmentSlot equipSlot;
	public SkinnedMeshRenderer mesh;
	public EquipmentMeshRegion[] coveredMeshRegions;
	public int def;
	public int att;

	public override void Use(){
		base.Use ();
		//Equip the item
		EquipmentManager.ins.Equip(this);

		//Remove it from the inventor
		RemoveFromInventory();
	}
}

public enum EquipmentSlot { 
	Head, Chest, Legs, 
	Weapon, Shield, Feet 
}

public enum EquipmentMeshRegion{ Legs, Arms, Torso };
