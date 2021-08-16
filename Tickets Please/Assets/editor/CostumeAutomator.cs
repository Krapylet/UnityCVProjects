using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Linq;
using System.IO;

public class CostumeAutomater : EditorWindow {

    string lookIn = "Assets/Resources/2D/Thim/Passengers";
    string outputPathMain = "Assets/Resources/Game/Passengers";
    bool includeFirstFolder = false;
    bool alreadyPressed = false;

    [MenuItem("Tickets Please tools/Costume Automator")]
    public static void ShowWindow() {
        GetWindow<CostumeAutomater>("Costume Automator");
    }

    private void OnGUI() {
        GUILayout.Label("Path to recurse through:");
        lookIn = EditorGUILayout.TextField(lookIn);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Include first folder:");
        includeFirstFolder = EditorGUILayout.Toggle(includeFirstFolder);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("Create Costume Parts")) {
            if (!alreadyPressed) {
                alreadyPressed = true;
                if (!includeFirstFolder) {
                    var subs = AssetDatabase.GetSubFolders(lookIn);
                    foreach (var sub in subs) {
                        Debug.Log("Searching in: "+sub);
                        CreateCostumePart(sub);
                    }
                } else {
                    CreateCostumePart(lookIn);
                }
            }
        } else {
            alreadyPressed = false;
        }
    }

    //Instanciate a new costumepart for path and it's subfolders
    private void CreateCostumePart(string path) {
        var assetsPathLocation = Application.dataPath + "\\";
        var assets = Directory.GetFiles(assetsPathLocation + path.Substring("Assets/".Length));
        CostumePart costumePart = CreateInstance<CostumePart>();

        //Search through files in 'path' and make a list of Spriteanimations from it
        List<SpriteAnimation> anims = new List<SpriteAnimation>();
        for (int i = 0; i < assets.Length; i++) {
            var assetString = assets[i];
            var startsAt = assetsPathLocation.Length + "Resources/".Length; //Does math for bellow string
            var asset = assetString.Substring(startsAt).Replace('\\','/'); //Makes it relative to resources folder
            if ((asset.Contains(".png")) && (!asset.Contains(".meta"))) {
                asset = asset.Substring(0, asset.Length - ".png".Length); //Removes .png
                var tex = Resources.Load<Texture2D>(asset);
                string animName = tex.name.Split('-')[0];
                animName = animName.Substring(0, animName.Length - 1);
                //Debug.Log(animName);
                anims.Add(new SpriteAnimation(animName, CostumePartEditor.SplitSprite(tex, new Vector2(1,4))));
            }
        }
        
        //Add file and if folder doesn't exist for it, add it also
        if (anims.Count != 0) {
            costumePart.anims = anims.ToArray();
            var locationToProjectFolder = assetsPathLocation.Substring(0, assetsPathLocation.Length - "assets/".Length);

            var filePath = path.Substring(lookIn.Length);
            filePath = outputPathMain + filePath + ".asset";
            //Debug.Log("Filepath: " + filePath);

            var fileNameLength = filePath.Split('/')[0].Length;
            var folderPath = filePath.Substring(0, filePath.Length - fileNameLength);
            folderPath = locationToProjectFolder + folderPath;
            //Debug.Log("Folderpath: " + folderPath);
            Directory.CreateDirectory(folderPath);
            AssetDatabase.CreateAsset(costumePart, filePath);
        }

        //Do the same for sub folders (this adds recursion)
        var subs = AssetDatabase.GetSubFolders(path);
        foreach (var sub in subs) {
            CreateCostumePart(sub);
        }
    }
}