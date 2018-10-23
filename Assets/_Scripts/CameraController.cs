using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private void Update () {
	    var mouseY = (Input.mousePosition.y / Screen.height) - 0.7f;
	    transform.localRotation =
	        Quaternion.Euler(new Vector4(-1f * (mouseY * 90f), 0f, transform.localRotation.z));
	}
}
