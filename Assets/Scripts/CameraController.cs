using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;
	public Vector3 offset;
	public float pitch = 2f;
	public float zoomSpeed = 4f;
	public float yawSpeed = 10f;
	public Vector2 zoomMinMax = new Vector2 (5f, 15f);

	float currentZoom = 10f;
	float currentYaw;

	void LateUpdate () {
		currentZoom -= Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp (currentZoom, zoomMinMax.x, zoomMinMax.y);
		if (Input.GetMouseButton (1)) {
			currentYaw += Input.GetAxis ("Mouse X") * yawSpeed * Time.deltaTime;
		} else {
			currentYaw -= Input.GetAxis ("Horizontal") * yawSpeed * Time.deltaTime;
		}

		transform.position = target.position - offset * currentZoom;
		transform.LookAt (target.position + Vector3.up * pitch);

		//transform.RotateAround (target.position, Vector3.up, target.eularAngle.y - 180f);
		transform.RotateAround (target.position, Vector3.up, currentYaw);
	}
}
