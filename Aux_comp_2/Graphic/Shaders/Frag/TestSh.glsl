﻿#version 460
uniform sampler2D srcTex;
in vec2 texCoord;
out vec4 color;
void main() {
	float c = texture(srcTex, texCoord).x;
	color = vec4(c, 1.0, 1.0, 1.0);
	//color.x=0.5;
}