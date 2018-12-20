using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {
	NavMeshAgent agent;
	Transform target;
	Vector3 beforePos;
	float nextTime;
	public float NEXT_TIME = 0.2f;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update(){
		if (target != null) {
			if (Time.time > nextTime && beforePos != target.position) {
				nextTime = Time.time + NEXT_TIME;
				//Debug.Log (" move > ");
				agent.SetDestination (target.position);
				beforePos = target.position;
			}

			FaceTarget ();
		}
	}

	public void MoveToPoint(Vector3 _point){
		agent.SetDestination (_point);
	}

	Vector3 oldPos;
	void FaceTarget(){
		/*
		if (!agent.updateRotation && oldPos != transform.position) {
			Vector3 _dir = transform.position - oldPos;
			_dir.y = transform.position.y;
			Quaternion _q = Quaternion.LookRotation (_dir);
			transform.rotation = Quaternion.Slerp (transform.rotation, _q, Time.deltaTime * 5f);
		}
		oldPos = transform.position;
		*/

		if (!agent.updateRotation) {
			Vector3 _dir = agent.velocity;
			if (_dir.magnitude > 0.1f) {
				Quaternion _q = Quaternion.LookRotation (_dir);
				transform.rotation = Quaternion.Slerp (transform.rotation, _q, Time.deltaTime * 5f);
			}
		}

		/*
		if (!agent.updateRotation) {
			Vector3 _dir = target.position - transform.position;
			_dir.y = 0;
			Quaternion _q = Quaternion.LookRotation (_dir);
			transform.rotation = Quaternion.Slerp (transform.rotation, _q, Time.deltaTime * 5f);
		}
		*/
	}

	public void FollowTarget(Interactable _newTarget){
		//전방에서 멈추기... 회전을 직접제어.
		agent.stoppingDistance 	= _newTarget.radius * 0.8f;
		agent.updateRotation 	= false;

		target = _newTarget.transform;
	}

	public void StopTarget(){
		//agent가 제하기.
		agent.stoppingDistance 	= 0f;
		agent.updateRotation 	= true;

		target = null;
	}

}
