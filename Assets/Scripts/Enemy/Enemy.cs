using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
	CharacterStats myStats;

	void Start(){
		myStats = GetComponent<CharacterStats> ();
	}

	public override void Interact(){
		base.Interact ();
		CharacterCombat _playerCombat 
			= PlayerManager.ins.player.GetComponent<CharacterCombat> ();
		//Debug.Log(_playerCombat);
		if (_playerCombat != null) {
			_playerCombat.Attack (myStats);
		}
	}
}
