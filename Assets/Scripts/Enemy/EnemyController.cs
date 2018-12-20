using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
	public float lookRadius = 10f;

	Transform target;
	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		target = PlayerManager.ins.player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		float _distance = Vector3.Distance (
			transform.position, 
			target.position
		);

		if (_distance < lookRadius) {
			agent.SetDestination (target.position);

			if (_distance <= agent.stoppingDistance) {
				FaceTarget ();
			}
		}
	}

	void FaceTarget(){
		Vector3 _dir = (target.position - transform.position);
		_dir.y = 0;
		Quaternion _dirQ = Quaternion.LookRotation (_dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, _dirQ, Time.deltaTime);
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}
}
