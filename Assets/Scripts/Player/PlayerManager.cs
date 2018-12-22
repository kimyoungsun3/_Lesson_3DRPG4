using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
	#region singletone
	public static PlayerManager ins;

	void Awake(){
		ins = this;
	}
	#endregion

	public GameObject player;

	public void KillPlayer()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
