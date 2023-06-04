#version 330

out vec4 colour;

in vec2 UVs;

uniform sampler2D albedo;

void main()
{
	colour = texture(albedo, UVs);
}