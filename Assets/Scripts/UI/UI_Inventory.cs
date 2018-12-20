using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour {
	public PlayerInventory playerInventory;
	public Transform itemsParent;
	public List<InventorySlot> slots;

	// Use this for initialization
	void Start () {
		playerInventory.RegisterEvent (OnUpdate);

		slots = new List<InventorySlot> (
			itemsParent.GetComponentsInChildren<InventorySlot>()
		);

		InvokeClose ();
	}
	
	// Update is called once per frame
	void OnUpdate () {
		//Debug.Log ("UI_Inventory refresh");
		for (int i = 0; i < slots.Count; i++) {
			if (i < playerInventory.items.Count) {
				slots [i].AddItem (playerInventory.items [i], playerInventory);
			} else {
				slots [i].ClearSlot ();
			}
		}
	}

	public void InvokeClose(){
		gameObject.SetActive (false);
	}
}
