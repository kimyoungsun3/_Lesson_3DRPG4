using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
	public float attackSpeed = 1f;
	float attackCooldown = 0f;
	public float attackDelay = 0.6f;

	CharacterStats myStats;

	const float COMBAT_COOL_DOWN = 5f;
	float lastAttackTime;
	public bool bCombat { get; private set; }
	public System.Action onAttack; 

    void Start()
    {
		myStats = GetComponent<CharacterStats> ();    
    }

	private void Update()
	{
		attackCooldown -= Time.deltaTime;

		//전투자세 시간이 완료되면 자동해제.
		if(Time.time - lastAttackTime > COMBAT_COOL_DOWN)
		{
			bCombat = false;
		}
	}

	//상대에게 데미지 가하기.
	public void Attack(CharacterStats _targetStats)
	{
		if (attackCooldown <= 0f)
		{
			Debug.Log(_targetStats.gameObject.name + " -> " + gameObject.name + " Attact " + _targetStats.damage.GetValue());
			//_targetStats.TakeDamage(myStats.damage.GetValue());
			StartCoroutine(DoDamager(_targetStats, attackDelay));
			attackCooldown = 1f / attackSpeed;
		}
	}



	IEnumerator DoDamager(CharacterStats _targetStats, float _delay)
	{
		//전투 & 자세.
		bCombat = true;
		lastAttackTime = Time.time;
		if(onAttack != null)
		{
			onAttack();
		}

		yield return new WaitForSeconds(_delay);
		_targetStats.TakeDamage(myStats.damage.GetValue());

		//적이 죽으면 전투자세 해제.
		if(_targetStats.currentHealth <= 0)
		{
			bCombat = false;
		}
	}
}
