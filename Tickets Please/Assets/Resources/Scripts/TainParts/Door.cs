using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Wagon wagon;
    private BoxCollider exitTrigger;
    private Animator anim;

    private void Start()
    {
        //Add door to parent wagon
        wagon = transform.parent.GetComponent<Wagon>();
        wagon.AddDoor(this);
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //when as passenger that is leaving the train enters the trigger, delete it, and play the open door animation;
        if(other.tag.Equals("Passenger") && other.GetComponent<Passenger>().GetDestination().Equals(gameObject))
        {
            // Play animation here;
            anim.Play("Open");
            print(other.name + " left the train");
            Destroy(other.gameObject);
        }
    }

    //spawns a passenger in front of the door.
    public void SpawnPassenger()
    {
        anim.Play("Open");
        Instantiate(Resources.Load("Prefabs/Passenger"), transform.position + Vector3.forward, transform.rotation);
    }

    //returns the position of the exittrigger on the door gameobject
    public Vector3 GetTriggerPos()
    {
        return gameObject.GetComponent<BoxCollider>().center + transform.position;
    }
}
