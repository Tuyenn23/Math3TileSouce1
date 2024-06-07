Shader "Matej Vanco/Mesh Tracker/Mesh Tracker_Transparent" 
{
	//---Shader written by Matej Vanco 2018
	//---Updated December 2020
	Properties 
	{
		_ColorUp("Upper Color", Color) = (1,1,1,1)
	    _ColorDown("Lower Color", Color) = (.9,.9,.9,1)
		_ColorDown_Power("Upper Color Multiplier",Range(-2,2)) = 0
		_ColorUp_Power("Lower Color Multiplier",Range(-2,2)) = 0
		_InterpolMulti("Interpolation Multiplier",Range(-8.0,8.0)) = 1.5
		_ColorAlpha("Alpha",Range(0,1)) = 1
		[Toggle]_HIsAlpha("Height Is Alpha",Int) = 0

		_MainTex("Upper Albedo", 2D) = "white" {}
		_SecTex("Lower Albedo", 2D) = "white" {}
		[Normal]_MainNormal("Lower Normal", 2D) = "bump" {}
		[Normal]_SecNormal("Upper Normal", 2D) = "bump" {}

		[Toggle]_EnableEmission("Enable Emission",Float) = 0
		_MainEmiss("Emission", 2D) = "white" {}
		[HDR]_ColorEmiss("Emission Color", Color) = (0,0,0,0)

		[Toggle]_IsMetallic("Enable Metallic Field",Float) = 1
		_MainMetallic("Metallic Texture", 2D) = "white" {}
		_Metal("Metallic Power", Range(0,1)) = 1
		_NormalAmount("Normal Power", Range(0.01,2)) = 0.5
		_Specular("Specular Power", Range(0,1)) = 1

		[Toggle]_EnableFluidEffect("Enable Fluid Effect",Float) = 0
		_FluidEffectSpeed("Fluid Effect Speed",Range(0,20)) = 1
		_FluidEffectScrollDir("Fluid Direction 1",Vector) = (1,0,0,0)
		_FluidEffectScrollDir2("Fluid Direction 2",Vector) = (-1,0,0,0)
		[Toggle]_ShowFluidDrops("Show Fluid Drops",Float) = 0
		_MainTexFluid("Fluid Drops Albedo", 2D) = "white" {}
		[Normal]_MainNormalFluid("Second Normal", 2D) = "bump" {}
		_Refraction("Refraction", Range(0.00, 1000.0)) = 1.0

		_EdgeBlending("Edge Blending", Range(0,20)) = 4

		[Toggle]_EnableWaveEffect("Enable Wave Effect",Float) = 0
		_WaveSpeed("Wave Speed", Range(0,5)) = 0.2
		_WaveSize("Wave Size",Range(0,1)) = 0.15
		_WaveLength("Wave Length",Range(0.1,10)) = 4
		_WaveDirection("Wave Direction",Vector) = (1,0,0,0)

		_TrackFactor("Track Depth", Float) = -0.5

		_Tess("Smooth Intensity [Tessellation]", Range(1,64)) = 4
		_TessMin("Minimum Detail Distance", Float) = 20
		_TessMax("Maximum Detail Distance", Float) = 120
		_DispTex("Displacement Track Texture", 2D) = "gray" {}

		[Toggle]_UpdateNormals("Update Normals",Float) = 0
	}

	SubShader 
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "ForceNoShadowCasting" = "True" }
		Blend SrcAlpha OneMinusSrcAlpha

		GrabPass
		{}

		CGPROGRAM

		#pragma surface surf Standard vertex:vert tessellate:tessDistance alpha:fade
		#pragma target 5.0

		#include "Tessellation.cginc"
		#include "MeshTracker_ExtSource.cginc"	
		#include "MeshTracker_ExtSourceTess.cginc"	
		
		ENDCG
	}
	CustomEditor "MeshTrackerEditor.MeshTrackSource_Editor"
	FallBack "Diffuse"
}