using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
	const float LOCOMATION_ANIMATION_SMOOTHTIME = 0.1f;

	public AnimationClip replaceableAttackAnim;			//
	public AnimationClip[] defaultAttackAnimSet;		//기본 공격.
	protected AnimationClip[] currentAttackAnimSet;		//현재 적용된 공격.

	NavMeshAgent agent;
	public Animator animator;
	int speedPercentHash;
	protected CharacterCombat combat;
	protected AnimatorOverrideController overrideController;

	// Use this for initialization
	protected virtual void Start () {
		agent = GetComponent<NavMeshAgent> ();
		speedPercentHash = Animator.StringToHash ("SpeedPercent");
		combat = GetComponent<CharacterCombat>();

		//AC을 카피한 실시간으로 AOC 만들어둔다.
		overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
		animator.runtimeAnimatorController = overrideController;

		currentAttackAnimSet = defaultAttackAnimSet;
		combat.onAttack += OnAttack;
	}

	protected virtual void OnAttack()
	{
		animator.SetTrigger("attack");
		int _idx = Random.RandomRange(0, currentAttackAnimSet.Length);
		overrideController[replaceableAttackAnim.name] = currentAttackAnimSet[_idx];
	}

	// Update is called once per frame
	protected virtual void Update () {
		float _percent = agent.velocity.magnitude / agent.speed;
		animator.SetFloat (speedPercentHash, _percent, LOCOMATION_ANIMATION_SMOOTHTIME, Time.deltaTime);

		//전투자세 애니...
		animator.SetBool("bCombat", combat.bCombat);
	}
}
