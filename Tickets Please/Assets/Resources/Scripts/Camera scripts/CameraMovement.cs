using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public Player pl;
    private Camera cm;

    public CameraProps playerCMP;
    public CameraProps wagonCMP;

    private float shakeShiftInterval;
    private float shaker;

    private float shakeShiftTime;
    private Vector3 pointToGoTo;
    private float sizeToGoTo;
    private Vector3 shakeBy;
    private Vector3 pointPlayerWagon;

    private float accelX, accelY, accelZ;
    private float deltaX, deltaY, deltaZ;
    private float springing, damping;
    private float offsetY, offsetZ;

    private float accelCM;
    private float deltaCM;
    public float springingCM, dampingCM;

    // Start is called before the first frame update
    void Start() {
        offsetY = transform.position.y;
        offsetZ = transform.position.z;
        cm = Camera.main;
        wagonCMP.zoom = cm.orthographicSize;

        pointToGoTo = pl.transform.position;
        springing = playerCMP.springing;
        damping = playerCMP.damping;
        shakeShiftTime = playerCMP.shakeShiftInterval;
    }

    void FixedUpdate() {
        MoveToPoint();
        SetCamSize();

        //Re-shakes the point in desired intervals
        if (shakeShiftTime <= 0) {
            ShakePoint();
            shakeShiftTime = shakeShiftInterval;
        } else {
            shakeShiftTime -= Time.deltaTime;
        }
    }

    //Shake the point so that it becomes more organic when normally set
    void ShakePoint() {
        var x = Random.Range(-shaker, shaker);
        var y = Random.Range(-shaker, shaker);
        var z = Random.Range(-shaker, shaker);
        shakeBy = new Vector3(x, + y, + z);
    }

    //Moves the point to the desired location
    void MoveToPoint() {
        var pointMoveTo = new Vector3(shakeBy.x+pointToGoTo.x, shakeBy.y + offsetY, pointToGoTo.z + offsetZ);

        //Move center point
        deltaX = pointMoveTo.x - transform.position.x;
        deltaY = pointMoveTo.y - transform.position.y;
        deltaZ = pointMoveTo.z - transform.position.z;

        //Create springing effect
        deltaX *= springing;
        deltaY *= springing;
        deltaZ *= springing;

        accelX += deltaX;
        accelY += deltaY;
        accelZ += deltaZ;

        //Move camera center
        var loosePoint = new Vector3(transform.position.x + accelX, transform.position.y + accelY, transform.position.z + accelZ);
        transform.position = loosePoint;

        //Slow down springing
        accelX *= damping;
        accelY *= damping;
        accelZ *= damping;

        // Defines which point to follow and sets the properties for it
        if (pointPlayerWagon == new Vector3()) {
            //Player follow
            pointToGoTo = pl.transform.position;
            sizeToGoTo = playerCMP.zoom;

            springing = playerCMP.springing;
            damping = playerCMP.damping;
            shakeShiftInterval = playerCMP.shakeShiftInterval;
            shaker = playerCMP.shaker;
        } else {
            //Wagon follow
            pointToGoTo = pointPlayerWagon;
            sizeToGoTo = wagonCMP.zoom;

            springing = wagonCMP.springing;
            damping = wagonCMP.damping;
            shakeShiftInterval = wagonCMP.shakeShiftInterval;
            shaker = wagonCMP.shaker;
        }
    }

    //Moves the point to the desired location
    void SetCamSize() {
        //Move center point
        deltaCM = sizeToGoTo - cm.orthographicSize;

        //Create springing effect
        deltaCM *= springingCM;
        accelCM += deltaCM;

        //Change camera size
        cm.orthographicSize = cm.orthographicSize + accelCM;

        //Slow down springing
        accelCM *= dampingCM;
    }

    public void SetWagonPoint(Vector3 point) {
        pointPlayerWagon = point;
    }
}