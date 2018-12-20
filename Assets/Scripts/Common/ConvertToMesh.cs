using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToMesh : MonoBehaviour {
	//SkinnedMeshRenderer -> MeshFilter, MeshRenderer
	[ContextMenu("Convert SkinnedMeshRender To Mesh")]
	public void Convert(){
		SkinnedMeshRenderer _skinned = GetComponent<SkinnedMeshRenderer> ();
		MeshFilter _meshFilter 		= gameObject.AddComponent<MeshFilter> ();
		MeshRenderer _meshRenderer 	= gameObject.AddComponent<MeshRenderer> ();

		//SkinnedMeshRenderer -> MeshFilter, MeshRenderer
		_meshFilter.sharedMesh 			= _skinned.sharedMesh;
		_meshRenderer.sharedMaterials 	= _skinned.sharedMaterials;

		DestroyImmediate (_skinned);
		DestroyImmediate (this);
	}
}
