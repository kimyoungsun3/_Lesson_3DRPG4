using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThirdPersonCameraPC : MonoBehaviour {
	public Vector2 ANGLE_MIN_MAX = new Vector2 (-60f, +80f);
	public Vector3 DISTANCE_MIN_MAX = new Vector2 (1f, 15f);
	public float CLAMPXY = 4f;

	public Transform target;
	Vector3 targetPosOld;

	Transform trans;
	float distance = 10f;
	float angleX, angleY;
	public float sensivityMouseY = 4.0f;
	public float sensivityMouseX = 4.0f;
	public float sensivityMouseW = 10.0f;
	Quaternion dirQ;
	float wheel;
	bool bMove;
	int mouseButton = 1;

	void Start(){		
		if (Application.platform == RuntimePlatform.Android) {
			UnityEngine.PostProcessing.PostProcessingBehaviour _p = GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour> ();
			if (_p != null) {
				_p.enabled = false;
			}
			enabled = false;
		}
		trans = transform;
		trans.LookAt (target);

		angleY = trans.eulerAngles.y;
		angleX = trans.eulerAngles.x;
		//if (angleX >= 180f) {
		//	angleX = 360f - angleX;
		//}
		bMove = true;
	}

	void Update(){
		//Debug.Log (" ------------");
		//x,y
		if (Input.GetMouseButton (mouseButton)) {
			bMove = true;
			float _dx = Input.GetAxis ("Mouse Y") * sensivityMouseY;
			float _dy = Input.GetAxis ("Mouse X") * sensivityMouseX;
			//Debug.Log (" > " + _dx + ":" + _dy);

			//Android 폰에서 값이 많이 튀는듯....
			_dx = Mathf.Clamp (_dx, -CLAMPXY, CLAMPXY);
			_dy = Mathf.Clamp (_dy, -CLAMPXY, CLAMPXY);

			angleX -= _dx;
			angleY += _dy;
			if (_dx != 0f) {
				angleX = Mathf.Clamp (angleX, ANGLE_MIN_MAX.x, ANGLE_MIN_MAX.y);
			}

			if (_dx == 0f && _dy == 0f) {
				bMove = false;
			}
		}

		//wheel. 앞(+), 뒤(-)
		wheel = Input.GetAxis ("Mouse ScrollWheel");
		if (wheel != 0f) {
			bMove = true;
			distance -= wheel * sensivityMouseW;
			distance = Mathf.Clamp (distance, DISTANCE_MIN_MAX.x, DISTANCE_MIN_MAX.y);
		}
	}


	void LateUpdate(){
		//move and look.
		if (bMove || target.position != targetPosOld) 
		{
			bMove = false;
			dirQ = Quaternion.Euler (angleX, angleY, 0);
			//Vector3 _pos = target.position + dirQ * Vector3.back * distance;
			//trans.position = Vector3.Slerp (trans.position, _pos, Time.deltaTime * 2f);
			trans.position = target.position + dirQ * Vector3.back * distance;
			trans.LookAt (target.position + Vector3.up * 1f);
		}
		targetPosOld = target.position; 		
	}
}
