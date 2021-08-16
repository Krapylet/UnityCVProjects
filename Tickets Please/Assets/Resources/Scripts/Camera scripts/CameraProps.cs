using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New property", menuName = "Tickets Please/Camera/Properties")]
public class CameraProps : ScriptableObject {
    public float springing, damping;
    public float zoom;

    public float shaker;
    public float shakeShiftInterval;
}
