using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class FadeableObject : MonoBehaviour {
    public float speed;
    private Material origMat;
    private Material origMatGone;

    private void Start() {
        origMat = GetComponent<Renderer>().sharedMaterial;
        origMatGone = new Material(origMat);
        origMatGone.color = new Color(origMat.color.r, origMat.color.g, origMat.color.b, 0f);
    }

    public void FadeIn() {
        //for (int i = 0; i < speed*100; i++) {
        //    GetComponent<Renderer>().sharedMaterial.Lerp(origMatGone, origMat, speed*100/i);
        //    Thread.Sleep(10);
        //}
    }

    public void FadeOut() {
        //for (int i = 0; i < speed * 100; i++) {
        //    GetComponent<Renderer>().sharedMaterial.Lerp(origMat, origMatGone, speed * 100 / i);
        //    Thread.Sleep(10);
        //}
    }
}
