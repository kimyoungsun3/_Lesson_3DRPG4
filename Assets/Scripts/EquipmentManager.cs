using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
	#region SingleTon
	public static EquipmentManager ins { get; private set; }
	void Awake()
	{
		ins = this;
	}
	#endregion

	//획득한 아이템을 장착...
	public Equipment[] defaultItems;
	public SkinnedMeshRenderer targetMesh;
	Equipment[] currentEquipment;
	SkinnedMeshRenderer[] currentMeshes;
	public delegate void OnEquipmentChanged (Equipment _newItem, Equipment _oldItem);
	public OnEquipmentChanged onEquipmentChanged;


	void Start(){
		int _len = System.Enum.GetNames (typeof(EquipmentSlot)).Length;
		currentEquipment 	= new Equipment[_len];
		currentMeshes 		= new SkinnedMeshRenderer[_len];

		//Debug.Log( currentEquipment.Length + ":" + currentEquipment[0] );
		EquipDefaultItems (); //기본 아이템 장착.
	}

	//기본 아이템 장착.
	void EquipDefaultItems(){
		foreach (Equipment _item in defaultItems) {
			Equip (_item);
		}
	}

	//아이템 장착.
	public void Equip(Equipment _newItem){
		int _idx = (int)_newItem.equipSlot;

		//동일 아이템 파트 교체시 다시 돌아오기.
		Equipment _oldItem = Unequip(_idx);

		//아이템 장착, 해제에 따른 능력치 변경.
		if (onEquipmentChanged != null) {
			onEquipmentChanged (_newItem, _oldItem);
		}

		//아이템 장착.
		currentEquipment [_idx] = _newItem;

		//메쉬장착(파트를 생성해서 연결해줌).
		SkinnedMeshRenderer _newMesh = Instantiate<SkinnedMeshRenderer>(_newItem.mesh);
		_newMesh.transform.SetParent (targetMesh.transform);
		_newMesh.bones = targetMesh.bones;
		_newMesh.rootBone = targetMesh.rootBone;
		currentMeshes [_idx] = _newMesh;
		
		//장착에 따른 SkinnedMeshRenderer 사이즈 조절.
		SetEquipmentBlendShapes(_newItem, 100);
	}

	//아이템 해제.
	public Equipment Unequip(int _idx){
		Equipment _oldItem = null;
		//Debug.Log(_idx + " > [" + currentEquipment[_idx] + "]");
		if (currentEquipment [_idx] != null) {
			_oldItem = currentEquipment [_idx];
			PlayerInventory.ins.Add (_oldItem);

			//아이템 장착, 해제에 따른 능력치 변경.
			if (onEquipmentChanged != null) {
				onEquipmentChanged (null, _oldItem);
			}

			//해제.
			currentEquipment [_idx] = null;

			//보이는 아이템 해제.
			if (currentMeshes [_idx] != null) {
				Destroy (currentMeshes [_idx].gameObject);
			}

			//해제에 따른 SkinnedMeshRenderer의 사이즈 원복.
			SetEquipmentBlendShapes(_oldItem, 0);
		}

		return _oldItem;
	}

	public void UnequipAll(){
		for (int i = 0; i < currentEquipment.Length; i++) {
			Unequip (i);
		}

		//모든 아이템 벗고 기본 아이템 장착.
		EquipDefaultItems();
	}

	//SkinnedMeshRendere의 BlendShape를 사이즈 조절.
	void SetEquipmentBlendShapes(Equipment _item, int _weight){
		foreach (EquipmentMeshRegion _blendShape in _item.coveredMeshRegions) {
			targetMesh.SetBlendShapeWeight ((int)_blendShape, _weight);
		}
	}

#if UNITY_EDITOR
	void Update(){
		if (Input.GetKeyDown (KeyCode.U)) {
			UnequipAll ();
		}
	}
#endif
}
