using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
	#region singletone
	public static PlayerManager ins;

	void Awake(){
		ins = this;
	}
	#endregion

	public GameObject player;
}
