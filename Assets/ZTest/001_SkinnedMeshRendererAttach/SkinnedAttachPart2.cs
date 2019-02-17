using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedTest2
{
	public enum ENUM_EQUIPMENT_PART { Head, Body, Leg, Shoes, Sword, Shield }
	[System.Serializable]
	public class ItemInfo
	{
		public ENUM_EQUIPMENT_PART partKind;
		public SkinnedMeshRenderer skin;
		//public bool bDefault = false;
		//public int def = 0;
		//public int att = 0;
	}

	//[System.Serializable]
	//public class EquipmentInfo
	//{
	//	public ItemInfo item;
	//}

	public class SkinnedAttachPart2 : MonoBehaviour
	{
		public SkinnedMeshRenderer skinBody;
		public List<ItemInfo> listItem = new List<ItemInfo>();

		[Header("Current Wearing Item(showing)")]
		public ItemInfo[] wearParts;

		void Start()
		{
			int _size = System.Enum.GetNames(typeof(ENUM_EQUIPMENT_PART)).Length;
			wearParts = new ItemInfo[_size];
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				//item list -> 찾아서 -> 어느파트인가?
				int _idxListItem = Random.Range(0, listItem.Count);
				ItemInfo _wearListItem = listItem[_idxListItem];
				ItemInfo _wearCurrent = wearParts[((int)_wearListItem.partKind)];

				if (_wearCurrent.skin != null)
				{
					Destroy(_wearCurrent.skin.gameObject);
				}

				//Skinned is Instantiate<T> and Bone Link...
				_wearCurrent.skin = Instantiate<SkinnedMeshRenderer>(listItem[_idxListItem].skin);
				_wearCurrent.skin.rootBone		= skinBody.rootBone;
				_wearCurrent.skin.bones			= skinBody.bones;
				_wearCurrent.skin.transform.SetParent(skinBody.transform);
			}
		}
	}
}
