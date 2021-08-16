using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ID : MonoBehaviour
{
    //textfields on the ID card
    [SerializeField] private Text Iname;
    [SerializeField] private Text IAge;

    [SerializeField] private Sprite[] SeglSprites = new Sprite[5];


    // enters the passengers papers info into the ticket
    public void SetInfo(Passenger p)
    {
        Iname.text = p.IName;
        IAge.text = p.IAge;
    }
}
