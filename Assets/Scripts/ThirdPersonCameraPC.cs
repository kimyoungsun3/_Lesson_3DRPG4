using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraPC : MonoBehaviour {
	public Vector2 ANGLE_MIN_MAX = new Vector2 (5f, +65f);
	public Vector3 DISTANCE_MIN_MAX = new Vector2 (3f, 15f);

	public Transform target;
	Vector3 targetPosOld;

	Transform trans;
	float distance = 10f;
	float distanceBack = -1f;
	float angleX, angleY;
	public float sensivityY = 5.0f;
	public float sensivityX = 5.0f;
	public float sensivityW = 15.0f;
	Quaternion dirQ;
	float wheel;
	bool bMove;

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
		bMove = true;
	}

	void Update(){
		//Debug.Log (" ------------");
		//x,y
		if (Input.GetMouseButton (1)) {
			bMove = true;
			angleY += Input.GetAxis ("Mouse X") * sensivityX;// * Time.deltaTime;
			angleX -= Input.GetAxis ("Mouse Y") * sensivityY;// * Time.deltaTime;
			//Debug.Log (" > " + _dx + ":" + _dy);

			angleX = Mathf.Clamp (angleX, ANGLE_MIN_MAX.x, ANGLE_MIN_MAX.y);
		}

		//wheel. 앞(+), 뒤(-)
		wheel = Input.GetAxis ("Mouse ScrollWheel");
		if (wheel != 0f) {
			bMove = true;
			distance -= wheel * sensivityW;// * Time.deltaTime;
			distance = Mathf.Clamp (distance, DISTANCE_MIN_MAX.x, DISTANCE_MIN_MAX.y);
		}

		/*
		 * 캐릭터 - 장애물(콜라이더)- 카메라
		 * 캐릭터 - 카메라 - 장애물  <- (앞으로땡김)
		 * 
		/**/
		Vector3 _targetPoint = (target.position + Vector3.up * 1f);
		Vector3 _dirBack = trans.position - _targetPoint;
		RaycastHit _hitBack;
		if(Physics.Raycast(_targetPoint, _dirBack, out _hitBack, distance))
		{
			//if(_hitBack.collider.gameObject != target.gameObject)
			{
				if (distanceBack == -1f)
				{
					distanceBack = distance;
				}
				distance = _hitBack.distance;
			}
		}
		else
		{
			if(distanceBack != -1f && !Physics.Raycast(_targetPoint, _dirBack, out _hitBack, distanceBack))
			{
				distance = distanceBack;
				distanceBack = -1f;
			}
		}
	}

	void LateUpdate(){
		//move and look.
		//if (bMove || target.position != targetPosOld) 
		{ 
			bMove = false;
			dirQ = Quaternion.Euler (angleX, angleY, 0);
			//Vector3 _pos = target.position + dirQ * Vector3.back * distance;
			//trans.position = Vector3.Slerp (trans.position, _pos, Time.deltaTime * 2f);
			trans.position = target.position + Vector3.up * 1f - dirQ * Vector3.forward * distance;
			trans.rotation = dirQ;
			//trans.LookAt (target.position + Vector3.up * 1f);
		}
		targetPosOld = target.position; 		
	}
}
