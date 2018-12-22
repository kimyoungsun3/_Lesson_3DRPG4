using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats {

	// Use this for initialization
	void Start () {
		EquipmentManager.ins.onEquipmentChanged 
		+= OnEquipmentChanged;	
	}

	void OnEquipmentChanged(Equipment _newItem, Equipment _oldItem){
		if (_newItem != null) {
			armor.AddModifier (_newItem.def);
			damage.AddModifier (_newItem.att);
		}
		if (_oldItem != null) {
			armor.RemoveModifier (_oldItem.def);
			damage.RemoveModifier (_oldItem.att);
		}
	}

	public override void Die()
	{
		base.Die();
		PlayerManager.ins.KillPlayer();
	}
}
