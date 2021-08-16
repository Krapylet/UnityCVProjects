using UnityEngine;

public class CostumeAnimation : MonoBehaviour {
    // The Unity sprite renderer so that we don't have to get it multiple times
    public Costume cost;
    [SerializeField] private CostumePartAnimation head;
    [SerializeField] private CostumePartAnimation body;
    [SerializeField] private CostumePartAnimation feet;

    [SerializeField] private CostumePartAnimation eyebrow;
    [SerializeField] private CostumePartAnimation beard;
    [SerializeField] private CostumePartAnimation hat;

    //private AudioSource audioStep;

    private void Start() {
        //Assign costume to PartAnimation
        head.SetCostumePart(cost.head);
        body.SetCostumePart(cost.body);
        feet.SetCostumePart(cost.feet);
        eyebrow.SetCostumePart(cost.eyebrow);
        beard.SetCostumePart(cost.beard);
        hat.SetCostumePart(cost.hat);

        //Assign AudioSource
        //audioStep = GetComponent<AudioSource>();
    }
}