using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seat : MonoBehaviour, IInteractable, ISittable
{
    private bool isOccupied;
    private Passenger passenger;
    private Wagon wagon;
    private Transform pickupDest;
    private Transform passengerHolderDest;
    private Player player;

    //---- Prototype -----
    private Color baseColor = Color.white;
    private Color OccupiedColor = new Color(1f, 0.5f, 0.5f, 1);
    private Color highlightColorMod = new Color(0, 0, 0.5f, 0);
    //---- Prototype -----

    // Start is called before the first frame update
    void Start()
    {
        InitializeValues();

        //add seat to parent wagon
        wagon = transform.GetComponentInParent<Wagon>();
        wagon.AddSeat(this);
    }

    public void Interact()
    {
        if (isOccupied)
        {
            //passenger.LeaveTrain();
            
            //disable player movement
            player.DisableMovement();

            //inspect ticket
            player.InspectTicket(passenger);
        }
        else
        {
            print("There is noone to talk on this seat");
        }
    }

    //Initalizes all starting values for the seat. 
    private void InitializeValues()
    {
        ReserveTo(null);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Returns the current passenger in this seat. Returns null if no passenger is present.
    public Passenger GetPassenger()
    {
        return passenger;
    }

    // Reserves the seat for the input passenger. 
    public void ReserveTo(Passenger newPassenger)
    {
        if (newPassenger == null && passenger != null)
        {
            //move from list of used seats to list of unused seats
            wagon.occupiedSeats.Remove(this);
            wagon.freeSeats.Add(this);
        }
        else if (newPassenger != null && passenger == null)
        {
            //move from list of unused seats to list of used seats
            wagon.occupiedSeats.Add(this);
            wagon.freeSeats.Remove(this);

            //----- Prototype ----- The seat is given a blue tint if it is occupied
            //tint all child spirtes
            foreach (SpriteRenderer s in gameObject.GetComponentsInChildren<SpriteRenderer>())
            {
                s.color = OccupiedColor;
            }
            //----- Prototype ------
        }
        passenger = newPassenger;
    }

    //Places a passenger on the seat
    public void Place(Passenger p)
    {
        //set the seat to occupied
        isOccupied = true;

        //place the passenger the right palce
        p.transform.position = passengerHolderDest.position;

        //disable any movement on the passenger
        p.GetComponent<NavMeshAgent>().enabled = false;
        p.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void AddHighlight()
    {
        Color newCol;
        if (passenger != null) { newCol = OccupiedColor + highlightColorMod; }
        else { newCol = new Color(0.5f, 0.5f, 1f); }

        //tint all child sprites
        foreach (SpriteRenderer s in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = newCol;
        }
    }

    public void RemoveHighlight()
    {
        Color newCol;
        if (passenger == null) { newCol = baseColor; }
        else { newCol = OccupiedColor; }

        //tint all child sprites
        foreach (SpriteRenderer s in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            s.color = newCol;
        }
    }

    //returns the transform in the center of the Pickuphandler trigger
    public Transform GetPickupTransform()
    {
        return pickupDest;
    }

    public Wagon GetWagon()
    {
        return wagon;
    }

    //Used by the PickupHandler to assign the destination where passengers are picked up. Only used when new seats are instantiated.
    public void SetPickupDestination(Transform t)
    {
        pickupDest = t;
    }

    //Assigns a passengerHolder to the seat, where the passenger later will be placed. Only used when new seats are instantiated.
    public void SetHolderDestination(Transform t)
    {
        passengerHolderDest = t;
    }
}
