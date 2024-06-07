Shader "Matej Vanco/Mesh Tracker/Mesh Tracker_Mobile" 
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

		_MainTex("Upper Albedo", 2D) = "white" {}
		_SecTex("Lower Albedo", 2D) = "white" {}
		[Normal]_MainNormal("Lower Normal", 2D) = "bump" {}
		[Normal]_SecNormal("Upper Normal", 2D) = "bump" {}

		[Toggle]_EnableEmission("Enable Emission", Float) = 0
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
		[Normal]_MainNormalFluid("Second Normal", 2D) = "bump" {}
		_MainTexFluid("Fluid Drops Albedo", 2D) = "white" {}

		_TrackFactor("Track Depth", Float) = -0.5
		_DispTex("Displacement Track Texture", 2D) = "gray" {}

		[Toggle]_UpdateNormals("Update Normals",Int) = 0
	}

	SubShader 
	{
		Tags { "RenderType"="Opaque" "Queue"="Geometry" }

		CGPROGRAM

		#pragma surface surf Standard addshadow vertex:vert
		#pragma target 3.0

		#include "MeshTracker_ExtSource.cginc"	
		
		ENDCG
	}
	CustomEditor "MeshTrackerEditor.MeshTrackSource_Editor"
	FallBack "Diffuse"
}