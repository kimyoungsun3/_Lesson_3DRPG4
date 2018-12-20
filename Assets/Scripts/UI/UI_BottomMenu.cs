using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BottomMenu : MonoBehaviour {

	public UI_Inventory uiInventory;

	public void InvokeUI_Inventory(){
		uiInventory.gameObject.SetActive (!uiInventory.gameObject.activeSelf);
	}
}
