#version 460 core
uniform vec3 LightPosition_world;
uniform vec3 MaterialDiffuse;
uniform vec3 MaterialAmbient;
uniform vec3 MaterialSpecular;
uniform float lightPower;
uniform sampler2D textureSample;
uniform int textureVis;

in GS_FS_INTERFACE
{
vec3 Position_world;
vec3 Normal_camera;
vec3 Normal_world;
vec3 EyeDirection_camera;
vec3 LightDirection_camera;
vec3 LightDirection_world;
}fs_in;
out vec4 color;
void main() {
	vec3 LightColor = vec3(1.0, 1.0, 1.0);
	float LightPower = lightPower;
	vec3 MaterialAmbientColor = MaterialAmbient;
	vec3 MaterialSpecularColor = MaterialSpecular;
	float distance = length( LightPosition_world - fs_in.Position_world );
	vec3 n = normalize( fs_in.Normal_world );
	vec3 l = normalize( fs_in.LightDirection_world );
	float cosTheta = clamp( dot( n,l ), 0,1 );

	vec3 lc = normalize( fs_in.LightDirection_camera );
	vec3 nc = normalize( fs_in.Normal_camera);
	vec3 E = normalize(fs_in.EyeDirection_camera);
	vec3 R = reflect(-lc,nc);

	float cosAlpha = clamp( dot( E,R ) , 0,1 );
	

	   vec3 MaterialDiffuseColor = vec3(0.5);
		MaterialAmbientColor = vec3(0.05);
		MaterialSpecularColor = vec3(0.01);
	    //color.xyz = MaterialDiffuseColor * LightColor * LightPower * cosTheta / (distance*distance);
	   color.xyz = MaterialAmbientColor + MaterialDiffuseColor * LightColor * LightPower * cosTheta / (distance*distance) +MaterialSpecularColor * LightColor * LightPower * pow(cosAlpha,5) / (distance*distance);
	   
	   
	   //color.xyz = vec3(0.5,0,0.5);
		color.w = 1.0;
	
	
	
}