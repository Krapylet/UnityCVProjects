using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Level))]
public class LevelEditor : Editor {
    private int probs = 0;
    private int toAdd = 0;

    private int probsP = 0;
    private int toAddP = 0;

    public override void OnInspectorGUI() {
        Level level = (Level)target;

        //Add banner with font
        var banner = Resources.Load<Texture2D>("2D/Thim/Env/SkinnerTEST");
        var bannerPlacement = new Rect(0, 0, Screen.width, ((float)banner.height / (float)banner.width) * Screen.width);
        var guiStyle = Resources.Load<GUISkin>("Game/Levels/CreatorSkins").GetStyle("Titel");
        guiStyle.fontSize = (int)(bannerPlacement.height / 3f);
        GUI.DrawTexture(bannerPlacement, banner);
        GUI.Label(bannerPlacement, "Level Creator", guiStyle);

        //Add logo in top right
        var logo = Resources.Load<Texture2D>("2D/Griner2");
        var logoPlacement = new Rect(Screen.width - (Screen.width / 10f), 0, Screen.width / 10f, Screen.width / 10f);
        GUI.DrawTexture(logoPlacement, logo);

        //Add main body
        var mainRect = new Rect(0, bannerPlacement.height+10, Screen.width, 1.5f * Screen.width);
        var mainBG = new Texture2D(1, 1);
        mainBG.SetPixel(0, 0, new Color(22,22,22 , 255));
        mainBG.Apply();
        GUI.DrawTexture(mainRect, mainBG);

        //Begin the main area
        GUILayout.BeginArea(mainRect);

        //"Train start/stop time" UI
        AddBetween("Train start/stop time", ref level.trainStartTime, ref level.trainStopTime);
        GUILayout.Space(10);

        //"Background music/Wagons to add/Passengers to add" UI
        GUILayout.Label("Background music/Wagons to add/Passengers to add:");
        GUILayout.BeginHorizontal();
        level.backgroundMusic = (AudioClip)EditorGUILayout.ObjectField(level.backgroundMusic, typeof(AudioClip), false);
        level.wagonToAdd = (Wagon)EditorGUILayout.ObjectField(level.wagonToAdd, typeof(Wagon), false);
        level.passengerCount = EditorGUILayout.IntField(level.passengerCount);
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //"Passenger spawn min/max interval" UI
        AddBetween("Passenger spawn min/max interval", ref level.spawnMinInterval, ref level.spawnMaxInterval);
        GUILayout.Space(10);

        //Adds the two chance panels
        AddChances("Ticket legality chances", ref probs, ref toAdd, ref level.probs, "Game/Chances/DefaultTicketLegality");
        AddChances("Passenger customes", ref probsP, ref toAddP, ref level.probsP, "Game/Chances/DefaultCloth");

        GUILayout.EndArea();
    }

    //Adds two float panels besides eachother and a name above
    private static void AddBetween(string name, ref float min, ref float max) {
        GUILayout.Label(name);
        GUILayout.BeginHorizontal();
        min = EditorGUILayout.FloatField(min);
        max = EditorGUILayout.FloatField(max);
        GUILayout.EndHorizontal();
    }

    //Adds a chances panel
    public static void AddChances(string name, ref int probs, ref int toAdd, ref Chances chances, string defaultChances) {
        if (chances == null) {
            chances = Resources.Load<Chances>(defaultChances);
        }

        //Get the current amount of chances
        probs = chances.probs.Length;

        //Add a field to change the chance count
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label(name+":");
        probs = EditorGUILayout.IntField(probs) + toAdd;
        chances = (Chances)EditorGUILayout.ObjectField(chances, typeof(Chances), false);
        toAdd = 0;
        GUILayout.EndHorizontal();

        //Makes a list equal to the current chance and name arrays
        //and looks at how many elements it should add/remove from it
        var probChange = probs - chances.probs.Length;
        List<float> probsL = new List<float>(chances.probs);
        List<string> probNamesL = new List<string>(chances.probNames);
        if (probChange > 0) {
            for (int i = 0; i < probChange; i++) {
                probsL.Add(0f);
                probNamesL.Add("");
            }
        } else {
            for (int i = 0; i < -probChange; i++){
                probsL.RemoveAt(probsL.Count - 1);
                probNamesL.RemoveAt(probNamesL.Count - 1);
            }
        }
        
        //Make a row for each chance (with name and the chance)
        for (int i = 0; i < probs; i++) {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            probNamesL[i] = EditorGUILayout.TextField(probNamesL[i]);

            GUILayout.Label("Chance:");
            probsL[i] = EditorGUILayout.FloatField(probsL[i]);
            GUILayout.EndHorizontal(); 
        }

        //"Add probability"
        //Creates the button and checks if it has been hit
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add probability")) {
            toAdd = 1;
        }

        //"Remove probability"
        //Creates the button and checks if it has been hit
        if (GUILayout.Button("Remove probability")) {
            toAdd = -1;
        }

        //"Recalibrate" by going through each chance, changeing it according to the sum of the chances
        //Creates the button and checks if it has been hit
        if (GUILayout.Button("Recalibrate")) {
            var sum = Sum(probsL);
            for (int i = 0; i < probsL.Count; i++) {
                probsL[i] = (probsL[i] / sum)*100;
            }
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);

        //The new lists become the previous arrays
        chances.probs = probsL.ToArray();
        chances.probNames = probNamesL.ToArray();
    }

    //Takes the sum of a list of floats
    public static float Sum(IEnumerable<float> source) {
        double sum = 0;
        foreach (float v in source) sum += v;
        return (float)sum;
    }
}