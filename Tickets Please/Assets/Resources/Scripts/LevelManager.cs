using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public Day[] days;
    private int dayIndex = -1;
    private int levelIndex = -1;
    private AudioSource audioSource;

    private Train thisTrain;
    private float spawnIntervalLeft;
    private float levelTimeLeft;
    private float dayTimeLeft;
    private int passengersSpawned;

    private void Start() {
        thisTrain = GameObject.Find("Train").GetComponent<Train>();
        audioSource = GetComponent<AudioSource>();
        ChangeDay();
        ChangeLevel();
    }

    private void Update() {
        //Runs when level should change 
        if (levelTimeLeft <= 0) {
            ChangeLevel();
        } else {
            levelTimeLeft -= Time.deltaTime;
        }

        //Runs when a spawn should happen
        if (passengersSpawned < days[0].levels[levelIndex].passengerCount) {
            if (spawnIntervalLeft <= 0) {
                spawnIntervalLeft = days[0].levels[levelIndex].spawnMaxInterval;
                thisTrain.SpawnPassenger();
                passengersSpawned++;
            } else {
                spawnIntervalLeft -= Time.deltaTime;
            } 
        }
    }

    //Runs each time the current day should change
    public void ChangeDay() {
        //Only increments the current level if other day exists.
        if (days.Length-1 > dayIndex) {
            dayIndex++; 
        }

        //Set current time intervals
        dayTimeLeft = days[dayIndex].GetDayTime();

        //Debugging
        print("Current day is now: " + (dayIndex+1));
    }

    //Runs each time the current level should change
    public void ChangeLevel() {
        //Only increments the current level if other levels exist.
        if (days[0].levels.Length-1 > levelIndex) {
            levelIndex++; 
        }

        //Set current time intervals
        levelTimeLeft = days[0].levels[levelIndex].GetLevelTime();
        spawnIntervalLeft = days[0].levels[levelIndex].spawnMaxInterval;

        //Reset passenger count
        passengersSpawned = 0;

        //Debugging
        print("Current level is now: " + (levelIndex+1));
        audioSource.clip = days[dayIndex].levels[levelIndex].backgroundMusic;
        audioSource.Play();
    }
}
