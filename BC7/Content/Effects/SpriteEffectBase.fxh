float4x4 MatrixTransform;
Texture2D Texture : register(s0);
SamplerState Sampler : register(s0);

struct VertexIn
{
	float3 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TexCoord : TEXCOORD0;
};

struct VertexOut
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TexCoord : TEXCOORD0;
};

VertexOut VS(in VertexIn input)
{
	VertexOut output;

	output.Position = mul(float4(input.Position, 1), MatrixTransform);
	output.TexCoord = input.TexCoord;
	output.Color = input.Color;

	return output;
}
