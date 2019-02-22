using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedTest{
	//public enum ENUM_EQUIPMENT_PART { Head, Body, Leg, Sword, Shield }
	
	public class SkinnedAttach : MonoBehaviour
	{
		public SkinnedMeshRenderer skBody;
		public List<SkinnedMeshRenderer> listSKParts = new List<SkinnedMeshRenderer>();
		SkinnedMeshRenderer skPart;
		int skIdx = 0;

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if(skPart != null)
				{
					Destroy(skPart.gameObject);
				}

				//Skinned is Instantiate<T> and Bone Link...
				skPart = Instantiate<SkinnedMeshRenderer>(listSKParts[skIdx]);
				//skPart.transform.position = Vector3.up * 10f;
				skPart.rootBone		= skBody.rootBone;
				skPart.bones		= skBody.bones;
				skPart.transform.SetParent(skBody.transform);

				skIdx++;
				skIdx = skIdx >= listSKParts.Count ? 0 : skIdx;
			}
		}
	}
}
