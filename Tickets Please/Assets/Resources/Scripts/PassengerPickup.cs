using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerPickup : MonoBehaviour
{
    private Seat parentSeat;

    // Start is called before the first frame update
    void Start()
    {
        //assigns this transform to parents pickupdestination, so that the passenger later can move towards this location
        parentSeat = transform.GetComponentInParent<Seat>();
        parentSeat.SetPickupDestination(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        //when a passenger enters the pickup trigger of their destination seat, place them on the seat
        if (other.tag.Equals("Passenger") && other.GetComponent<Passenger>().GetDestination().Equals(parentSeat.gameObject))
        {
            parentSeat.Place(other.GetComponent<Passenger>());
        }
    }
}
