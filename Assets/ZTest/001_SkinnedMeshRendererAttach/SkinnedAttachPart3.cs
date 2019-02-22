using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedTest3
{
	public enum ENUM_EQUIPMENT_PART { Head, Body, Leg, Shoes, Sword, Shield }

	public class Item
	{
		new public string name;
		public int itemcode;
		public bool bDefault;
	}

	[System.Serializable]
	public class Item_Equipment : Item
	{
		//착용아이템파트, 스킨, 정보들....
		public SkinnedMeshRenderer skin;
		public ENUM_EQUIPMENT_PART partKind;
		public int def = 0;
		public int att = 0;
	}

	[System.Serializable]
	public class Item_Custome : Item
	{
		//소모템의 아이템들....
		public int hp = 0;
	}

	public class SkinnedAttachPart3 : MonoBehaviour
	{
		public SkinnedMeshRenderer skinBody;
		public List<Item_Equipment> list_ItemInfo = new List<Item_Equipment>();

		//착용하고 아있는 아이템... <=== 절대로 Inspector에 안나와야한다...
		Item_Equipment[] wearItem;
		SkinnedMeshRenderer[] wearMesh;

		void Start()
		{
			int _size	= System.Enum.GetNames(typeof(ENUM_EQUIPMENT_PART)).Length;
			wearItem	= new Item_Equipment[_size];
			wearMesh	= new SkinnedMeshRenderer[_size];

			WearDefault();
		}

		void WearDefault()
		{
			for (int i = 0, iMax = list_ItemInfo.Count; i < iMax; i++)
			{
				if(list_ItemInfo[ i].bDefault)
				{
					WearItem(i);
				}
			}
		}

		void WearItem(int _newListIdx)
		{
			//item list -> 찾아서 -> 어느파트인가?
			Item_Equipment _newItem = list_ItemInfo[_newListIdx];
			SkinnedMeshRenderer _newMesh;
			int _partKind = ((int)_newItem.partKind);

			Item_Equipment _oldItem = wearItem[_partKind];
			SkinnedMeshRenderer _oldMesh = wearMesh[_partKind];

			//기존것 삭제...
			if (_oldMesh != null)
			{
				Destroy(_oldMesh.gameObject);
			}

			//Skinned is Instantiate<T> and Bone Link...
			_newMesh = Instantiate<SkinnedMeshRenderer>(_newItem.skin);
			_newMesh.rootBone = skinBody.rootBone;
			_newMesh.bones = skinBody.bones;
			_newMesh.transform.SetParent(skinBody.transform);

			//착용아이템의 정보를 링크연결하기...
			wearItem[_partKind] = _newItem;
			wearMesh[_partKind] = _newMesh;
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				//item list -> 찾아서 -> 어느파트인가?
				int _idxListItem = Random.Range(0, list_ItemInfo.Count);
				WearItem(_idxListItem);			}
		}
	}
}
