using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISittable
{
    //Assign a spirterenderer to hold the image of the passenger in the seat.
    void SetHolderDestination(Transform t);

    //Places a passenger in the Sittable
    void Place(Passenger p);

    //Reserves this seat for passenger p
    void ReserveTo(Passenger p);

    //Returns the centerposition of the pickup trigger
    Transform GetPickupTransform();

    //Returns the wagon the ISittable object is placed in
    Wagon GetWagon();
}
