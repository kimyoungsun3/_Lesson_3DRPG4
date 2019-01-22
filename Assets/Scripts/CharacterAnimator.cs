using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
	const float LOCOMATION_ANIMATION_SMOOTHTIME = 0.1f;
	public Animator animator;

	public AnimationClip replaceableAttackAnim;			//
	public AnimationClip[] defaultAttackAnimSet;		//기본 공격.
	protected AnimationClip[] currentAttackAnimSet;		//현재 적용된 공격.

	NavMeshAgent agent;
	int speedPercentHash;
	protected CharacterCombat combat;
	protected AnimatorOverrideController aoc;

	// Use this for initialization
	protected virtual void Start () {
		agent = GetComponent<NavMeshAgent> ();
		speedPercentHash = Animator.StringToHash ("SpeedPercent");
		combat = GetComponent<CharacterCombat>();
		
		//AC을 카피한 실시간으로 AOC 만들어둔다.
		aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = aoc;

		//transform.Translate(Vector3.up);

		currentAttackAnimSet = defaultAttackAnimSet;
		combat.onAttack += OnAttack;
	}

	protected virtual void OnAttack()
	{
		int _idxAttack = Random.RandomRange(0, currentAttackAnimSet.Length);
		aoc[replaceableAttackAnim.name] = currentAttackAnimSet[_idxAttack];
		animator.SetTrigger("attack");
	}

	// Update is called once per frame
	protected virtual void Update () {
		float _percent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat (speedPercentHash, _percent, LOCOMATION_ANIMATION_SMOOTHTIME, Time.deltaTime);

		//전투자세 애니...
		animator.SetBool("bCombat", combat.bCombat);
	}
}
