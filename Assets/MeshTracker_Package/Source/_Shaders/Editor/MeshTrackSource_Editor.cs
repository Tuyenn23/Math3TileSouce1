using UnityEditor;

//---Mesh Tracker - Material Source Editor
//Author: Matej Vanco

namespace MeshTrackerEditor
{
    public class MeshTrackSource_Editor : MeshTracker_MaterialEditorUtilities
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            ps(20);
            pl("Colors");
            pv();

            pv();
            ppDrawProperty(materialEditor, properties, "_ColorUp");
            ppDrawProperty(materialEditor, properties, "_ColorDown");
            pve();
            pv();
            ppDrawProperty(materialEditor, properties, "_ColorDown_Power");
            ppDrawProperty(materialEditor, properties, "_ColorUp_Power");
            pve();
            ppDrawProperty(materialEditor, properties, "_ColorAlpha");
            ppDrawProperty(materialEditor, properties, "_HIsAlpha", false, "If enabled, the alpha value will be controlled by the height-map data (grayscale)");
            ppDrawProperty(materialEditor, properties, "_InterpolMulti", false, "Blend value multiplier between Up & Down");

            pve();

            ps(10);
            pl("Material Settings");
            pv();

            ps(5);
            pv();
            ppDrawProperty(materialEditor, properties, "_MainTex", true);
            ppDrawProperty(materialEditor, properties, "_SecTex", true);
            pve();
            ps(5);
            pv();
            ppDrawProperty(materialEditor, properties, "_SecNormal", true);
            ppDrawProperty(materialEditor, properties, "_MainNormal", true);
            ppDrawProperty(materialEditor, properties, "_NormalAmount");
            pve();
            ps(5);
            ppDrawProperty(materialEditor, properties, "_EnableEmission");
            if (ppCompareProperty(materialEditor, "_EnableEmission", 1))
            {
                pv();
                ppDrawProperty(materialEditor, properties, "_MainEmiss", true);
                ppDrawProperty(materialEditor, properties, "_ColorEmiss");
                pve();
            }
            ps(5);
            ppDrawProperty(materialEditor, properties, "_IsMetallic");
            if (ppCompareProperty(materialEditor, "_IsMetallic", 1))
            {
                pv();
                ppDrawProperty(materialEditor, properties, "_MainMetallic", true);
                ppDrawProperty(materialEditor, properties, "_Metal");
                pve();
            }
            pv();
            ppDrawProperty(materialEditor, properties, "_Specular");
            pve();
            ps(5);

            materialEditor.TextureScaleOffsetProperty(FindProperty("_MainTex", properties));

            pve();

            ps(10);

            if (ppDrawProperty(materialEditor, properties, "_EnableFluidEffect"))
            {
                if (ppCompareProperty(materialEditor, "_EnableFluidEffect",1))
                {
                    pv();
                    ppDrawProperty(materialEditor, properties, "_FluidEffectSpeed");
                    ps(5);
                    bool fluid = ppDrawProperty(materialEditor, properties, "_ShowFluidDrops", false, "Enable refraction effect");
                    if(fluid)
                    {
                        if (ppCompareProperty(materialEditor, "_ShowFluidDrops", 1))
                        {
                            pv();
                            ppDrawProperty(materialEditor, properties, "_MainTexFluid", true);
                            ppDrawProperty(materialEditor, properties, "_Refraction");
                            pve();
                        }
                        ps(5);
                    }
                    ppDrawProperty(materialEditor, properties, "_MainNormalFluid", true);
                    ps(5);

                    pv();
                    ppDrawProperty(materialEditor, properties, "_FluidEffectScrollDir");
                    ppDrawProperty(materialEditor, properties, "_FluidEffectScrollDir2");
                    pve();

                    if(fluid) materialEditor.TextureScaleOffsetProperty(FindProperty("_MainTexFluid", properties));
                    ps();
                    pv();
                    ppDrawProperty(materialEditor, properties, "_EdgeBlending");
                    pve();
                    pve();
                }
                ps(10);
            }

            if (ppDrawProperty(materialEditor, properties, "_EnableWaveEffect"))
            {
                if (ppCompareProperty(materialEditor, "_EnableWaveEffect", 1))
                {
                    pv();
                    ppDrawProperty(materialEditor, properties, "_WaveSpeed");
                    ppDrawProperty(materialEditor, properties, "_WaveSize");
                    ppDrawProperty(materialEditor, properties, "_WaveLength");
                    ppDrawProperty(materialEditor, properties, "_WaveDirection");
                    pve();
                }
            }
            ps();

            pl("Track Settings");
            pv();
            pv();
            ppDrawProperty(materialEditor, properties, "_TrackFactor");
            pve();
            ps(5);

            pv();
            ppDrawProperty(materialEditor, properties, "_Tess");
            ppDrawProperty(materialEditor, properties, "_TessMin");
            ppDrawProperty(materialEditor, properties, "_TessMax");
            pve();

            ppDrawProperty(materialEditor, properties, "_DispTex");

            pv();
            ppDrawProperty(materialEditor, properties, "_UpdateNormals", false, "Update additional normals (takes more performance)");
            pve();

            ps();
            pve();
            ps(40);
        }
    }
}