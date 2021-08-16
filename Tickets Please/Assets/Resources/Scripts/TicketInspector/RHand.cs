using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RHand : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RHandHold rHandHold;

    //The object the hand is currently holding
    private TIDraggableObject currentObj;

    // Start is called before the first frame update
    private void Start() {
        //Sets all init values
        currentObj = null;
    }

    // Update is called once per frame
    private void OnGUI() {
        MoveHand();
        rHandHold.HoldHandler(currentObj);
    }

    // Updates the hands position to match the cursors position
    private void MoveHand() {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        transform.position = canvas.transform.TransformPoint(pos);

        //If an object is picked up, it is also moved
    }

    //returns the object currently held by the hand
    public TIDraggableObject GetCurrentObject() {
        return currentObj;
    }

    //Picks up an TIDraggableObject. Is only called from the TIDraggableObject iself.
    public void PickUp(TIDraggableObject obj) {
        currentObj = obj;
    }
}
