using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

//---Mesh Tracker Startup Window - Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    public class MeshTracker_StartUpWindow : EditorWindow
    {
        public Texture2D Logo;

        public Texture2D Home;
        public Texture2D Web;
        public Texture2D Doc;
        public Texture2D Discord;

        public Font font;
        private GUIStyle style;

        private const string INTERNAL_TrackCreatorDownloadLink = "https://mega.nz/#!i9oGEIAa!L9X0BMm_6atSdlvyGsA-Z_6JlGWwRJhTaXltu8uYZIk";
        private const string INTERNAL_ExampleContentDownloadLink = "https://mega.nz/file/ShwjgYgB#tHewjStXPC9imZmbYJEFtNBc-6PlOoWf3imOLjZYvIk";
        private const string INTERNAL_DiscordLink = "https://discord.gg/Ztr8ghQKqC";

        [MenuItem("Window/Mesh Tracker/StartUp Window")]
        public static void Init()
        {
            MeshTracker_StartUpWindow md = (MeshTracker_StartUpWindow)EditorWindow.GetWindow(typeof(MeshTracker_StartUpWindow));
            md.maxSize = new Vector2(400, 480);
            md.minSize = new Vector2(399, 481);
            md.Show();
        }

        private void OnGUI()
        {
            style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.font = font;
            GUILayout.Label(Logo);
            style.fontSize = 13;
            style.wordWrap = true;

            style.alignment = TextAnchor.MiddleCenter;
            GUILayout.BeginVertical("Box");
            GUILayout.Label("Mesh Tracker Version 1.4\n[Realtime CPU & GPU Simulations]", style);
            GUILayout.EndVertical();

            style.alignment = TextAnchor.UpperLeft;
            GUILayout.Space(5);
            style.fontSize = 12;

            GUILayout.BeginVertical("Box");
            GUILayout.Label("<size=14><color=#87e9f0>Mesh Tracker Package version 1.4  [07/12/2020 <size=8>dd/mm/yyyy</size>]</color></size>\n\n" +
                "- Major upgrade for CPU-Based tracking\n" +
                "  >Added exponential deformation\n" +
                "  >Improved processing performance\n" +
                "- Added 'Custom Canvas' generator feature\n" +
                "- Added 'Tracker Effects' feature\n" +
                "- Added noise feature to brush shader\n" +
                "- Fixed track duplications\n" +
                "- Fixed transparent shader\n" +
                "- Optimized overall tracking system twice\n" +
                "- Updated overall shader source\n" +
                "- Huge code refactor & scene clean-up", style);
            GUILayout.Space(12);
            GUILayout.EndVertical();

            GUILayout.Label("  No idea where to start? Open Documentation to get more!", style);

            GUILayout.Space(5);

            style = new GUIStyle(GUI.skin.button);
            style.imagePosition = ImagePosition.ImageAbove;

            GUILayout.BeginHorizontal("Box");
            if(GUILayout.Button(new GUIContent("Take Me Home",Home), style))
            {
                try
                {
                    EditorSceneManager.OpenScene(Application.dataPath + "/MeshTracker_Package/Examples/Scenes/Introduction.unity");
                }
                catch
                {
                    Debug.LogError("I can't take you home! Directory with example scenes couldn't be found. Required path: [" + Application.dataPath + "/MeshTracker_Package/Examples/Scenes/]");
                }
            }
            if (GUILayout.Button(new GUIContent("Creator's Webpage", Web),style))
                Application.OpenURL("https://matejvanco.com");

            if (GUILayout.Button(new GUIContent("Open Documentation", Doc), style))
            {
                try
                {
                    System.Diagnostics.Process.Start(Application.dataPath + "/MeshTracker_Package/MeshTracker_Documentation.pdf");
                }
                catch
                {
                    Debug.LogError("Documentation could not be found! Required path: "+ Application.dataPath + "/MeshTracker_Package/MeshTracker_Documentation.pdf");
                }
            }

            GUILayout.EndHorizontal();
            if (GUILayout.Button(new GUIContent(Discord)))
                Application.OpenURL(INTERNAL_DiscordLink);
            if (GUILayout.Button(new GUIContent("Download Latest Example Content"), style))
                Application.OpenURL(INTERNAL_ExampleContentDownloadLink);
            if (GUILayout.Button(new GUIContent("Download TrackCreator [V2 - PC_Windows]"), style))
                Application.OpenURL(INTERNAL_TrackCreatorDownloadLink);
        }
    }
}