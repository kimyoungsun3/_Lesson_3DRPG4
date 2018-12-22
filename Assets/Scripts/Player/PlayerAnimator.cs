using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
	[System.Serializable]
	public struct WeaponAnimations
	{
		public Equipment weapon;
		public AnimationClip[] clips;
	}
	public WeaponAnimations[] weaponAnimations;
	Dictionary<Equipment, AnimationClip[]> weaponAnimationsDic 
		= new Dictionary<Equipment, AnimationClip[]>();
	
    // Start is called before the first frame update
    protected override void Start()
    {
		base.Start();
		EquipmentManager.ins.onEquipmentChanged += OnEquipmentChanged;

		foreach(WeaponAnimations _v in weaponAnimations)
		{
			weaponAnimationsDic.Add(_v.weapon, _v.clips);
		}
    }

	void OnEquipmentChanged(Equipment _newItem, Equipment _oldItem)
	{
		//오른손 무기 장착/해제.
		if (_newItem != null && _newItem.equipSlot == EquipmentSlot.Weapon)
		{
			animator.SetLayerWeight(2, 1);

			//등록된 애니셑으로 교체.
			if (weaponAnimationsDic.ContainsKey(_newItem))
			{
				currentAttackAnimSet = weaponAnimationsDic[_newItem];
			}
		}
		else if (_newItem == null && _oldItem != null && _oldItem.equipSlot == EquipmentSlot.Weapon)
		{
			//해제 -> 기본애니, 기본 애니동작셑으로 교체.
			animator.SetLayerWeight(2, 0);
			currentAttackAnimSet = defaultAttackAnimSet;
		}

		//왼손 방패 장착/해제.
		if (_newItem != null && _newItem.equipSlot == EquipmentSlot.Shield)
		{
			animator.SetLayerWeight(1, 1f);
		}
		else if (_newItem == null && _oldItem != null && _oldItem.equipSlot == EquipmentSlot.Shield)
		{
			animator.SetLayerWeight(1, 0f);
		}
	}
}
