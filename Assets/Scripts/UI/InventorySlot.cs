using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
	public Image icon;
	Item item;
	public Button removeButton;
	PlayerInventory playerInventory;

	public void AddItem(Item _item, PlayerInventory _inventory){
		item = _item;

		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.GetComponent<Image> ().enabled = true;
		removeButton.enabled = true;
		playerInventory = _inventory;
	}

	public void ClearSlot(){
		item = null;

		icon.sprite = null;
		icon.enabled = false;
		removeButton.GetComponent<Image> ().enabled = false;
		removeButton.enabled = false;
		playerInventory = null;
	}

	public void InvokeRemoveItem(){
		if (playerInventory != null) {
			playerInventory.Remove (item);
		}
	}

	public void InvokeUseItem(){
		if (item != null) {
			item.Use ();
		}
	}
}
