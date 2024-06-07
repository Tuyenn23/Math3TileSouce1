using UnityEngine;
using UnityEditor;
using MeshTracker;

//---Mesh Tracker Surface Drawing - Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    [CustomEditor(typeof(MeshTracker_SurfaceDrawing))]
    [CanEditMultipleObjects]
    public class MeshTracker_SurfaceDrawing_Editor : MeshTracker_EditorUtilities
    {
        private MeshTracker_SurfaceDrawing msd;

        private Vector2 scroll;
        private float indx;
        private bool useSlider = true;

        private void OnEnable()
        {
            msd = (MeshTracker_SurfaceDrawing)target;
            indx = msd.mmt_SelectedTrackGraphicIndex;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("Surface Drawing is compatible with MeshTracker_Object GPU-Based system only!", MessageType.Warning);
            ps();
            pl("Track Graphics Palette", true);
            pv();
            ppDrawProperty("mmt_TrackGraphics", "Track Graphics", "", true);

            scroll = GUILayout.BeginScrollView(scroll);
            ph();
            for (int i = 0; i < msd.mmt_TrackGraphics.Count; i++)
            {
                if (i == msd.mmt_SelectedTrackGraphicIndex)
                    pv();
                GUILayout.Label(new GUIContent(i.ToString(), msd.mmt_TrackGraphics[i]), GUILayout.Width(50), GUILayout.Height(50));
                if (i == msd.mmt_SelectedTrackGraphicIndex)
                    pve();
            }
            phe();
            GUILayout.EndScrollView();
            if (msd.mmt_TrackGraphics.Count > 0)
            {
                if (pb("Remove Last"))
                    msd.mmt_TrackGraphics.RemoveAt(msd.mmt_TrackGraphics.Count - 1);
            }
            if (useSlider && !Application.isPlaying)
            {
                indx = GUILayout.HorizontalSlider(indx, 0, msd.mmt_TrackGraphics.Count - 1);
                if (msd.mmt_SelectedTrackGraphicIndex != (int)indx)
                    msd.mmt_SelectedTrackGraphicIndex = Mathf.RoundToInt(indx);
            }
            ps(20);
            useSlider = GUILayout.Toggle(useSlider, "Use Smooth Slider");

            ppDrawProperty("mmt_SelectedTrackGraphicIndex", "Selected Index", "Index of the track list above");
            pve();

            pv();
            ppDrawProperty("mmt_RandomizeTrackDrawing", "Randomize Track Drawing");
            if (msd.mmt_RandomizeTrackDrawing)
            {
                ps();
                pl("          From index " + msd.mmt_RandomizeIndex.x.ToString());
                ps(5);
                msd.mmt_RandomizeIndex.x = GUILayout.HorizontalSlider(msd.mmt_RandomizeIndex.x, 0, msd.mmt_TrackGraphics.Count - 1);
                ps(20);
                pl("          To index " + msd.mmt_RandomizeIndex.y.ToString());
                ps(5);
                msd.mmt_RandomizeIndex.y = GUILayout.HorizontalSlider(msd.mmt_RandomizeIndex.y, 0, msd.mmt_TrackGraphics.Count - 1);
                ps(20);
                msd.mmt_RandomizeIndex.x = Mathf.RoundToInt(msd.mmt_RandomizeIndex.x);
                msd.mmt_RandomizeIndex.y = Mathf.RoundToInt(msd.mmt_RandomizeIndex.y);
            }
            pve();

            ps(5);

            pl("Input & Platform", true);
            pv();
            ppDrawProperty("mmt_CamTarget", "Camera Target","Main Camera Target that will represent the 'Raycast Origin'");
            ppDrawProperty("mmt_MobilePlatform", "Mobile Platform");
            if (msd.mmt_MobilePlatform == false)
                ppDrawProperty("mmt_InputKey", "Input Key");
            pve();

            ps(5);

            pl("Track Parameters", true);
            pv();
            ppDrawProperty("mmt_TrackSize", "Track Size");
            ppDrawProperty("mmt_TrackStrength", "Track Opacity");
            ppDrawProperty("mmt_TrackHeight", "Track Height");
            ps(5);
            ppDrawProperty("mmt_LoadTracksIntoLayoutGroup", "Load Tracks Into Layout Group","If enabled, you will be able to visualize assigned tracks into UI images group");
            if (msd.mmt_LoadTracksIntoLayoutGroup)
            {
                ppDrawProperty("mmt_LayoutGroupParent", "Layout Group Parent");
                ppDrawProperty("mmt_ImageSize", "Image Size");
                ps(5);
                ppDrawProperty("mmt_AdditionalOnClickEvent", "Additional On Click Event", "Additional event if any generated track image button is pressed");
            }
            pve();

            ps(5);

            pl("Conditions", true);
            pv();
            ppDrawProperty("mmt_AllowedLayers", "Allowed Layers");
            ppDrawProperty("mmt_AllObjectsAllowed", "All Objects Allowed");
            if (!msd.mmt_AllObjectsAllowed)
                ppDrawProperty("mmt_AllowedWithTag", "Allowed Object With Tag");
            pve();
            ps();

            serializedObject.Update();
        }
    }
}