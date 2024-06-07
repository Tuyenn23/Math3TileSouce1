using UnityEngine;
using UnityEditor;
using MeshTracker;

//---Mesh Tracker RepairTextureModification - Window
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    public class MeshTracker_ModifyRepairTextureWindow : EditorWindow
    {
        public static MeshTracker_Object Sender;

        private static MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality _Sizes = MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x1024;

        private static float _RepairSpeed;
        private static float _RepairInterval;

        public static void Init(MeshTracker_Object send)
        {
            Sender = send;
            _RepairSpeed = Sender.mmt_trackParamsGPUbased.mmt_SurfaceRepairShaderSpeed;
            _RepairInterval = Sender.mmt_trackParamsGPUbased.mmt_SurfaceRepairShaderInterval;
            _Sizes = Sender.mmt_trackParamsGPUbased.mmt_CanvasQuality;
            MeshTracker_ModifyRepairTextureWindow w = (MeshTracker_ModifyRepairTextureWindow)GetWindow(typeof(MeshTracker_ModifyRepairTextureWindow));
            w.minSize = new Vector2(300, 315);
            w.maxSize = new Vector2(310, 325);
            w.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(15);
            GUILayout.Label("Repair Surface - Shader Settings");
            GUILayout.Space(10);
            GUILayout.Label("Presets");
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Very Slow"))
            {
                switch (_Sizes)
                {
                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x2048:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.1f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x1024:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.2f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x512:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.3f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x256:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.4f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x128:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.5f;
                        break;

                    default:
                        _RepairSpeed = 0.005f;
                        _RepairInterval = 0.1f;
                        break;
                }
            }
            if (GUILayout.Button("Slow"))
            {
                switch (_Sizes)
                {
                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x2048:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.1f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x1024:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.2f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x512:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.3f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x256:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.4f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x128:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.5f;
                        break;

                    default:
                        _RepairSpeed = 0.01f;
                        _RepairInterval = 0.1f;
                        break;
                }
            }
            if (GUILayout.Button("Fast"))
            {
                switch (_Sizes)
                {
                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x2048:
                        _RepairSpeed = 0.08f;
                        _RepairInterval = 0.035f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x1024:
                        _RepairSpeed = 0.12f;
                        _RepairInterval = 0.05f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x512:
                        _RepairSpeed = 0.15f;
                        _RepairInterval = 0.06f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x256:
                        _RepairSpeed = 0.18f;
                        _RepairInterval = 0.07f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x128:
                        _RepairSpeed = 0.25f;
                        _RepairInterval = 0.08f;
                        break;

                    default:
                        _RepairSpeed = 0.08f;
                        _RepairInterval = 0.035f;
                        break;
                }
            }
            if (GUILayout.Button("Very Fast"))
            {
                switch (_Sizes)
                {
                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x2048:
                        _RepairSpeed = 0.1f;
                        _RepairInterval = 0.035f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x1024:
                        _RepairSpeed = 0.15f;
                        _RepairInterval = 0.036f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x512:
                        _RepairSpeed = 0.18f;
                        _RepairInterval = 0.038f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x256:
                        _RepairSpeed = 0.2f;
                        _RepairInterval = 0.04f;
                        break;

                    case MeshTracker_Object.TrackParams_GPUbased._mmt_CanvasQuality.x128:
                        _RepairSpeed = 0.3f;
                        _RepairInterval = 0.05f;
                        break;

                    default:
                        _RepairSpeed = 0.1f;
                        _RepairInterval = 0.035f;
                        break;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(10);

            GUILayout.BeginVertical("Box");
            GUILayout.Label("Repair Speed [alpha value] " + _RepairSpeed);
            _RepairSpeed = GUILayout.HorizontalSlider(_RepairSpeed, 0.0001f, 0.5f);
            GUILayout.Space(14);
            _RepairSpeed = EditorGUILayout.FloatField(_RepairSpeed);
            GUILayout.EndVertical();

            GUILayout.Space(10);

            GUILayout.BeginVertical("Box");
            GUILayout.Label("Repair Interval [in seconds] " + _RepairInterval);
            _RepairInterval = GUILayout.HorizontalSlider(_RepairInterval, 0.035f, 60);
            GUILayout.Space(14);
            _RepairInterval = EditorGUILayout.FloatField(_RepairInterval);
            GUILayout.EndVertical();

            if (GUILayout.Button("Edit & Apply"))
            {
                Sender.mmt_trackParamsGPUbased.mmt_SurfaceRepairShaderSpeed = _RepairSpeed;
                Sender.mmt_trackParamsGPUbased.mmt_SurfaceRepairShaderInterval = _RepairInterval;

                Sender = null;
                AssetDatabase.Refresh();
                Close();
            }
        }
    }
}