using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CustomEditor(typeof(SpriteSheet))]
//public class SpriteSheetEditor : Editor {
//    public override void OnInspectorGUI() {
//        SpriteSheet sS = (SpriteSheet)target;

//        GUILayout.BeginHorizontal();
//        sS.rows = EditorGUILayout.IntField(sS.rows);
//        sS.cols = EditorGUILayout.IntField(sS.cols);
//        sS.sprite = (Sprite)EditorGUILayout.ObjectField(sS.sprite, typeof(Sprite));
//        GUILayout.EndHorizontal();
//    }
//}
[System.Serializable]
public struct SpriteAnimation {
    public string name;
    public Sprite[] sprites;

    public SpriteAnimation(string name) {
        this.name = name;
        sprites = new Sprite[4];
    }

    public SpriteAnimation(string name, Sprite[] sprites) {
        this.name = name;
        this.sprites = sprites;
    }
}

[CreateAssetMenu(fileName = "New Costume Part", menuName = "Tickets Please/Costume/Part")]
public class CostumePart : ScriptableObject {
    public SpriteAnimation[] anims = {
        new SpriteAnimation("IdleR"),
        new SpriteAnimation("IdleL"),

        new SpriteAnimation("WalkR"),
        new SpriteAnimation("WalkL"),

        new SpriteAnimation("BackWalkR"),
        new SpriteAnimation("BackWalkL"),

        new SpriteAnimation("SitR"),
        new SpriteAnimation("SitL")
    };
    public float chance;

    public SpriteAnimation GetValue(string key) {
        foreach (var anim in anims) {
            if (anim.name == key) {
                return anim;
            }
        }
        return new SpriteAnimation("Null");
    }

    public SpriteAnimation this[string key] {
        get { return GetValue(key); }
    }
}