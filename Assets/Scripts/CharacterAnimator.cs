using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour {
	NavMeshAgent agent;
	public Animator animator;
	const float LOCOMATION_ANIMATION_SMOOTHTIME = 0.1f;
	int speedPercentHash;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		speedPercentHash = Animator.StringToHash ("SpeedPercent");
	}
	
	// Update is called once per frame
	void Update () {
		float _percent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat (speedPercentHash, _percent, LOCOMATION_ANIMATION_SMOOTHTIME, Time.deltaTime);
	}
}
