using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace MeshTracker
{
    //-------------MeshTracker - Surface Drawing
    //--The script allows you to draw on any surface containing mesh collider & mesh renderer with any graphics.
    //---------------------------------------
    //      Additional info
    //  public methods macro = PUBLIC_
    //  accessible variables macro = mmt_
    //---------------------------------------
    //Author: Matej Vanco
    [AddComponentMenu("Matej Vanco/Mesh Tracker/Mesh Tracker Surface Drawing")]
    public class MeshTracker_SurfaceDrawing : MonoBehaviour
    {
        public List<Texture> mmt_TrackGraphics = new List<Texture>();
        public Texture mmt_SelectedTrackGraphic;
        public int mmt_SelectedTrackGraphicIndex;

        public KeyCode mmt_InputKey = KeyCode.Mouse0;
        public float mmt_TrackSize = 100.0f;
        [Range(0.01f,5.0f)]
        public float mmt_TrackStrength = 1.0f;
        public float mmt_TrackHeight = 0.5f;

        public bool mmt_LoadTracksIntoLayoutGroup = false;
        public Transform mmt_LayoutGroupParent;
        public float mmt_ImageSize = 50;
        public UnityEvent mmt_AdditionalOnClickEvent;

        public Camera mmt_CamTarget;
        public bool mmt_MobilePlatform;

        public LayerMask mmt_AllowedLayers = -1;

        public bool mmt_AllObjectsAllowed = true;
        public string mmt_AllowedWithTag;

        public bool mmt_RandomizeTrackDrawing = false;
        public Vector2 mmt_RandomizeIndex;

        private RaycastHit mmt_Hit;
        private Ray mmt_Ray;

        private Material mmt_BMaterial;

        private void Awake()
        {
            mmt_BMaterial = new Material(Shader.Find("Matej Vanco/Mesh Tracker/Mesh Tracker Brush"));

            if (mmt_CamTarget == null)
            {
                mmt_CamTarget = Camera.main;
                if (mmt_CamTarget) return;

                Debug.LogError("Mesh Tracker Surface Drawing - Camera Target is required");
                this.enabled = false;
                return;
            }
        }

        private void Start()
        {
            if (mmt_LoadTracksIntoLayoutGroup == false)
                return;
            if (mmt_LayoutGroupParent == null)
                return;
            if (mmt_ImageSize <= 5)
                mmt_ImageSize = 20;

            for (int i = 0; i < mmt_TrackGraphics.Count; i++)
            {
                Image img = new GameObject(i.ToString()).AddComponent<Image>();
                Button but = img.gameObject.AddComponent<Button>();
                but.transform.SetParent(mmt_LayoutGroupParent.transform,false);
                but.GetComponent<RectTransform>().sizeDelta = new Vector2(mmt_ImageSize, mmt_ImageSize);
                ColorBlock block = but.colors;
                block.highlightedColor = new Color(0, 0, 0, 0);
                but.colors = block;
                img.sprite = Sprite.Create((Texture2D)mmt_TrackGraphics[i],
                new Rect(Vector2.zero, new Vector2(mmt_TrackGraphics[i].width, mmt_TrackGraphics[i].height)), Vector2.zero);
                but.image = img;

                but.onClick.AddListener(delegate { PUBLIC_ChangeTrackGraphic(int.Parse(img.name, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture)); if (mmt_AdditionalOnClickEvent != null) mmt_AdditionalOnClickEvent.Invoke(); });
            }
        }

        private void Update()
        {
            mmt_Hit = new RaycastHit();

            if (mmt_MobilePlatform)
            {
                if (Input.touchCount > 0)
                {
                    for (int i = 0; i < Input.touchCount; i++)
                        mmt_Ray = mmt_CamTarget.ScreenPointToRay(Input.GetTouch(i).position);
                }
                else return;
            }
            else
            {
                mmt_Ray = mmt_CamTarget.ScreenPointToRay(Input.mousePosition);
                if (!Input.GetKey(mmt_InputKey)) return;
            }

            bool RaycastResult = Physics.Raycast(mmt_Ray, out mmt_Hit, Mathf.Infinity, mmt_AllowedLayers);

            if (!RaycastResult)
                return;
            if (UnityEngine.EventSystems.EventSystem.current && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
            if (mmt_RandomizeTrackDrawing)
                mmt_SelectedTrackGraphicIndex = Random.Range((int)mmt_RandomizeIndex.x, (int)mmt_RandomizeIndex.y + 1);

            if (mmt_SelectedTrackGraphicIndex >= mmt_TrackGraphics.Count || mmt_SelectedTrackGraphicIndex < 0)
                mmt_SelectedTrackGraphicIndex = 0;
            if (mmt_TrackGraphics.Count == 0)
                return;

            mmt_BMaterial.SetFloat("_Opacity", mmt_TrackStrength);
            mmt_BMaterial.SetFloat("_Height", mmt_TrackHeight);

            mmt_SelectedTrackGraphic = mmt_TrackGraphics[mmt_SelectedTrackGraphicIndex];
            if (mmt_Hit.collider.transform.GetComponent<MeshTracker_Object>())
            {
                MeshTracker_Object mmtSource = mmt_Hit.collider.transform.GetComponent<MeshTracker_Object>();
                if (mmtSource.mmt_UseGPUbasedType)
                    mmtSource.fGPUbased_CreateTrack(mmt_Hit.textureCoord, mmt_TrackSize, mmt_SelectedTrackGraphic, mmt_BMaterial);
            }
        }

        /// <summary>
        /// Change exists track graphic by specific index in the mmt_TrackGraphics array
        /// </summary>
        public void PUBLIC_ChangeTrackGraphic(int index)
        {
            mmt_SelectedTrackGraphicIndex = index;
        }

        /// <summary>
        /// Change current track size
        /// </summary>
        public void PUBLIC_ChangeTrackSize(float size)
        {
            mmt_TrackSize = size;
        }
        /// <summary>
        /// Change current track size
        /// </summary>
        public void PUBLIC_ChangeTrackSize(Slider size)
        {
            mmt_TrackSize = size.value;
        }

        /// <summary>
        /// Change current track strength
        /// </summary>
        public void PUBLIC_ChangeTrackStrength(float strength)
        {
            mmt_TrackStrength = strength;
        }
        /// <summary>
        /// Change current track strength
        /// </summary>
        public void PUBLIC_ChangeTrackStrength(Slider strength)
        {
            mmt_TrackStrength = strength.value;
        }

        /// <summary>
        /// Change current track height
        /// </summary>
        public void PUBLIC_ChangeTrackHeight(float height)
        {
            mmt_TrackHeight = height;
        }
        /// <summary>
        /// Change current track height
        /// </summary>
        public void PUBLIC_ChangeTrackHeight(Slider height)
        {
            mmt_TrackHeight = height.value;
        }

        /// <summary>
        /// Get selected track graphic to selected image (visualize the selected track)
        /// </summary>
        public void PUBLIC_SetImageToSelectedTrack(Image target)
        {
            if (mmt_TrackGraphics.Count == 0)
                return;
            target.sprite = Sprite.Create((Texture2D)mmt_TrackGraphics[mmt_SelectedTrackGraphicIndex], 
                new Rect(Vector2.zero, new Vector2(mmt_TrackGraphics[mmt_SelectedTrackGraphicIndex].width, mmt_TrackGraphics[mmt_SelectedTrackGraphicIndex].height)), Vector2.zero);
        }
    }
}