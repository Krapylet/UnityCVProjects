using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {

    AudioSource aSStep;

    private void Start() {
        aSStep = GetComponent<AudioSource>();
    }

    public void PlayStep() {
        aSStep.pitch = Random.Range(0.7f, 1.2f);
        aSStep.Play();
    }
}
