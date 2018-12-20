using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
	public static PlayerInventory ins;
	void Awake(){
		ins = this;
	}

	public int size = 20;
	public List<Item> items = new List<Item>();
	public VOID_FUN_VOID onItemChange;


	public void RegisterEvent(VOID_FUN_VOID _on){
		onItemChange += _on;
	}

	public bool Add(Item _item){
		bool _rtn = false;
		if (!_item.bDefaultItme) {
			if (items.Count < size) {
				items.Add (_item);
				if (onItemChange != null) {
					onItemChange ();
				}
				_rtn = true;
			}
		}
		return _rtn;
	}

	public void Remove(Item _item){
		items.Remove (_item);
		if (onItemChange != null) {
			onItemChange ();
		}
	}
}
