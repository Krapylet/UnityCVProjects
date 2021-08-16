using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Tickets Please/Level")]
public class Level : ScriptableObject {
    public int passengerCount = 4;              //Defines how many passengers should be spawn for this level
    public float spawnMinInterval = 0.5f;       //Defines the time between each passenger spawn
    public float spawnMaxInterval = 1.5f;       //Defines the time between each passenger spawn
    public float trainStartTime = 3.5f;         //Defines the time it takes to start the train
    public float trainStopTime = 3.5f;          //Defines the time it take to stop the train
    public Wagon wagonToAdd = null;             //Defines which wagon to add, and if null it shouldn't add any
    public AudioClip backgroundMusic = null;    //Defines which background music to play during the level
    public Chances probs = null;        //Defines an array of chances for ticket legality
    public Chances probsP = null;       //Placeholder probabilities

    //Gets the level time in seconds (it's defined by bg track length)
    public float GetLevelTime() {
        return backgroundMusic.length;
    }

    //Returns chance of legality
    public float GetChanceOfLegality(string which) {
        return probs.GetChance(which);
    }
}