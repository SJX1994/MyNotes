//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia

#ifndef INCLUDE_GUARD_BATTLE_EFFECT_SHADER_COMMON
#define INCLUDE_GUARD_BATTLE_EFFECT_SHADER_COMMON

//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
float4	_battleEffectParam;

sampler2D _battleEffectParamTexture;		//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
float4 _battleEffectParamTextureParams;

sampler2D _teamColorTexture;		//Adia

float4 _heartbeatColor;				//Adia

//Adia


//Adia
//Adia
//Adia
//Adia

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
inline float4 createBattleEffectParam(float4 clipPos)
{
	float depth = clipPos.z / clipPos.w;
	return float4(_battleEffectParam.x, 0.0f, 0.0f, depth);
}

//Adia
//Adia
//Adia
//Adia
inline float2 getBattleEffectParamTextureUVOffset()
{
	return _battleEffectParamTextureParams.xy;
}

//Adia
//Adia
//Adia
//Adia
inline float calcBattleEffectParamPriority(float4 param)
{
	//Adia
	return param.a * 10 + param.g;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
inline float4 getOutlineParamFromTex(float4 texValue)
{
	float4 result = float4(texValue.r, texValue.a, 0.0f, texValue.a > 0.0f ? 1.0f : 0.0f);
	return result;
}

//Adia
//Adia
//Adia
//Adia
//Adia
//Adia
inline float4 collectOutlineParam(float4 accum, float4 texValue)
{
	float4 collectedTexValue = getOutlineParamFromTex(texValue);
	float accumPriority = calcBattleEffectParamPriority(accum);
	float texValuePriority = calcBattleEffectParamPriority(collectedTexValue);
	float4 result = accumPriority > texValuePriority ? accum : collectedTexValue;
	result.a = accum.a + collectedTexValue.a;
	return result;
}

//Adia
//Adia
//Adia
//Adia
//Adia
inline float4 getOutlineColorFromTex(float4 battleEffectParam)
{
	return tex2D(_teamColorTexture, float2(battleEffectParam.r, 0.5f));
}

//Adia
//Adia
//Adia
//Adia
//Adia
inline float2 clipPosToScreenUv(float4 clipPos)
{
	float2 uv = clipPos.xy / clipPos.w;
	return uv * float2(0.5f, -0.5f) + 0.5f;
}

//Adia

#endif //Adia
//Adia
//Adia
//Adia
