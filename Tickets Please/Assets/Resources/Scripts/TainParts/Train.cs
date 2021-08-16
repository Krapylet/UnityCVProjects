using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    private List<Wagon> wagons = new List<Wagon>();
    private LevelManager levelManager;

    //TODO: Change fields under (and remove some)
    [SerializeField] private float acceleration = 0.2f;
    [SerializeField] private float speedMax = 2f;
    private float speed = 0f;
    [SerializeField] private Renderer trackRenderer;

    private void Start() {
        levelManager = GetComponent<LevelManager>();
    }

    private void Update() {
        //----- Prototype --- Spawn a passenger whenever the key 'p' is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnPassenger();
        }
        //--- Prototype
    }

    private void FixedUpdate() {
        trackRenderer.material.SetFloat("Vector1_2B345030", (Time.time*(speed/100f)));
        if (speed <= speedMax) {
            speed += acceleration;
        }
    }

    // Spawns a passenger at a random door in this train.
    //  TODO:
    //  Should take a train as parameter, and be moved to the LevelManager Script once the levelManager GameObject has been Created, in order to increase chohesion 
    public void SpawnPassenger() {
        List<Door> doors = GetAllDoors();
        doors[Random.Range(0, doors.Count - 1)].SpawnPassenger();
    }

    // Returns all seats in the entire train.
    public List<Seat> GetAllSeats()
    {
        List<Seat> allSeats = new List<Seat>();

        foreach (Wagon w in wagons)
        {
            allSeats.AddRange(w.GetAllSeats());
        }

        return allSeats;
    }

    // Returns all unoccupied seats in the entire train.
    public List<Seat> GetFreeSeats()
    {
        List<Seat> allSeats = new List<Seat>();

        foreach (Wagon w in wagons)
        {
            allSeats.AddRange(w.freeSeats);
        }

        return allSeats;
    }

    // Returns all occupied seats in the entire train.
    public List<Seat> GetOccupiedSeats()
    {
        List<Seat> allSeats = new List<Seat>();

        foreach (Wagon w in wagons)
        {
            allSeats.AddRange(w.occupiedSeats);
        }

        return allSeats;
    }

    // Returns all occupied seats in the entire train.
    public List<Door> GetAllDoors()
    {
        List<Door> allDoors = new List<Door>();

        foreach (Wagon w in wagons)
        {
            allDoors.AddRange(w.doors);
        }

        return allDoors;
    }

    // Adds a wagon to the trains list of wagons. Only used when wagons are instantiated.
    public void AddWagon(Wagon w)
    {
        wagons.Add(w);
    }
}
