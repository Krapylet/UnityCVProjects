using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Costume Picker", menuName = "Tickets Please/Costume/Picker/Picker")]
public class CostumePickerSelector : ScriptableObject {
    //Male parts
    [SerializeField] private CostPartWithSubCostParts[] maleHeads;
    [SerializeField] private CostumePart[] maleBodies;
    [SerializeField] private CostumePart[] maleFeet;
    [SerializeField] private CostumePart[] maleHats;

    //Female parts
    [SerializeField] private CostPartWithSubCostParts[] femaleHeads;
    [SerializeField] private CostumePart[] femaleBodies;
    [SerializeField] private CostumePart[] femaleFeet;
    [SerializeField] private CostumePart[] femaleHats;

    public Costume GetRandomCostume(bool isMale) {
        var cost = new Costume();
        
        if (isMale) {
            //cost.head = GetRandomPiece(maleHeads);
            cost.body = GetRandomPiece(maleBodies);
            cost.feet = GetRandomPiece(maleFeet);
            cost.hat = GetRandomPiece(maleHats);
        } else {
            //cost.head = GetRandomPiece(femaleHeads);
            cost.body = GetRandomPiece(femaleBodies);
            cost.feet = GetRandomPiece(femaleFeet);
            cost.hat = GetRandomPiece(femaleHats);
        }



        return cost;
    }

    public static CostumePart GetRandomPiece(CostumePart[] costParts) {
        var rd = Random.value;
        var beenThrough = 0f;
        var sum = GetSumOfChances(costParts);

        foreach (var costPart in costParts) {
            beenThrough += costPart.chance;
            if (rd <= (beenThrough/sum)) {
                //If it has found a costume
                return costPart;
            }
        }

        return null;
    }

    public static float GetSumOfChances(CostumePart[] costParts) {
        var sum = 0f;
        foreach (var costPart in costParts) {
            sum += costPart.chance;
        }
        return sum;
    }
}

[System.Serializable]
[CreateAssetMenu(fileName = "New CostPart with subs", menuName = "Tickets Please/Costume/Picker/CostPart with sub parts")]
public class CostPartWithSubCostParts : ScriptableObject {
    //Main part e.g. Head
    [SerializeField] private CostumePart mainPart;

    //Sub parts, e.g. matching hair parts
    [SerializeField] private CostumePart[][] subParts;

    public static CostumePart GetRandomPiece(CostumePart[] costParts) {
        return CostumePickerSelector.GetRandomPiece(costParts);
    }
}