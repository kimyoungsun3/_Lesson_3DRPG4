using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {
	public float radius = 3f;
	public Transform interactionTransform;

	protected Transform player;
	bool bInteracted = false;

	public void OnFocused(Transform _player){
		player 		= _player;
		bInteracted = false;
	}

	public void OnDefocused(){
		player 		= null;
		bInteracted = false;
	}

	public virtual void Interact(){
		//Debug.Log ("Inacterable");
	}

	void Update(){
		//Debug.Log (11);
		if (player != null && !bInteracted) {
			float _distance = Vector3.Distance (interactionTransform.position, player.position);
			if (_distance <= radius) {
				//Debug.Log ("INTERACT");
				bInteracted = true;
				Interact ();
			}
		}
	}

	void OnDrawGizmosSelected(){
		if (interactionTransform == null) {
			interactionTransform = transform;
		}
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (interactionTransform.position, radius);
	}
}
