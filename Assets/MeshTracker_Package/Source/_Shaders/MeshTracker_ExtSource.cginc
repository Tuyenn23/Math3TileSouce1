//---Official Mesh Tracker shader source written by Matej Vanco 2018
//---Updated December 2020

sampler2D _MainTex, _SecTex;
sampler2D _MainNormal, _SecNormal;
sampler2D _MainEmiss;
sampler2D _MainMetallic;
sampler2D _MainTexFluid;
sampler2D _MainNormalFluid;

float _IsMetallic;
float _Metal;
float _EnableEmission;
float _ShowFluidDrops;

half4 _ColorUp;
half4 _ColorDown;
half _ColorDown_Power;
half _ColorUp_Power;
half4 _ColorEmiss;
half _ColorAlpha;
half _InterpolMulti;

float _Specular;
float _NormalAmount;

float _EnableFluidEffect;
float _FluidEffectSpeed;
float _Refraction;
float4 _FluidEffectScrollDir;
float4 _FluidEffectScrollDir2;

float _EdgeBlending;
int _HIsAlpha;

float _EnableWaveEffect;
float _WaveSpeed;
float _WaveSize;
float _WaveLength;
float4 _WaveDirection;

int _UpdateNormals;

float4 _LocalPos;

float _TrackFactor;

struct appdata
{
	float4 vertex : POSITION;
	float4 tangent : TANGENT;
	float3 normal : NORMAL;
	float2 texcoord : TEXCOORD0;

	float2 texcoord1 : TEXCOORD1;
	float2 texcoord2 : TEXCOORD2;
};

struct v2f
{
	float4 vertex : POSITION;
	float2 texcoord : TEXCOORD0;
};

sampler2D _DispTex;
float _Displacement;

//Alternative normal calculation
float3 calcNormal(float2 texcoord)
{
	float3 off = float3(-0.001f, 0, 0.001f);
	float2 size = float2(0.01f, 0.0);

	float a = tex2Dlod(_DispTex, float4(texcoord.xy - off.xy, 0, 0)).x * _TrackFactor;

	float b = tex2Dlod(_DispTex, float4(texcoord.xy - off.zy, 0, 0)).x * _TrackFactor;

	float c = tex2Dlod(_DispTex, float4(texcoord.xy - off.yx, 0, 0)).x * _TrackFactor;

	float d = tex2Dlod(_DispTex, float4(texcoord.xy - off.yz, 0, 0)).x * _TrackFactor;

	float3 v1 = normalize(float3(size.xy, b - a));
	float3 v2 = normalize(float3(size.yx, d - c));

	return normalize(cross(v1, v2));
}

//Vertex shader processor
void vert(inout appdata v)
{
	if (_EnableWaveEffect == 1)
	{
		float k = 2 * UNITY_PI / _WaveLength;
		float c = sqrt(9.8 / k);
		float2 d2 = normalize(_WaveDirection);
		float f = k * (dot(d2, v.vertex.xz) - c * _Time.y * _WaveSpeed);

		v.vertex.x += d2.x * (_WaveSize * cos(f));
		v.vertex.y = _WaveSize * sin(f);
		v.vertex.z += d2.y * (_WaveSize * cos(f));
	}
	
	float d = tex2Dlod(_DispTex, float4(v.texcoord.xy, 0, 0)).r * _TrackFactor;
	v.vertex.xyz -= v.normal.xyz * d;

	if (_UpdateNormals == 1) v.normal.xyz = normalize(calcNormal(v.texcoord) + v.normal.xyz);
}

float4 _GrabTexture_TexelSize;
sampler2D _GrabTexture;
sampler2D _CameraDepthTexture;
float4 refrColor;
float3 n2;

//General input
struct Input
{
	float2 uv_MainTex;
	float2 uv_DispTex;
	float2 uv_MainTexFluid;
	float3 color;
	float4 screenPos;
	float3 worldPos;
};

//Metallic calculation
void SetMetallic(Input IN, inout SurfaceOutputStandard o)
{
	fixed4 metallic = tex2D(_MainMetallic, IN.uv_MainTex);
	o.Metallic = metallic.r * _Metal;
	o.Smoothness = metallic.a * _Specular;
}

//Surface shader calculation
void surf(Input IN, inout SurfaceOutputStandard o)
{
	float val = saturate(lerp(0, tex2Dlod(_DispTex, float4(IN.uv_DispTex, 0, 0)).r , _InterpolMulti));
	if (_EnableFluidEffect == 1) IN.uv_MainTex.xy += _FluidEffectScrollDir.xy * _Time.x * _FluidEffectSpeed;

	float3 mainTex = tex2D(_MainTex, IN.uv_MainTex).rgb;
	float3 secTex = tex2D(_SecTex, IN.uv_MainTex).rgb;

	float l = clamp(val, 0, 1);
	float3 c = lerp(secTex * _ColorUp.rgb + _ColorUp_Power, mainTex * _ColorDown.rgb + _ColorDown_Power, val);
	float3 n = lerp(UnpackNormal(tex2D(_MainNormal, IN.uv_MainTex)), UnpackNormal(tex2D(_SecNormal, IN.uv_MainTex)), l);
	n.z = n.z / _NormalAmount;

	if (_EnableFluidEffect == 1)
	{
		float eBlend = _EdgeBlending;
		if (eBlend >= 1)
		{
			half depth = UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos)));
			depth = LinearEyeDepth(depth);
			eBlend = 22 - _EdgeBlending;
			eBlend = saturate(eBlend * (depth - IN.screenPos.w));
		}
		else eBlend = 1;

		float2 fluidUV = IN.uv_MainTex;
		fluidUV.xy += _FluidEffectScrollDir2.xy * _Time.x * _FluidEffectSpeed;

		float3 distort = tex2D(_MainTexFluid, IN.uv_MainTexFluid) * IN.color.rgb;
		float2 offset = distort * _Refraction * _GrabTexture_TexelSize.xy;
		IN.screenPos.xy = offset * IN.screenPos.z + IN.screenPos.xy;
		if (IN.screenPos.x != 0 && IN.screenPos.y != 0 && IN.screenPos.z != 0 && IN.screenPos.w != 0)
			refrColor = tex2Dproj(_GrabTexture, IN.screenPos);
		
		n2 = UnpackNormal(tex2D(_MainNormalFluid, fluidUV));
		n2.z = n2.z / _NormalAmount;
		o.Alpha = eBlend *_ColorAlpha;
		o.Normal = normalize(n) + normalize(n2);
		if (_ShowFluidDrops == 1)
			c *= refrColor.rgb;
	}
	else
	{
		o.Normal = normalize(n);
		o.Alpha = _ColorAlpha;
	}

	if (_HIsAlpha)
		o.Alpha *= clamp(val, 0, 1);

	o.Albedo = saturate(c.rgb);

	o.Smoothness = _Specular;
	if(_EnableEmission == 1)
		o.Emission = tex2D(_MainEmiss, IN.uv_MainTex) * _ColorEmiss;

	if(_IsMetallic == 1)
		SetMetallic(IN, o);
}