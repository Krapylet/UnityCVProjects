using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon : MonoBehaviour
{
    public List<Seat> freeSeats = new List<Seat>();
    public List<Seat> occupiedSeats = new List<Seat>();
    public List<Door> doors = new List<Door>();
    private Train train;

    private void Start()
    {
        //add wagon to parent train
        train = transform.GetComponentInParent<Train>();
        train.AddWagon(this);
    }

    // Returns a list containing all the seats in the wagon.
    public List<Seat> GetAllSeats()
    {
        List<Seat> allSeats = new List<Seat>();
        allSeats.AddRange(freeSeats);
        allSeats.AddRange(occupiedSeats);
        return allSeats;
    }

    public Train GetTrain()
    {
        return train;
    }

    // Adds a seat to the wagons lists of seats. Only used when new seats are instatiated.
    public void AddSeat(Seat s)
    {
        if (s.GetPassenger() == null)
        {
            freeSeats.Add(s);
        }
        else
        {
            occupiedSeats.Add(s);
        }
    }

    // Adds a door to the wagons list of doors. Only used when new doors are instatiated.
    public void AddDoor(Door d)
    {
        doors.Add(d);
    }
}
