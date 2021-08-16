using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RHandHold : MonoBehaviour {
    [SerializeField] private Sprite handHold;
    [SerializeField] private Sprite handPoint;
    [SerializeField] private Sprite handFine;
    [SerializeField] private Sprite handCutter;
    [SerializeField] private Image handCutterBack;

    //makes sure the RHand has the correct sprite.
    public void HoldHandler(TIDraggableObject heldObject) {
        //select the right sprite for the hand
        handCutterBack.enabled = false;
        if (Input.GetMouseButton(0)) {
            if (heldObject == null) {
                //Select the pickup hand sprite
                GetComponent<Image>().sprite = handHold; 
            } else if (heldObject.name.Equals("Fine")) {
                GetComponent<Image>().sprite = handFine;
                heldObject.GetComponent<Image>().enabled = false;
            } else {
                GetComponent<Image>().sprite = handCutter;
                heldObject.GetComponent<Image>().enabled = false;
                handCutterBack.enabled = true;
                handCutterBack.transform.position = transform.position;
            }
        } else {
            //Select the pointing hand sprite
            GetComponent<Image>().sprite = handPoint;
        }
    }
}
