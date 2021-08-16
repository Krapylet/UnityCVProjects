using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New chances", menuName = "Tickets Please/Chances")]
public class Chances : ScriptableObject {
    public float[] probs = new float[1];        //Defines an array of chances for ticket legality
    public string[] probNames = new string[1];  //Defines an array of names matching the chances of ticket legality
    
    //Returns chance
    public float GetChance(string which) {
        //Goes through the names of chances and looks for a match
        for (int i = 0; i < probs.Length; i++) {
            if (probNames[i] == which) {
                return probs[i];
            }
        }
        return -1;
    }
}