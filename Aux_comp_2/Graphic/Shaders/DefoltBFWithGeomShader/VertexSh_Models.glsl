#version 460 core

layout(location = 0) in vec3 _Position_model;
layout(location = 1) in vec3 _Normal_model;
layout(location = 4) in mat4 _ModelMatrix;
layout (rgba32f, binding = 0) uniform  image2D objdata;

out VS_GS_INTERFACE
{
vec3 Position_world;
vec3 Normal_world;
} vs_out;

void main() 
{
	vs_out.Position_world =  (_ModelMatrix*vec4(_Position_model,1)).xyz;
	vs_out.Normal_world = (_ModelMatrix*vec4(_Normal_model,0)).xyz;
	gl_Position = vec4(vs_out.Position_world,1);
}