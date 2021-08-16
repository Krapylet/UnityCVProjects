using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerHolder : MonoBehaviour
{
    void Start()
    {
        //link this passengerholder to the parent sittable object, so that passengers can be placed here later
        GetComponentInParent<ISittable>().SetHolderDestination(transform);
    }
}
