using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
	[SerializeField]
	private int baseValue;
	private List<int> modifiers = new List<int>();

	//리스트의 내용을 전부 합해서 계산해준다...
	//음... 실시간으로 되어 있는것 나중에 계산해두는 방식으로 변경.
	public int GetValue(){
		int _finalValue = baseValue;
		modifiers.ForEach(_x => _finalValue += _x);
		return _finalValue;
	}

	public void AddModifier(int _v){
		if (_v != 0) {
			modifiers.Add (_v);
		}
	}

	public void RemoveModifier(int _v){
		if (_v != 0) {
			modifiers.Remove (_v);
		}
	}
}
