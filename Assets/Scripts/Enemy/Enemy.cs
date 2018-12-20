using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
	PlayerManager playerManager;
	CharacterStats myStats;

	void Start(){
		playerManager = PlayerManager.ins;
		myStats = GetComponent<CharacterStats> ();
	}

	public override void Interact(){
		base.Interact ();
		CharacterCombat _playerCombat 
			= playerManager.GetComponent<CharacterCombat> ();
		if (_playerCombat != null) {
			_playerCombat.Attack (myStats);
		}
	}
}
