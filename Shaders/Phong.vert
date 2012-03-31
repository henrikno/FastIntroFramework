uniform float time;
varying vec3 N, L;
void main(void)
{
    gl_Position = gl_ProjectionMatrix * gl_ModelViewMatrix * gl_Vertex;
	gl_TexCoord[0] = gl_TextureMatrix[0] * gl_MultiTexCoord0;
    //gl_Position.x += sin(time);

    // eye-space normal
    N = normalize(gl_NormalMatrix * gl_Normal);
    // eye-space light vector
    vec4 V = gl_ModelViewMatrix * gl_Vertex;
    L = gl_LightSource[0].position.xyz - V.xyz;
    gl_FrontColor = gl_Color;
}