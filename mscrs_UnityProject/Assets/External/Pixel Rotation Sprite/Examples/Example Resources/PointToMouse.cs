using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToMouse : MonoBehaviour {

    public bool limitRotation = false;
    public float minAngle, maxAngle;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 p = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        p.z = transform.position.z;

        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(p.y - transform.position.y, p.x - transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        if(limitRotation)
            AngleDeg = Mathf.Clamp(AngleDeg, minAngle, maxAngle);
        transform.localRotation = Quaternion.Euler(0, 0, AngleDeg);


    }
}
