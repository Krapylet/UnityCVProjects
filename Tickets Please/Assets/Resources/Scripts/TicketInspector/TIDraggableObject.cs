using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TIDraggableObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private bool mouseOver = false;
    private bool pickedUp = false;
    private Vector3 startPos;

    [SerializeField] private RHand rHand;
    [SerializeField] private Ticket ticket;

    private void Start()
    {
        //save local start pos for later use
        startPos = transform.localPosition;
    }

    private void OnGUI()
    {
        //folow RHand if the ticket is picked up;
        if (pickedUp)
        {
            transform.position = rHand.transform.position;
        }
        else
        {
            transform.localPosition = startPos;
        }
    }

    //highlight when mouse hovers over the object
    public void OnPointerEnter(PointerEventData data)
    {
        //Dont highliht if an object has already been picked up 
        if (!pickedUp)
        {
            mouseOver = true;
            gameObject.GetComponent<Image>().color = Color.blue;
        }
    }

    //removes highlight when mouse no longer hovers over the fine
    public void OnPointerExit(PointerEventData data)
    {
        //if statement takes care of edgecase where the mouse is moved very fast or mouse is outside gamewindow.
        if (!pickedUp) 
        {
            mouseOver = false;
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }

    //make the ticket folow the mouse when dragged
    public void OnPointerDown(PointerEventData data)
    {
        if (mouseOver)
        {
            pickedUp = true;
            rHand.PickUp(this);
            GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
    }

    //Move the fine back to the starting position if it let go
    public void OnPointerUp(PointerEventData data)
    {
        //intercept the method with the OnPointerUp method of the ticket (the eventhandler will only trigger one method)
        ticket.OnPointerUp(data);

        //resume method
        pickedUp = false;
        rHand.PickUp(null);
        gameObject.GetComponent<Image>().color = Color.white;
        GetComponent<Image>().raycastTarget = true;
        GetComponent<Image>().enabled = true;
    }
}
