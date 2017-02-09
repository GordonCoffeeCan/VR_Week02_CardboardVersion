using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//to access unity's VR functions, add the namespace;
using UnityEngine.VR;

public class MosueLook : MonoBehaviour {
    private Transform _camTrans;

	// Use this for initialization
	void Start () {
        _camTrans = Camera.main.transform;

        if (VRDevice.isPresent == true) {
            //if so, print vr HMD model name to the console;
            Debug.Log("VR is enabled!" + VRDevice.model);
        } else {
            //move camera up if vr isn't enabled;
            Camera.main.transform.Translate(0, 2, 0);
        }

        //render at 50% of the original quality
        VRSettings.renderViewportScale = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if (VRDevice.isPresent == false) {
            //do backup emergency input code only if VR isn't working
            
            //rotate camera based on mouse delta speed;
            _camTrans.Rotate(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            //unroll our camera
            _camTrans.localEulerAngles = new Vector3(_camTrans.localEulerAngles.x, _camTrans.localEulerAngles.y, 0);
        }

        //write whatever code you want

	}
}
