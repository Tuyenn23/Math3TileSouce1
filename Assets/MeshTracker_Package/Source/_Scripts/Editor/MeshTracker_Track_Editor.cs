using UnityEngine;
using UnityEditor;
using MeshTracker;

//---Mesh Tracker Track - Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    [CustomEditor(typeof(MeshTracker_Track))]
    [CanEditMultipleObjects]
    public class MeshTracker_Track_Editor : MeshTracker_EditorUtilities
    {
        private MeshTracker_Track m;

        private static MeshTracker_Track.mmt_TrackLayer CopyMeshTrackLayerStorage;

        private void OnEnable()
        {
            m = (MeshTracker_Track)target;
        }

        public override void OnInspectorGUI()
        {
            if (target == null) return;

            serializedObject.Update();

            ps();

            GUI.backgroundColor = Color.white;
            GUI.color = Color.white;

            pv();
            ppDrawProperty("mmt_TargetedToCPUBasedType", "Targeted To CPU-Based Type", "If enabled, the track will be mostly targeted to surfaces with Mesh Tracker Object - CPU-Based type surface. This is optional, the tracks will work on both surfaces either. This will just clear unecessary fields...");
            pve();

            ps();

            ph();
            if (pb("Add Layer"))
            {
                m.TrackLayer.Add(new MeshTracker_Track.mmt_TrackLayer());
                return;
            }
            if (pb("Remove Last Layer") && m.TrackLayer.Count > 0)
            {
                string tName = m.TrackLayer[m.TrackLayer.Count - 1].mmt_TrackName;
                if (string.IsNullOrEmpty(tName)) tName = "Track Layer " + (m.TrackLayer.Count - 1).ToString();
                if (!EditorUtility.DisplayDialog("Question", "Are you sure you want to delete last layer \n'" + tName + "'?\nThere is no way back!", "Yes", "No"))
                    return;
                m.TrackLayer.RemoveAt(m.TrackLayer.Count - 1);
                return;
            }
            phe();

            if (CopyMeshTrackLayerStorage != null)
                GUI.color = Color.white;
            else
                GUI.color = Color.gray;

            if (pb("Paste Layer"))
            {
                if (CopyMeshTrackLayerStorage != null)
                    m.TrackLayer.Add(CopyMeshTrackLayerStorage);

                CopyMeshTrackLayerStorage = null;
                return;
            }

            GUI.color = Color.white;

            if (m.TrackLayer.Count > 0)
            {
                ps(10);

                ppDrawList();
            }
            GUI.backgroundColor = Color.white;

            ps(10);

            pl("Track Effects");

            pv();
            ppDrawProperty("mmt_useEffects", "Use Track Effects", "If enabled, the system will keep checking the surface every frame");
            if (m.mmt_useEffects)
            {
                ppDrawProperty("mmt_effectQuality", "Effects Quality", "If enabled, you will be able to check the surface in specified interval");
                if(m.mmt_effectQuality == MeshTracker_Track.eEffectQuality.Custom)
                {
                    ppDrawProperty("mmt_CustomTrackBuffers", "Custom Quality", "Type custom quality value. The higher number is, the more raycast passes you will get, but the more performance it will take");
                    EditorGUILayout.HelpBox("It's highly recommended to use preset quality settings to avoid performance issues!", MessageType.Warning);
                }
            }
            pve();

            ps(10);

            pl("Update Logic");

            pv();
            ppDrawProperty("mmt_AlwaysCheckSurface", "Always Check Surface", "If enabled, the system will keep checking the surface every frame");
            if (!m.mmt_AlwaysCheckSurface)
            {
                ppDrawProperty("mmt_UseInterval", "Use Intervals", "If enabled, you will be able to check the surface in specified interval");
                if (m.mmt_UseInterval)
                    ppDrawProperty("mmt_Interval", "Interval [In Seconds]");
            }
            pve();
        }

        private bool foldoutEffects = false;

        /// <summary>
        /// Draw track list
        /// </summary>
        private void ppDrawList()
        {
            for (int i = 0; i < m.TrackLayer.Count; i++)
            {
                GUI.backgroundColor = m.TrackLayer[i].mmt_TrackColor;

                pv();

                SerializedProperty item = serializedObject.FindProperty("TrackLayer").GetArrayElementAtIndex(i);
                string Tname = m.TrackLayer[i].mmt_TrackName;
                if (string.IsNullOrEmpty(Tname)) Tname = "Track Layer " + i.ToString();

                ppDrawProperty(item, Tname);

                if (!item.isExpanded)
                {
                    pve();
                    continue;
                }

                ps(5);

                EditorGUI.indentLevel += 1;

                pl("  Track Basics");
                pv();//-----------------------------1
                pv();
                if (!m.TrackLayer[i].mmt_FixObjectScaleWithTrackSize)
                    ppDrawProperty(item.FindPropertyRelative("mmt_TrackSize"), "Track Size");
                else
                {
                    ppDrawProperty(item.FindPropertyRelative("mmt_GetScaleFromRoot"), "Get Scale From Root","If enabled, the scale value will be received from this objects root transform");
                    ppDrawProperty(item.FindPropertyRelative("mmt_InternalScaleMultiplier"), "Internal Scale Multiplier","Additional scale multiplier");
                }

                ppDrawProperty(item.FindPropertyRelative("mmt_FixObjectScaleWithTrackSize"), "Objects Scale Is Track Size","If enabled, the track size will be equal to the current objects local scale");
                pve();

                if (!m.mmt_TargetedToCPUBasedType)
                {
                    ps(3);

                    pv();
                    ppDrawProperty(item.FindPropertyRelative("mmt_TrackGraphic"), "Track Graphic", "Enter track graphic. If surface will be set to 'Script Type', this field will be ignored");
                    ppDrawProperty(item.FindPropertyRelative("mmt_TrackBrush"), "Track Brush Type", "(optional) Enter material with brush shader. Add more details to your tracks [This field is not required]");
                    pve();

                    ps(3);

                    if (m.TrackLayer[i].mmt_TrackGraphic)
                    {
                        pv();
                        ppDrawProperty(item.FindPropertyRelative("mmt_UseSmartLayerRotation"), "Use Smart Layer Rotation", "If enabled, track graphic will rotate by the object transform move direction (Especially for Smart Layers)");
                        if (m.TrackLayer[i].mmt_UseSmartLayerRotation && m.TrackLayer[i].mmt_TrackBrush == null)
                            EditorGUILayout.HelpBox("The Track Brush Type is required while using the Smart Layers", MessageType.Warning);
                        if (m.TrackLayer[i].mmt_UseSmartLayerRotation == false)
                            ppDrawProperty(item.FindPropertyRelative("mmt_CopyObjectRotation"), "Copy Transform Rotation", "If enabled, track graphic will rotate by the object transform rotation Y Axis");
                        else ppDrawProperty(item.FindPropertyRelative("mmt_InverseSmartLayerRotation"), "Inverse Smart Layer Rotation", "If enabled, the Y rotation of the smart track will be inversed by 180 degrees");
                        pve();
                    }
                }
                ps(3);
                ppDrawProperty(item.FindPropertyRelative("mmt_TrackName"), "Track Name [Editor]");
                ppDrawProperty(item.FindPropertyRelative("mmt_TrackColor"), "Track Color [Editor]");
                pve();//-----------------------------1

                ps(10);

                pl("  Ray Settings");
                pv();//-----------------------------2
                ppDrawProperty(item.FindPropertyRelative("mmt_RayOriginIsCursor"), "Ray Origin Is Cursor", "If enabled, you will be able to create track from cursor position");
                if (!m.TrackLayer[i].mmt_RayOriginIsCursor)
                {
                    ppDrawProperty(item.FindPropertyRelative("mmt_RayDirection"), "Ray Direction", "Global ray direction (in world space)");
                    ppDrawProperty(item.FindPropertyRelative("mmt_RayOriginOffset"), "Ray Origin Offset");
                }
                else
                {
                    ppDrawProperty(item.FindPropertyRelative("mmt_CameraTarget"), "Camera Target", "Enter target camera for raycast origin");
                    ppDrawProperty(item.FindPropertyRelative("mmt_MobilePlatform"), "Mobile Platform", "If enabled, ray origin will be set to 'touch position'");
                    if (!m.TrackLayer[i].mmt_MobilePlatform)
                    {
                        ppDrawProperty(item.FindPropertyRelative("mmt_InputEvent"), "Enable Input");
                        if (m.TrackLayer[i].mmt_InputEvent)
                            ppDrawProperty(item.FindPropertyRelative("mmt_InputKey"), "Key");
                    }
                }

                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_RayDistance"), "Ray Distance", "");
                pve();

                pve();//-----------------------------2

                ps(10);

                pl("  Conditions");
                pv();//-----------------------------3
                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_AllowedLayers"), "Allowed Layers");
                pve();

                ps(5);

                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_AllObjectsAllowed"), "All Objects Allowed", "If enabled, all surfaces will be allowed for this ray");
                if (!m.TrackLayer[i].mmt_AllObjectsAllowed)
                    ppDrawProperty(item.FindPropertyRelative("mmt_AllowedWithTag"), "Allowed Tags");
                pve();

                if (!m.TrackLayer[i].mmt_RayOriginIsCursor)
                {
                    pv();
                    ps(5);
                    ppDrawProperty(item.FindPropertyRelative("mmt_UseSpeedLimits"), "Use Speed Limits","If enabled, you will be able to use speed limits & access to the object's speed parameter");
                    if (m.TrackLayer[i].mmt_UseSpeedLimits)
                    {
                        ppDrawProperty(item.FindPropertyRelative("mmt_SpeedLimitMin"), "Minimum Speed Limit");
                        ppDrawProperty(item.FindPropertyRelative("mmt_SpeedLimitMax"), "Maximum Speed Limit");
                    }
                    pve();

                    GUI.color = Color.grey;
                    if (m.TrackLayer[i].mmt_UseSpeedLimits)
                        ppDrawProperty("speed", "Debug: Object Speed");
                }
                GUI.color = Color.white;
                pve();//-----------------------------3

                if (m.mmt_useEffects)
                {
                    ps(10);

                    pl("  Track Effects");
                    pv();//-----------------------------4
                    ppDrawProperty(item.FindPropertyRelative("mmt_enabledTrackEffects"), "Enable Effects");
                    if (m.TrackLayer[i].mmt_enabledTrackEffects)
                        ppDrawEffectList(m.TrackLayer[i], item);
                    pve();//-----------------------------4
                }

                ps(10);

                pl("  Events");
                pv();//-----------------------------5
                ppDrawProperty(item.FindPropertyRelative("mmt_EnableEvents"), "Enable Events");
                if (m.TrackLayer[i].mmt_EnableEvents)
                {
                    ppDrawProperty(item.FindPropertyRelative("mmt_Event_UpdateEventEveryFrame"), "Update Events Every Frame");
                    ppDrawProperty(item.FindPropertyRelative("mmt_Event_SurfaceDetected"), "On Surface Detected");
                    ppDrawProperty(item.FindPropertyRelative("mmt_Event_SurfaceExit"), "On Surface Exit");
                }
                pve();//-----------------------------5

                EditorGUI.indentLevel -= 1;

                ps(10);

                ph();
                if (pb("Copy"))
                {
                    MeshTracker_Track.mmt_TrackLayer t = new MeshTracker_Track.mmt_TrackLayer();
                    t.mmt_TrackName = m.TrackLayer[i].mmt_TrackName;
                    t.mmt_TrackColor = m.TrackLayer[i].mmt_TrackColor;
                    t.mmt_TrackSize = m.TrackLayer[i].mmt_TrackSize;
                    t.mmt_TrackGraphic = m.TrackLayer[i].mmt_TrackGraphic;
                    if (m.TrackLayer[i].mmt_TrackBrush) t.mmt_TrackBrush = m.TrackLayer[i].mmt_TrackBrush;
                    else t.mmt_TrackBrush = null;
                    if (m.TrackLayer[i].mmt_CameraTarget) t.mmt_CameraTarget = m.TrackLayer[i].mmt_CameraTarget;
                    else t.mmt_CameraTarget = null;
                    t.mmt_CopyObjectRotation = m.TrackLayer[i].mmt_CopyObjectRotation;
                    t.mmt_InverseSmartLayerRotation = m.TrackLayer[i].mmt_InverseSmartLayerRotation;
                    t.mmt_UseSmartLayerRotation = m.TrackLayer[i].mmt_UseSmartLayerRotation;
                    t.mmt_FixObjectScaleWithTrackSize = m.TrackLayer[i].mmt_FixObjectScaleWithTrackSize;
                    t.mmt_GetScaleFromRoot = m.TrackLayer[i].mmt_GetScaleFromRoot;
                    t.mmt_InternalScaleMultiplier = m.TrackLayer[i].mmt_InternalScaleMultiplier;
                    t.mmt_RayOriginIsCursor = m.TrackLayer[i].mmt_RayOriginIsCursor;
                    t.mmt_MobilePlatform = m.TrackLayer[i].mmt_MobilePlatform;
                    t.mmt_InputEvent = m.TrackLayer[i].mmt_InputEvent;
                    t.mmt_InputKey = m.TrackLayer[i].mmt_InputKey;
                    t.mmt_RayDirection = m.TrackLayer[i].mmt_RayDirection;
                    t.mmt_RayDistance = m.TrackLayer[i].mmt_RayDistance;
                    t.mmt_RayOriginOffset = m.TrackLayer[i].mmt_RayOriginOffset;
                    t.mmt_AllowedLayers = m.TrackLayer[i].mmt_AllowedLayers;
                    t.mmt_AllObjectsAllowed = m.TrackLayer[i].mmt_AllObjectsAllowed;
                    t.mmt_AllowedWithTag = m.TrackLayer[i].mmt_AllowedWithTag;
                    t.mmt_UseSpeedLimits = m.TrackLayer[i].mmt_UseSpeedLimits;
                    t.mmt_SpeedLimitMin = m.TrackLayer[i].mmt_SpeedLimitMin;
                    t.mmt_SpeedLimitMax = m.TrackLayer[i].mmt_SpeedLimitMax;
                    t.mmt_EnableEvents = m.TrackLayer[i].mmt_EnableEvents;
                    t.mmt_Event_UpdateEventEveryFrame = m.TrackLayer[i].mmt_Event_UpdateEventEveryFrame;
                    t.mmt_Event_SurfaceDetected = m.TrackLayer[i].mmt_Event_SurfaceDetected;
                    t.mmt_Event_SurfaceExit = m.TrackLayer[i].mmt_Event_SurfaceExit;

                    t.mmt_enabledTrackEffects = m.TrackLayer[i].mmt_enabledTrackEffects;
                    t.mmt_trackEffects = m.TrackLayer[i].mmt_trackEffects.Clone();

                    CopyMeshTrackLayerStorage = t;
                    return;
                }
                ps(20);
                if (pb("Duplicate"))
                {
                    MeshTracker_Track.mmt_TrackLayer t = new MeshTracker_Track.mmt_TrackLayer();
                    t.mmt_TrackName = m.TrackLayer[i].mmt_TrackName;
                    t.mmt_TrackColor = m.TrackLayer[i].mmt_TrackColor;
                    t.mmt_TrackSize = m.TrackLayer[i].mmt_TrackSize;
                    t.mmt_TrackGraphic = m.TrackLayer[i].mmt_TrackGraphic;
                    if (m.TrackLayer[i].mmt_TrackBrush) t.mmt_TrackBrush = m.TrackLayer[i].mmt_TrackBrush;
                    else t.mmt_TrackBrush = null;
                    if (m.TrackLayer[i].mmt_CameraTarget) t.mmt_CameraTarget = m.TrackLayer[i].mmt_CameraTarget;
                    else t.mmt_CameraTarget = null;
                    t.mmt_CopyObjectRotation = m.TrackLayer[i].mmt_CopyObjectRotation;
                    t.mmt_InverseSmartLayerRotation = m.TrackLayer[i].mmt_InverseSmartLayerRotation;
                    t.mmt_UseSmartLayerRotation = m.TrackLayer[i].mmt_UseSmartLayerRotation;
                    t.mmt_FixObjectScaleWithTrackSize = m.TrackLayer[i].mmt_FixObjectScaleWithTrackSize;
                    t.mmt_GetScaleFromRoot = m.TrackLayer[i].mmt_GetScaleFromRoot;
                    t.mmt_InternalScaleMultiplier = m.TrackLayer[i].mmt_InternalScaleMultiplier;
                    t.mmt_RayOriginIsCursor = m.TrackLayer[i].mmt_RayOriginIsCursor;
                    t.mmt_MobilePlatform = m.TrackLayer[i].mmt_MobilePlatform;
                    t.mmt_InputEvent = m.TrackLayer[i].mmt_InputEvent;
                    t.mmt_InputKey = m.TrackLayer[i].mmt_InputKey;
                    t.mmt_RayDirection = m.TrackLayer[i].mmt_RayDirection;
                    t.mmt_RayDistance = m.TrackLayer[i].mmt_RayDistance;
                    t.mmt_RayOriginOffset = m.TrackLayer[i].mmt_RayOriginOffset;
                    t.mmt_AllowedLayers = m.TrackLayer[i].mmt_AllowedLayers;
                    t.mmt_AllObjectsAllowed = m.TrackLayer[i].mmt_AllObjectsAllowed;
                    t.mmt_AllowedWithTag = m.TrackLayer[i].mmt_AllowedWithTag;
                    t.mmt_UseSpeedLimits = m.TrackLayer[i].mmt_UseSpeedLimits;
                    t.mmt_SpeedLimitMin = m.TrackLayer[i].mmt_SpeedLimitMin;
                    t.mmt_SpeedLimitMax = m.TrackLayer[i].mmt_SpeedLimitMax;
                    t.mmt_EnableEvents = m.TrackLayer[i].mmt_EnableEvents;
                    t.mmt_Event_UpdateEventEveryFrame = m.TrackLayer[i].mmt_Event_UpdateEventEveryFrame;
                    t.mmt_Event_SurfaceDetected = m.TrackLayer[i].mmt_Event_SurfaceDetected;
                    t.mmt_Event_SurfaceExit = m.TrackLayer[i].mmt_Event_SurfaceExit;

                    t.mmt_enabledTrackEffects = m.TrackLayer[i].mmt_enabledTrackEffects;
                    t.mmt_trackEffects = m.TrackLayer[i].mmt_trackEffects.Clone();

                    m.TrackLayer.Add(t);
                    return;
                }
                if (pb("Remove"))
                {
                    string tName = m.TrackLayer[i].mmt_TrackName;
                    if (string.IsNullOrEmpty(tName)) tName = "Track Layer " + i.ToString();
                    if (!EditorUtility.DisplayDialog("Question", "Are you sure you want to delete layer \n'" + tName + "'?\nThere is no way back!", "Yes", "No"))
                        return;
                    m.TrackLayer.RemoveAt(i);
                    return;
                }
                phe();
                ppDrawProperty(item.FindPropertyRelative("mmt_Debug_ShowDebugGraphic"), "Show Debug Graphics");

                pve();
            }
        }

        /// <summary>
        /// Draw effect track list
        /// </summary>
        private void ppDrawEffectList(MeshTracker_Track.mmt_TrackLayer t, SerializedProperty prop)
        {
            SerializedProperty item = prop.FindPropertyRelative("mmt_trackEffects");

            EditorGUI.indentLevel += 1;
            foldoutEffects = EditorGUILayout.Foldout(foldoutEffects, "Track Effects");
            if (foldoutEffects)
            {
                ps(5);

                if (!m.mmt_TargetedToCPUBasedType)
                {
                    pv();
                    ppDrawProperty(item.FindPropertyRelative("mmt_useTrackReferences"), "Use Original Track Reference", "If enabled, the track graphic and track brush will be received from the track above. Otherwise new fields will appear");
                    if (!t.mmt_trackEffects.mmt_useTrackReferences)
                    {
                        ppDrawProperty(item.FindPropertyRelative("mmt_TrackGraphic"), "Effect Track Graphic", "New track graphic for the effect");
                        ppDrawProperty(item.FindPropertyRelative("mmt_TrackBrush"), "Effect Track Brush", "New track brush for the effect");
                    }
                    pve();
                    ps();

                    pv();
                    ppDrawProperty(item.FindPropertyRelative("mmt_SmartTrackRotation"), "Smart Track Rotation", "If enabled, the effect track will use the Smart Track Rotation technique");
                    if (!t.mmt_trackEffects.mmt_SmartTrackRotation)
                        ppDrawProperty(item.FindPropertyRelative("mmt_LocalSpace"), "Local Space", "If enabled, the track direction will be set to objects local space, otherwise the direction will be global");
                    else
                    {
                        if (t.mmt_trackEffects.mmt_useTrackReferences && !t.mmt_TrackBrush)
                            EditorGUILayout.HelpBox("For using Smart Track Rotation, the Track Brush in reference track is required!", MessageType.Error);
                        else if (!t.mmt_trackEffects.mmt_useTrackReferences && !t.mmt_trackEffects.mmt_TrackBrush)
                            EditorGUILayout.HelpBox("For using Smart Track Rotation, the Track Brush is required!", MessageType.Error);
                    }
                    pve();
                }
                else
                {
                    if (t.mmt_trackEffects.mmt_SmartTrackRotation) t.mmt_trackEffects.mmt_SmartTrackRotation = false;
                    ppDrawProperty(item.FindPropertyRelative("mmt_LocalSpace"), "Local Space", "If enabled, the track direction will be set to objects local space, otherwise the direction will be global");
                }

                pv();
                if (t.mmt_trackEffects.mmt_SmartTrackRotation || t.mmt_trackEffects.mmt_LocalSpace)
                    ppDrawProperty(item.FindPropertyRelative("mmt_Direction"), "Direction Offset", "Direction offset in DEGREES of the effect track [use mostly the Y field for rotation offset left & right]");
                else
                    ppDrawProperty(item.FindPropertyRelative("mmt_Direction"), "Direction", "Global direction of the effect track");

                ppDrawProperty(item.FindPropertyRelative("mmt_useDoubleCast"), "Use Double Effect", "If enabled, the raycasting will increase 2x, so you will be able to cast two rays at once in different directions");
                if (t.mmt_trackEffects.mmt_useDoubleCast)
                {
                    if (t.mmt_trackEffects.mmt_SmartTrackRotation || t.mmt_trackEffects.mmt_LocalSpace)
                        ppDrawProperty(item.FindPropertyRelative("mmt_DirectionDouble"), "Second Direction Offset", "Second direction offset in DEGREES of the second effect track [use mostly the Y field for rotation offset left & right]");
                    else
                        ppDrawProperty(item.FindPropertyRelative("mmt_DirectionDouble"), "Second Direction", "Global second direction of the second effect track");
                }
                pve();

                ps(5);

                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_MotionSpeed"), "Motion Speed");
                ppDrawProperty(item.FindPropertyRelative("mmt_LinearMotion"), "Linear Motion", "If enabled, the track motion & lifetime will be linear, otherwise the track will exponentially slow down or faster up");
                pv();
                if (!t.mmt_trackEffects.mmt_LinearMotion)
                    ppDrawProperty(item.FindPropertyRelative("mmt_MotionDrag"), "Motion Drag", "Expontential value of the motion drag. The lower value is, the higher expontential value is");
                pve();
                pve();

                ps();

                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_adjustLifetimebyObjectSpeed"), "Adjust Life Time By Speed", "If enabled, the lifetime will adjust by the objects speed. The faster the object is, the lower lifetime value will be");
                ppDrawProperty(item.FindPropertyRelative("mmt_EffectLifetime"), t.mmt_trackEffects.mmt_adjustLifetimebyObjectSpeed ? "Lifetime Thresholder" : "Effect Lifetime", "Overall effect lifetime in SECONDS");
                pve();

                ps();

                if (!m.mmt_TargetedToCPUBasedType)
                {
                    pv();
                    ppDrawProperty(item.FindPropertyRelative("mmt_ChangeBrushOpacity"), "Change Brush Opacity", "If enabled, the track will change the brush opacity by the overall lifetime (if there is any brush)");
                    if (t.mmt_trackEffects.mmt_ChangeBrushOpacity)
                    {
                        ppDrawProperty(item.FindPropertyRelative("mmt_StartBrushOpacity"), "Starting Opacity");
                        ppDrawProperty(item.FindPropertyRelative("mmt_EndBrushOpacity"), "Ending Opacity");
                        if (t.mmt_trackEffects.mmt_useTrackReferences && !t.mmt_TrackBrush)
                            EditorGUILayout.HelpBox("For using Brush Opacity effect, the Track Brush in reference track is required!", MessageType.Error);
                        else if (!t.mmt_trackEffects.mmt_useTrackReferences && !t.mmt_trackEffects.mmt_TrackBrush)
                            EditorGUILayout.HelpBox("For using Brush Opacity effect, the Track Brush is required!", MessageType.Error);
                    }
                    pve();

                    ps();
                }
                else
                {
                    if (t.mmt_trackEffects.mmt_ChangeBrushOpacity) t.mmt_trackEffects.mmt_ChangeBrushOpacity = false;
                }

                pv();
                ppDrawProperty(item.FindPropertyRelative("mmt_ChangeTrackSize"), "Change Track Size", "If enabled, the track will change the track size by the overall lifetime");
                if (t.mmt_trackEffects.mmt_ChangeTrackSize)
                {
                    ppDrawProperty(item.FindPropertyRelative("mmt_StartTrackSize"), "Starting Size");
                    ppDrawProperty(item.FindPropertyRelative("mmt_EndTrackSize"), "Ending Size");
                }
                pve();


                ps(5);
            }
            EditorGUI.indentLevel -= 1;
        }
    }
}