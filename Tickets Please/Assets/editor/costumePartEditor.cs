using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;

[CustomEditor(typeof(CostumePart))]
public class CostumePartEditor : Editor {
    private Texture2D[] animMasterSprite;
    private Vector2[] animMasterSpriteSlices;
    private Texture2D animMasterSpriteMASTER;
    private Vector2 animMasterSpriteSlicesMASTER = new Vector2(1, 4);
    bool[] showBeneath;
    private int animStates;
    private int toAdd;

    bool hasRun = false;
    bool showAdvancedPanel = false;

    public override void OnInspectorGUI() {
        CostumePart cP = (CostumePart)target;

        //Add banner with font
        var banner = Resources.Load<Texture2D>("2D/Thim/Env/SkinnerTEST");
        var bannerPlacement = new Rect(0, 0, Screen.width, ((float)banner.height / (float)banner.width) * Screen.width);
        var guiStyle = Resources.Load<GUISkin>("Game/Levels/CreatorSkins").GetStyle("Titel");
        guiStyle.fontSize = (int)(bannerPlacement.height / 4f);
        GUI.DrawTexture(bannerPlacement, banner);
        GUI.Label(bannerPlacement, "Costume Part Creator", guiStyle);

        //Add logo in top right
        var logo = Resources.Load<Texture2D>("2D/Griner2");
        var logoPlacement = new Rect(Screen.width - (Screen.width / 10f), 0, Screen.width / 10f, Screen.width / 10f);
        GUI.DrawTexture(logoPlacement, logo);
        
        //Begin the main area
        GUILayout.Space(bannerPlacement.height-30);
        AssignStatesBlockSetAnimationBlock(ref animMasterSpriteMASTER, ref animMasterSpriteSlicesMASTER);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Show advanced options:");
        showAdvancedPanel = EditorGUILayout.Toggle(showAdvancedPanel);
        GUILayout.EndHorizontal();
        if (showAdvancedPanel) {
            SetAnimationStateBlock(ref cP.anims, ref animStates, ref toAdd); 
            GUILayout.Space(10);
            DrawDefaultInspector();
        }
    }

    //Set default slice
    public void DefaultInit() {
        var states = ((CostumePart)(target)).anims.Length;
        animMasterSpriteSlices = new Vector2[states];
        animMasterSprite = new Texture2D[states];
        showBeneath = new bool[states];
        for (int i = 0; i < states; i++) {
            animMasterSpriteSlices[i] = new Vector2(1, 4);
            showBeneath[i] = false;
        }
    }

    public void AssignStatesBlockSetAnimationBlock(ref Texture2D main, ref Vector2 slicer) {
        GUILayout.BeginHorizontal();
        main = (Texture2D)EditorGUILayout.ObjectField(main, typeof(Texture2D), false);
        GUILayout.Label("Cols:");
        slicer.x = EditorGUILayout.FloatField(slicer.x);
        GUILayout.Label("Rows:");
        slicer.y = EditorGUILayout.FloatField(slicer.y);
        GUILayout.EndHorizontal();

        const int buttonCols = 4;
        int drawStateCount = ((CostumePart)target).anims.Length;
        int rowCount = (int)Mathf.Ceil((float)drawStateCount / (float)buttonCols);
        Debug.Log("RC: "+rowCount);
        for (int i = 0; i < rowCount; i++) {
            GUILayout.BeginHorizontal();
            //Loop through normally
            for (int j = 0; j < buttonCols; j++) {
                var animIndex = i * buttonCols + j;
                Debug.Log(animIndex);

                if (animIndex < drawStateCount) {
                    if (GUILayout.Button("S/A: " + ((CostumePart)target).anims[animIndex].name)) {
                        if (main != null) {
                            ((CostumePart)target).anims[animIndex].sprites = SplitSprite(main, slicer);
                        }
                    } 
                }
            }
            GUILayout.EndHorizontal(); 
        }
        GUILayout.Space(10);
    }

