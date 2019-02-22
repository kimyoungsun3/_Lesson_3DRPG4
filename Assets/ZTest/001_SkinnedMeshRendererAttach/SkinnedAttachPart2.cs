using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedTest2
{	
	public class Item
	{
		//new public string name;
		//public int itemcode;
	}

	public enum ENUM_EQUIPMENT_PART { Head, Body, Leg, Shoes, Sword, Shield }
	[System.Serializable]
	public class Item_Equipment : Item
	{
		//착용아이템파트, 스킨, 정보들....
		public ENUM_EQUIPMENT_PART partKind;
		public SkinnedMeshRenderer skin;
	}

	//[System.Serializable]
	//public class Item_Custome : Item
	//{
	//	//소모템의 아이템들....
	//}

	public class SkinnedAttachPart2 : MonoBehaviour
	{
		//가지고 있는 모든 아이템 리스트 정보.
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

			//Debug.Log(listWear[0]);
		}

		// Update is called once per frame
		void Update()
		{
			//Debug.Log(listWear[0]);

			if (Input.GetMouseButtonDown(0))
			{
				//item list -> 찾아서 -> 어느파트인가?
				int _newListIdx			= Random.Range(0, list_ItemInfo.Count);
				Item_Equipment _newItem	= list_ItemInfo[_newListIdx];
				SkinnedMeshRenderer _newMesh;
				int _partKind			= ((int)_newItem.partKind);

				Item_Equipment _oldItem		= wearItem[_partKind];
				SkinnedMeshRenderer _oldMesh = wearMesh[_partKind];

				//old expire and new item instantiate
				if (_oldMesh != null)
				{
					Destroy(_oldMesh.gameObject);
				}

				//Skinned is Instantiate<T> and Bone Link...
				_newMesh					= Instantiate<SkinnedMeshRenderer>(_newItem.skin);
				_newMesh.rootBone			= skinBody.rootBone;
				_newMesh.bones				= skinBody.bones;
				_newMesh.transform.SetParent(skinBody.transform);

				//착용아이템의 정보를 링크연결하기...
				wearItem[_partKind] = _newItem;
				wearMesh[_partKind] = _newMesh;
			}
		}
	}
}
