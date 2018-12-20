using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraMobile : MonoBehaviour {
	public Vector2 ANGLE_MIN_MAX = new Vector2 (5f, +65f);
	public Vector3 DISTANCE_MIN_MAX = new Vector2 (3f, 15f);

	public Transform target;
	Vector3 targetPosOld;

	Transform trans;
	float distance = 10f;
	float angleX, angleY;
	public float sensivityY = 25.0f;
	public float sensivityX = 25.0f;
	public float sensivityW = 1.0f;
	Quaternion dirQ;
	bool bMove;

	void Start () {
		if (Application.platform != RuntimePlatform.Android) {
			enabled = false;
		}

		trans = transform;
		trans.LookAt (target);

		angleY = trans.eulerAngles.y;
		angleX = trans.eulerAngles.x;
		bMove = true;
	}

	int touchCount;
	Touch t0, t1;
	Vector2 t0OldPos, t1OldPos;
	float distanceOld, distanceCur, deltaDistance;
	void Update () {
		touchCount = Input.touchCount;
		if (touchCount > 0) {
			t0 = Input.GetTouch (0);
			if (touchCount == 1) {
				if (t0.deltaPosition != Vector2.zero) {
					bMove = true;
					angleY += t0.deltaPosition.x * sensivityX * Time.deltaTime;
					angleX -= t0.deltaPosition.y * sensivityY * Time.deltaTime;
					//Debug.Log (t0.deltaPosition + ":" + currentX + ":"+ currentY);

					angleX = Mathf.Clamp (angleX, ANGLE_MIN_MAX.x, ANGLE_MIN_MAX.y);
				}
			} else if (Input.touchCount == 2) {
				t1 = Input.GetTouch (1);
				if (t0.deltaPosition != Vector2.zero || t1.deltaPosition != Vector2.zero) {
					bMove = true;

					t0OldPos = t0.position - t0.deltaPosition;
					t1OldPos = t1.position - t1.deltaPosition;

					distanceOld = Vector2.Distance (t0OldPos, t1OldPos);
					distanceCur = Vector2.Distance (t0.position, t1.position);
					deltaDistance = distanceOld - distanceCur;
					//Debug.Log (t0.deltaPosition + ":" + t1.deltaPosition);
					//Debug.Log (deltaDistance + ":" + (t0.deltaPosition + t1.deltaPosition).magnitude);

					distance += deltaDistance * sensivityW * Time.deltaTime;
					distance = Mathf.Clamp (distance, DISTANCE_MIN_MAX.x, DISTANCE_MIN_MAX.y);
				}
			}
		}
	}

	void LateUpdate(){
		if (bMove || target.position != targetPosOld) {
			bMove = false;
			dirQ = Quaternion.Euler (angleX, angleY, 0);
			trans.position = target.position + dirQ * Vector3.back * distance;
			trans.LookAt (target.position + Vector3.up * 1f);
			//Debug.Log(Vector3.Distance(target.position, trans.position));
		}
		targetPosOld = target.position; 
	}
}
