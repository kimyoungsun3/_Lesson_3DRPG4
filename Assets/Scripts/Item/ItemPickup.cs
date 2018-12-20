using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {
	public Item item;
	public override void Interact(){
		base.Interact ();

		PickUp ();
	}

	void PickUp(){
		//Debug.Log ("Picking up " + item.name);
		bool _bGetItem = false;
		PlayerInventory _inventory = player.GetComponent<PlayerInventory> ();
		if (_inventory != null) {
			_bGetItem = _inventory.Add (item);
			if (_bGetItem) {
				Destroy (gameObject);
			} else {
				Debug.Log ("Inventory Full");
			}
		}
	}
}
