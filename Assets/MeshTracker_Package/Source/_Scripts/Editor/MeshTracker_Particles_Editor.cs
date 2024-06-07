using UnityEditor;
using UnityEngine;
using MeshTracker;

//---Mesh Tracker Particles - Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    [CustomEditor(typeof(MeshTracker_Particles))]
    [CanEditMultipleObjects]
    public class MeshTracker_Particles_Editor : MeshTracker_EditorUtilities
    {
        private MeshTracker_Particles m;

        private void OnEnable()
        {
            m = (MeshTracker_Particles)target;
        }

        public override void OnInspectorGUI()
        {
            if (target == null) return;

            serializedObject.Update();

            ps(5);

            GUI.backgroundColor = Color.white;
            GUI.color = Color.white;

            ppDrawProperty("CustomTrack", "Custom Track");
            if (m.CustomTrack) EditorGUILayout.HelpBox("This will instantiate new track prefab on every particle collision. Takes more performance and memory allocation!", MessageType.Warning);

            ps(5);

            GUILayout.BeginVertical("Box");

            if (m.CustomTrack)
            {
                ppDrawProperty("TrackPrefab", "Track Prefab");
                ppDrawProperty("TrackLifeTime", "Track LifeTime","Lifetime for track destroy (in seconds)");
            }
            else
            {
                ppDrawProperty("TrackSize", "Track Size");
                ppDrawProperty("TrackGraphic", "Track Graphic");
                ppDrawProperty("AdditionalBrush", "Additional Brush", "Additional custom brush for more details");
            }
            GUILayout.EndVertical();
        }
    }
}