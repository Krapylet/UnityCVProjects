using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Costume", menuName = "Tickets Please/Costume/Costume")]
public class Costume : ScriptableObject {
    public CostumePart head;
    public CostumePart body;
    public CostumePart feet;

    public CostumePart eyebrow;
    public CostumePart beard;
    public CostumePart hat;
}