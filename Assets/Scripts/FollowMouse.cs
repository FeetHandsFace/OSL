using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {

	Transform myTransform;
	float width, height;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		width = GetComponent<RectTransform>().sizeDelta.x * 0.75f;
		height = GetComponent<RectTransform>().sizeDelta.y * 0.75f;
	}
	
	// Update is called once per frame
	void Update () {
		myTransform.position = new Vector3(Input.mousePosition.x + width, Input.mousePosition.y + height);	
	}
}
