using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour {
    public float rotationSpeed = 8;
    public Animator shotGunAnim;

    public static bool isFire = false;

    private Transform _weaponPivot;
    private Transform _transform;
    private Transform _mainCamera;

    private void Awake() {
        _transform = this.transform;
        _mainCamera = Camera.main.transform;
        _weaponPivot = this.transform.FindChild("WeaponPivot");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        shotGunAnim.SetBool("isFire", isFire);
    }

    private void FixedUpdate() {
        _transform.position = _mainCamera.position;

        Quaternion _controllerTargetRotation = Quaternion.Euler(0, _mainCamera.eulerAngles.y, 0);
        Quaternion _pivotTargetRotation = Quaternion.Euler(_mainCamera.eulerAngles.x, _mainCamera.eulerAngles.y, 0);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, _controllerTargetRotation, rotationSpeed * Time.deltaTime);
        _weaponPivot.rotation = Quaternion.Slerp(_weaponPivot.rotation, _pivotTargetRotation, rotationSpeed * Time.deltaTime);

    }
}
