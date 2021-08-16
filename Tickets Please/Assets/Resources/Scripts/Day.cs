using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Day", menuName = "Tickets Please/Day")]
public class Day : ScriptableObject {
    public Level[] levels;

    public float GetDayTime() {
        float sum = 0f;

        foreach (var level in levels) {
            sum += level.GetLevelTime();
        }

        return sum;
    }
}