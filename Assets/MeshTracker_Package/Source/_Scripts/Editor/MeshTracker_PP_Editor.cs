using UnityEngine;
using UnityEditor;
using MeshTracker;

//---Mesh Tracker Procedural Plane [PP] - Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    [CustomEditor(typeof(MeshTracker_ProceduralPlane))]
    [CanEditMultipleObjects]
    public class MeshTracker_PP_Editor : MeshTracker_EditorUtilities
    {
        private MeshTracker_ProceduralPlane asp;

        private void OnEnable()
        {
            asp = (MeshTracker_ProceduralPlane)target;
        }

        public override void OnInspectorGUI()
        {
            ps(5);
            ppDrawProperty("UpdateMeshEveryFrame", "Update Every Frame");
            if (!asp.UpdateMeshEveryFrame)
            {
                if (pb("Generate Plane")) asp.GenerateMesh();
            }

            GUI.color = Color.green;
            ppDrawProperty("GenerateMeshColliderAfterStart", "Generate Collider After Start");
            GUI.color = Color.white;

            pv();

            ppDrawProperty("Plane_length", "Length");
            ppDrawProperty("Plane_width", "Width");

            ps(6);

            ppDrawProperty("Plane_resX", "Resolution");

            pve();

            serializedObject.Update();
        }
    }
}