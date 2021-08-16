using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;

public class Passenger : MonoBehaviour
{
    private Train train;
    private NavMeshAgent NMAgent;
    private ISittable chosenSeat;
    private Rigidbody rBody;
    private GameObject destination;

    private CostumePicker costumePicker;
    private CostumeAnimation costumeAnimation;

    [SerializeField] private TextAsset maleNames;
    [SerializeField] private TextAsset femaleNames;

    private bool isMale;

    //PassengerInfo: 
    //TODO! these should all get getter methods... i was just too lazy
    //ticket info
    public int TSegl;
    public string TName;
    public string TAge;
    //id info
    public int ISegl;
    public string IName;
    public string IAge;
   

    private bool isLegal = true;

    private void Start() {
        train = GameObject.Find("Train").GetComponent<Train>();
        NMAgent = gameObject.GetComponent<NavMeshAgent>();
        ChooseSeat();
        InitializePapers();

        //disable rigidbody rotation, so that the passenger doesnt spin when hit by the player
        rBody = gameObject.GetComponent<Rigidbody>();
        rBody.freezeRotation = true;

        //Assign random costume to passenger
        costumeAnimation = GetComponentInChildren<CostumeAnimation>();
        costumePicker = GetComponentInChildren<CostumePicker>();
        costumeAnimation.cost = costumePicker.GetRandomCostume();
    }

    private void Update()
    {
        //make sure that the passenger doesnt slide around after hitting the player
        rBody.velocity = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.L)) {
            LeaveTrain();
        }
    }

    //set desination to a random free seat in the train.
    private void ChooseSeat() {
        //find an empty seat
        List<Seat> freeSeats = train.GetFreeSeats();
        destination = freeSeats[Random.Range(0, freeSeats.Count - 1)].gameObject;
        chosenSeat = destination.GetComponent<Seat>();

        //reserve seat
        chosenSeat.ReserveTo(this);

        //move towards seat
        NMAgent.destination = chosenSeat.GetPickupTransform().position;
    }

    //Make the passenger leave through a random door.
    public void LeaveTrain()
    {
        //Remove the passenger from the seat
        chosenSeat.ReserveTo(null);

        //get random door in the train
        List<Door> doors = chosenSeat.GetWagon().GetTrain().GetAllDoors();
        Door door = doors[Random.Range(0, doors.Count - 1)];

        //Reactivate NavMeshAgent and set destinaton to the chosen door
        NMAgent.enabled = true;
        destination = door.gameObject;
        NMAgent.destination = door.GetTriggerPos();

        //place at the Sittables pickupDestination
        transform.position = chosenSeat.GetPickupTransform().position;
    }

    //Gives random matching values to the passengers ticket and ID.
    private void InitializePapers()
    {
        //set info
        //randomly determins wheter the passenger is male or female
        isMale = 1 == Random.Range(0, 1);

        TName = IName = getRandomName();
        TAge = IAge = getRandomAge().ToString();
        TSegl = ISegl = Random.Range(0, 4);

        //random chance for wrong info
        //TODO! Later, the chance could be changed to depend on level)
        //TODO! the wrong info should be some permutation of the correct string
        int risk = 40;
        int res = Random.Range(1, 101);
        if(res <= risk)
        {
            isLegal = false;
        }
        if (res <= risk * 1 / 8)
        {
            TName = "wrong info";
        }
        else if (res <= risk * 2 / 8)
        {
            TAge = "wrong info";
        }
        else if (res <= risk * 3 / 8)
        {
            //assigns a random new segl (cant be the same as before)
            TSegl = (TSegl + Random.Range(1, 4)) % 5;
        }
        else if (res <= risk * 4 / 8)
        {
            IName = "wrong info";
        }
        else if (res <= risk * 5 / 8)
        {
            IAge = "wrong info";
        }
        else if (res <= risk * 6 / 8)
        {
            //assigns a random new segl (cant be the same as before)
            ISegl = (ISegl + Random.Range(1, 4)) % 5;
        }
    }

    //gets a random gender appropriate name
    private string getRandomName()
    {
        string[] names;
        if (isMale)
        {
            names = maleNames.text.Split('\n');
        }
        else
        {
            names = femaleNames.text.Split('\n');
        }

        return names[Random.Range(0, names.Length - 1)];
    }


    //gets a random date
    private int getRandomAge()
    {
        return Random.Range(10, 90);
    }


    //returns the gameobject the passenger is currently traveling towards
    //this is not the NVAgents transform dest, but a manually updated gameobj dest
    public GameObject GetDestination()
    {
        return destination;
    }

    public bool GetLegality()
    {
        return isLegal;
    }
}
