using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CostumePartAnimation : MonoBehaviour {
    // The Unity sprite renderer so that we don't have to get it multiple times
    private SpriteRenderer spriteRenderer;
    public int indexToDraw;
    public int drawState;
    private CostumePart cPart;

    // Use this for initialization
    private void Start() {
        // Get and cache the sprite renderer for this game object
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Runs after the animation has done its work
    private void LateUpdate() {
        if (cPart != null) {
            if (cPart.anims.Length > drawState) {
                if (cPart.anims[drawState].sprites.Length > indexToDraw) {
                    spriteRenderer.sprite = cPart.anims[drawState].sprites[indexToDraw];
                } else { LogFail(); }
            } else { LogFail(); }
        }
    }

    public void LogFail() {
        Debug.LogWarning("Couldn't find S" + drawState + "I" + indexToDraw + " to draw");
    }

    //Sets the costume part
    public void SetCostumePart(CostumePart cPart) {
        this.cPart = cPart;
    }
}