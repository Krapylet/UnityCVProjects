using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TIHandler : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private Passenger passenger;

    [SerializeField] private RHand rHand;
    [SerializeField] private Ticket ticket;
    [SerializeField] private ID ID;
    [SerializeField] private Canvas canvas;


    // Start is called before the first frame update
    void Start()
    {
        //hide the TI at the start of the game
        gameObject.SetActive(false);
    }

    //Loads the information from the passenger into the Ticket and the ID
    public void LoadPassenger(Passenger p)
    {
        passenger = p;
        ticket.SetInfo(p);
        ID.SetInfo(p);
    }

    public void ApplyObjectToTicket(TIDraggableObject usedObj)
    {
        if (usedObj != null)
        {
            //If the ticket was a fine, make the passenger leave the train
            if (usedObj.name.Equals("Fine"))
            {
                passenger.LeaveTrain();
                if (passenger.GetLegality())
                {
                    print("Passenger was given an UNJUST fine");
                    gameManager.LooseLife();
                }
                else
                {
                    print("Passenger was given a JUSTIFIED fine");
                }

            }
            //if the locher is dropped, let the passenger stay
            else if (usedObj.name.Equals("Locher"))
            {
                if (passenger.GetLegality())
                {
                    print("Passenger was approved CORRECTLY");
                }
                else
                {
                    print("Passenger was approved INCORRECTLY");
                    gameManager.LooseLife();
                }

                //do nothing
            }

            //reenable player movement
            GameObject.Find("Player").GetComponent<Player>().EnableMovement();

            //Hide the TI
            gameObject.SetActive(false);
        }
    }

    //returns a reference to the RHand script
    public RHand GetRHand()
    {
        return rHand;
    }

    //Returns the passenger that is currently being inspected
    public Passenger GetPassenger()
    {
        return passenger;
    }
}
