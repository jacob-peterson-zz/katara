using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

	  public GameObject target;
		private Vector3 height;

    public float distance = 5.0f;
    public float rotationalSpeed = 5.0f; //player rotation speed
    public float orbitSpeed = 120.0f; //camera orbit speed

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 10f;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start ()
    {
        //get target and set the appropriate height
        target = transform.parent.gameObject;
    }

    void LateUpdate ()
    {
        height = target.transform.lossyScale;
        height.x = 0;
        height.y *= 2;
        height.z = 0;

        float rSpeed = rotationalSpeed * Time.timeScale;

        float oSpeed = orbitSpeed * Time.timeScale;

        //free look vs the normal camera
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            //orbit camera horizontally
            x += Input.GetAxis("Mouse X") * oSpeed * distance * 0.02f;
        }
        else
        {
            //rotate player as mouse moves left to right
            x = Input.GetAxis("Mouse X") * rSpeed;
            target.transform.Rotate(0, x, 0);

            //calculate desired camera angle
            x = target.transform.eulerAngles.y;
        }

		//orbit camera vertically
        y -= Input.GetAxis("Mouse Y") * oSpeed * 0.02f;

        //limit how high and low camera can go
        y = ClampAngle(y, yMinLimit, yMaxLimit);

        //calculate rotation of camera
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        //set distance adjustable via mouse wheel
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel")*5, distanceMin, distanceMax);

        //position camera the distance away from the player taking rotation into account
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + (target.transform.position + height);

        //apply rotation and position to camera
        transform.rotation = rotation;
        transform.position = position;
    }

    //simple clamp function, used to limit angle
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
