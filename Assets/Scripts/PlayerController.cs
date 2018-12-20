//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Camera cam;
	public LayerMask maskGround;
	PlayerMotor motor;
	public Interactable focus;
	int pointerId;

	// Use this for initialization
	void Start () {
		cam 	= Camera.main;
		motor 	= GetComponent<PlayerMotor> ();
		#if UNITY_EDITOR || UNITY_STANDALONE
		pointerId = -1;
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_IPHONE
		pointerId = 0;
		#endif
	}
	
	// Update is called once per frame
	void Update () {
		if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject (
			pointerId)) 
		{
			return;
		}

		if (Input.GetMouseButtonDown (0)) {
			Ray _ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit _hit;
			if (Physics.Raycast (_ray, out _hit, 100f, maskGround)) {
				//Debug.Log ("Hit " + _hit.collider.name + ":" + _hit.point);
				motor.MoveToPoint(_hit.point);

				Interactable _scp = _hit.collider.GetComponent<Interactable> ();
				if (_scp != null) {
					//Debug.Log (_hit.point + ":" + _scp.transform.position);
					SetFocus (_scp);
				} else {
					RemoveFocus ();
				}
			}
		}		
	}

	void SetFocus(Interactable _focus){
		if (_focus != null && focus == _focus) {
			return;
		}

		if (focus != null) {
			focus.OnDefocused ();
		}
		focus = _focus;
		focus.OnFocused (transform);

		motor.FollowTarget (_focus);
	}

	void RemoveFocus(){
		if (focus != null) {
			focus.OnDefocused ();
		}
		focus = null;

		motor.StopTarget();
	}
}
