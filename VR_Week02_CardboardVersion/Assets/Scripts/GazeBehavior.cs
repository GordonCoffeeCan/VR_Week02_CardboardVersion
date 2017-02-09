using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeBehavior : MonoBehaviour {

    public Image crosshairHighlighted;

    private Transform _camTrans;
    private Ray _ray;
    private float _timeLookedAt = 1;

    private AudioSource shotgunFire;

    private bool _firstTimeRayHitTarget = false;
    private bool _isCoolDown = true;

    private void Awake() {
        _camTrans = this.transform;
        shotgunFire = GetComponent<AudioSource>();
        if (crosshairHighlighted == null) {
            Debug.LogError("crosshairHighlighted is not assigned a reference");
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void FixedUpdate() {
        _ray = new Ray(_camTrans.position, _camTrans.forward);

        RaycastHit _rayHit = new RaycastHit();

        if (Physics.Raycast(_ray, out _rayHit)) {
            if (crosshairHighlighted != null) {
                if (_rayHit.transform.tag == "Target") {
                    _firstTimeRayHitTarget = true;
                    _timeLookedAt = Mathf.Clamp01(_timeLookedAt + Time.deltaTime);
                    
                } else {
                    _timeLookedAt = Mathf.Clamp01(_timeLookedAt - Time.deltaTime);
                }
                if(_firstTimeRayHitTarget == true) {
                    crosshairHighlighted.fillAmount = _timeLookedAt;
                    if (crosshairHighlighted.fillAmount == 1 && _isCoolDown == true) {
                        Rigidbody _targetRig = _rayHit.transform.GetComponent<Rigidbody>();
                        _targetRig.AddForce(_rayHit.point * 2, ForceMode.Impulse);
                        WeaponBehavior.isFire = true;
                        shotgunFire.Play();
                        _isCoolDown = false;
                    } else if (crosshairHighlighted.fillAmount < 0.8f) {
                        _isCoolDown = true;
                    }else if(_isCoolDown == false) {
                        WeaponBehavior.isFire = false;
                    }
                }
            }
        }
    }
}