    public void SetAnimationBlock(int animIndex, ref Texture2D main, ref SpriteAnimation spriteAnimation, ref Vector2 slicer) {
        GUILayout.BeginHorizontal();
        spriteAnimation.name = EditorGUILayout.TextField(spriteAnimation.name);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        main = (Texture2D)EditorGUILayout.ObjectField(main, typeof(Texture2D), false);
        GUILayout.Label("Cols:");
        slicer.x = EditorGUILayout.FloatField(slicer.x);
        GUILayout.Label("Rows:");
        slicer.y = EditorGUILayout.FloatField(slicer.y);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Slice and assign")) {
            if (main != null) {
                var getSliced = SplitSprite(main, slicer);
                //foreach (var sprite in getSliced) { Debug.Log(sprite.name); }
                spriteAnimation.sprites = getSliced;
                //foreach (var sprite in spriteAnimation.sprites) { Debug.Log(sprite.name); }
                main = null;
            } 
        }
        GUILayout.BeginHorizontal();
        GUILayout.Label("Show anim sprites:");
        showBeneath[animIndex] = EditorGUILayout.Toggle(showBeneath[animIndex]);
        GUILayout.EndHorizontal();
        if (showBeneath[animIndex]) {
            for (int i = 0; i < spriteAnimation.sprites.Length; i++) {
                spriteAnimation.sprites[i] = (Sprite)EditorGUILayout.ObjectField(spriteAnimation.sprites[i], typeof(Sprite), false);
            } 
        }
        GUILayout.Space(10);
    }

    //Adds a chances panel
    public void SetAnimationStateBlock(ref SpriteAnimation[] anims, ref int animStates, ref int toAdd) {
        if (!hasRun) {
            DefaultInit();
            hasRun = true;
        }

        //Get the current amount of chances
        animStates = anims.Length;

        //Add a field to change the chance count
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Draw states:");
        animStates = EditorGUILayout.IntField(animStates) + toAdd;
        toAdd = 0;
        GUILayout.EndHorizontal();

        for (int i = 0; i < anims.Length; i++) {
            SetAnimationBlock(i, ref animMasterSprite[i], ref anims[i], ref animMasterSpriteSlices[i]);
        }

        //Makes a list equal to the current chance and name arrays
        //and looks at how many elements it should add/remove from it
        var animStatesChange = animStates - anims.Length;
        List<SpriteAnimation> animsL = new List<SpriteAnimation>(anims);
        if (animStatesChange > 0) {
            for (int i = 0; i < animStatesChange; i++) {
                animsL.Add(new SpriteAnimation("Unnamed"));
            }
        } else {
            for (int i = 0; i < -animStatesChange; i++){
                animsL.RemoveAt(animsL.Count - 1);
            }
        }

        if (animStatesChange != 0) {
            hasRun = false;
        }

        //"Add Draw State"
        //Creates the button and checks if it has been hit
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Draw State")) {
            toAdd = 1;
        }

        //"Remove Draw State"
        //Creates the button and checks if it has been hit
        if (GUILayout.Button("Remove Draw State")) {
            toAdd = -1;
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //The new lists become the previous arrays
        anims = animsL.ToArray();
    }

    public static Sprite[] SplitSprite(Texture2D myTexture, Vector2 slicer) {
        string path = AssetDatabase.GetAssetPath(myTexture);
        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
        ti.isReadable = true;

        if (ti.spriteImportMode == SpriteImportMode.Multiple) {
            // Bug? Need to convert to single then back to multiple in order to make changes when it's already sliced
            ti.spriteImportMode = SpriteImportMode.Single;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        List<SpriteMetaData> newData = new List<SpriteMetaData>();

        int SliceWidth = (int)(myTexture.width/(float)slicer.x);
        int SliceHeight = (int)(myTexture.height/(float)slicer.y);

        //Debug.Log(SliceWidth + " - " + SliceHeight);
        for (int i = 0; i < myTexture.width; i += SliceWidth) {
            for (int j = myTexture.height; j > 0; j -= SliceHeight) {
                SpriteMetaData smd = new SpriteMetaData {
                    pivot = new Vector2(0.5f, 0.5f),
                    alignment = 9,
                    name = myTexture.name + ": " + (myTexture.height - j) / SliceHeight + ", " + i / SliceWidth,
                    rect = new Rect(i, j - SliceHeight, SliceWidth, SliceHeight)
                };
                newData.Add(smd);
            }
        }

        //foreach (var newDat in newData) { Debug.Log(newDat.name); }
        ti.filterMode = FilterMode.Point;
        ti.textureCompression = TextureImporterCompression.Uncompressed;
        ti.spriteImportMode = SpriteImportMode.Multiple;
        ti.spritesheet = newData.ToArray();
        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        //Debug.Log(GetResourcePath(path, 4));
        var sprites = Resources.LoadAll<Sprite>(GetResourcePath(path, 4));
        //foreach (var sprite in sprites) { Debug.Log(sprite.name); }
        return sprites;
    }

    public static string GetResourcePath(string path, int end) {
        const string removePart = "Assets/Resources/";
        if (path.Contains(removePart)) {
            var Start = path.IndexOf(removePart, 0) + removePart.Length;
            return path.Substring(Start, path.Length - Start-end);
        } else {
            return "";
        }
    }
}