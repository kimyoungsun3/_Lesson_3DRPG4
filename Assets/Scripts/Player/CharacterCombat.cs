using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
	CharacterStats myStats;

    void Start()
    {
		myStats = GetComponent<CharacterStats> ();    
    }

	//상대에게 데미지 가하기.
	public void Attack(CharacterStats _targetStats){
		_targetStats.TakeDamage (myStats.damage.GetValue());
	}
}
