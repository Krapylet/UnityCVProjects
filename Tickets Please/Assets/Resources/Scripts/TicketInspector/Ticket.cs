using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ticket : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    private bool mouseOver = false;
    private RHand rHand;
    [SerializeField] private TIHandler ticketInspector;

    //stat fields
    [SerializeField] private Image TAgeMarker;
    [SerializeField] private Text TAgeMarkerText;
    [SerializeField] private Text Tname;
    [SerializeField] private Image TSegl;

    //age markers
    [SerializeField] private Sprite juniorMarker;
    [SerializeField] private Sprite adultMarker;
    [SerializeField] private Sprite SeniorMarker;

    //segl
    [SerializeField] private Sprite[] SeglSprites = new Sprite[5];

    //highlight when mouse hovers over the object
    public void OnPointerEnter(PointerEventData data)
    { 
        gameObject.GetComponent<Image>().color = Color.blue;
        mouseOver = true;
    }

    //removes highlight when mouse no longer hovers over the fine
    public void OnPointerExit(PointerEventData data)
    {
            mouseOver = false;
            gameObject.GetComponent<Image>().color = Color.white;
    }

    //if the hand is letting go of an object above the ticket, react according to the dropped object
    //Also hides the TicektInspector afterwards and reenables player movement.
    public void OnPointerUp(PointerEventData data)
    {
        if (mouseOver)
        {
            TIDraggableObject droppedObj = ticketInspector.GetRHand().GetCurrentObject();
            ticketInspector.ApplyObjectToTicket(droppedObj);

            //remove highlight
            mouseOver = false;
            gameObject.GetComponent<Image>().color = Color.white;
            GetComponent<Image>().raycastTarget = true;
            GetComponent<Image>().enabled = true;
        }
    }

    // enters the passengers papers info into the ticket
    public void SetInfo(Passenger p)
    {
        Tname.text = p.TName;
        TSegl.sprite = SeglSprites[p.TSegl];

        //sets the little colored marker to match the age
        if(int.Parse(p.TAge) < 30)
        {
            TAgeMarker.sprite = juniorMarker;
            TAgeMarkerText.text = "10-29";
        }
        else if (int.Parse(p.TAge) < 60)
        {
            TAgeMarker.sprite = adultMarker;
            TAgeMarkerText.text = "30-59";
        }
        else
        {
            TAgeMarker.sprite = SeniorMarker;
            TAgeMarkerText.text = "60+";
        }
    }
}
