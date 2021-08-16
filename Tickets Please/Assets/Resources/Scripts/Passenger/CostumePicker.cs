using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CostumePicker : MonoBehaviour {
    //Male parts
    [SerializeField] private CostumePart[] maleHeads;
    [SerializeField] private CostumePart[] maleBodies;
    [SerializeField] private CostumePart[] maleFeet;
    [SerializeField] private CostumePart[] maleHats;

    //Female parts
    [SerializeField] private CostumePart[] femaleHeads;
    [SerializeField] private CostumePart[] femaleBodies;
    [SerializeField] private CostumePart[] femaleFeet;
    [SerializeField] private CostumePart[] femaleHats;

    public Costume GetRandomCostume() {
        return null;
    }
}