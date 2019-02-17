using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedTest3
{
	public enum ENUM_EQUIPMENT_PART { Head, Body, Leg, Shoes, Sword, Shield }
	[System.Serializable]
	public class ItemInfo
	{
		public ENUM_EQUIPMENT_PART partKind;
		public SkinnedMeshRenderer skin;
		public bool bDefault;
		//public int def = 0;
		//public int att = 0;
	}

	[System.Serializable]
	public struct EquipmentInfo
	{
		public ItemInfo item;
	}

	public class SkinnedAttachPart3 : MonoBehaviour
	{
		public SkinnedMeshRenderer skinBody;
		public List<ItemInfo> listItem = new List<ItemInfo>();

		[Header("Current Wearing Item(showing)")]
		public EquipmentInfo[] wearParts;

		void Start()
		{
			int _size = System.Enum.GetNames(typeof(ENUM_EQUIPMENT_PART)).Length;
			wearParts = new EquipmentInfo[_size];

			//Debug.Log("Start:" + wearParts[0]);
			WearDefault();
		}

		void WearDefault()
		{
			for (int i = 0, iMax = listItem.Count; i < iMax; i++)
			{
				if(listItem[ i].bDefault)
				{
					WearItem(i);
				}
			}
		}

		void WearItem(int _idxListItem)
		{
			ItemInfo _wearListItem = listItem[_idxListItem];
			ItemInfo _wearCurrent = wearParts[((int)_wearListItem.partKind)].item;
			Debug.Log(
				_idxListItem 
				+ ":" + ((int)_wearListItem.partKind)
				+ ":" + _wearListItem
				+ ":" + wearParts.Length
				+ ":" + wearParts[0]
				+ ":" + _wearCurrent
				);

			//기존것 삭제...
			if (_wearCurrent.skin != null)
			{
				Destroy(_wearCurrent.skin.gameObject);
			}

			//새것 장착...
			//Skinned is Instantiate<T> and Bone Link...
			_wearCurrent.skin = Instantiate<SkinnedMeshRenderer>(listItem[_idxListItem].skin);
			_wearCurrent.skin.rootBone = skinBody.rootBone;
			_wearCurrent.skin.bones = skinBody.bones;
			_wearCurrent.skin.transform.SetParent(skinBody.transform);
		}

		// Update is called once per frame
		void Update()
		{

			Debug.Log("Update:" + wearParts[0]);
			if (Input.GetMouseButtonDown(0))
			{
				//item list -> 찾아서 -> 어느파트인가?
				int _idxListItem = Random.Range(0, listItem.Count);
				WearItem(_idxListItem);

				//ItemInfo _wearListItem = listItem[_idxListItem];
				//ItemInfo _wearCurrent = wearParts[((int)_wearListItem.partKind)];
				//
				//if (_wearCurrent.skin != null)
				//{
				//	Destroy(_wearCurrent.skin.gameObject);
				//}
				//
				//Skinned is Instantiate<T> and Bone Link...
				//_wearCurrent.skin = Instantiate<SkinnedMeshRenderer>(listItem[_idxListItem].skin);
				//_wearCurrent.skin.rootBone		= skinBody.rootBone;
				//_wearCurrent.skin.bones			= skinBody.bones;
				//_wearCurrent.skin.transform.SetParent(skinBody.transform);
			}
		}
	}
}
