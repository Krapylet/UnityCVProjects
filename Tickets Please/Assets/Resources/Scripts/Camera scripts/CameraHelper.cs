using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHelper : MonoBehaviour {
    private CameraMovement cm;

    public FadeableObject[] faders;

    private void Start() {
        cm = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter(Collider other) {
        if (cm != null && other.tag == "Player") {
            cm.SetWagonPoint(transform.position);
            foreach (var fader in faders) {
                fader.FadeIn();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (cm != null && other.tag == "Player") {
            cm.SetWagonPoint(new Vector3());
            foreach (var fader in faders) {
                fader.FadeOut();
            }
        }
    }
}
