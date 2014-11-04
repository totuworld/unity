using UnityEngine;
using System.Collections;

public class DragMap : MonoBehaviour {

	public Camera mainCamera;
	Vector3 mouseCurrentPos, mouseLastPos, mouseDeltaPos;
	void OnDrag()
	{
		mouseCurrentPos = Input.mousePosition;
		mouseDeltaPos = mouseLastPos!=null? mouseCurrentPos - mouseLastPos:Vector3.zero;
		mouseDeltaPos = mouseDeltaPos.normalized * 0.1f;
		mouseLastPos = mouseCurrentPos;
		
		mainCamera.transform.position += mouseDeltaPos;
	}
}
