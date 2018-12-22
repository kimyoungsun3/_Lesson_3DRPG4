using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public Stat armor;

	void Awake(){
		currentHealth = maxHealth;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			TakeDamage (10);
		}
	}

	public void TakeDamage(int _damage){
		Debug.Log("_damage:" + _damage);
		_damage -= armor.GetValue ();
		_damage = Mathf.Clamp (_damage, 0, int.MaxValue);

		currentHealth -= _damage;
		Debug.Log (transform.name + " takes " + _damage + " damages");

		if (currentHealth <= 0) {
			Die ();
		}
	}

	public virtual void Die(){
		//Die in some way
		//This method is meant to be overwritten.
		Debug.Log ("Die " + gameObject.name);
	}
}
