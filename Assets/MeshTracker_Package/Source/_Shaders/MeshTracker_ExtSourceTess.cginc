//Tessalation addition for non-mobile targets
float _Tess;
float _TessMin;
float _TessMax;
float4 tessDistance(appdata v0, appdata v1, appdata v2)
{
	return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, _TessMin, _TessMax, _Tess);
}