#version 330 core
layout (location = 0) out vec4 gPosition;
layout (location = 1) out vec4 gNormal;
layout (location = 2) out vec4 gAlbedoSpec;

in vec2 TexCoords;
in vec3 FragPos;
in vec3 Normal;
in vec4 FragPosLightSpace;

uniform sampler2D shadowMap;

uniform vec3 lightPos;

float ShadowCalculation()
{
    // perform perspective divide
    vec3 projCoords = FragPosLightSpace.xyz / FragPosLightSpace.w;
    // transform to [0,1] range
    projCoords = projCoords * 0.5 + 0.5;
    // get closest depth value from light's perspective (using [0,1] range fragPosLight as coords)
    float closestDepth = texture(shadowMap, projCoords.xy).r; 
    // get depth of current fragment from light's perspective
    float currentDepth = projCoords.z;
    // calculate bias (based on depth map resolution and slope)
    vec3 lightDir = normalize(lightPos - FragPos);
    float bias = max(0.05 * (1.0 - dot(normalize(Normal), lightDir)), 0.005);
    // check whether current frag pos is in shadow
    //float shadow = currentDepth - bias > closestDepth  ? 1.0 : 0.0;
    // PCF
    float shadow = 0.0;
    vec2 texelSize = 1.0 / textureSize(shadowMap, 0);
    for(int x = -1; x <= 1; ++x)
    {
        for(int y = -1; y <= 1; ++y)
        {
            float pcfDepth = texture(shadowMap, projCoords.xy + vec2(x, y) * texelSize).r; 
            shadow += currentDepth - bias > pcfDepth  ? 1.0 : 0.0;        
        }    
    }
    shadow /= 9.0;
    // keep the shadow at 0.0 when outside the far_plane region of the light's frustum.
    if(projCoords.z > 1.0)
        shadow = 0.0;
        
    return shadow;
}

void main()
{    
    // store the fragment position vector in the first gbuffer texture
    gPosition.rgb = FragPos;
    // also store the per-fragment normals into the gbuffer
    gNormal.rgb = normalize(Normal);
    // store the shadow mask
    gPosition.a = ShadowCalculation();
    // store specular power in gNormal's alpha component
    gNormal.a = 64.0;
    // and the diffuse per-fragment color
    gAlbedoSpec.rgb = vec3(0.7,0.7,0.7);
    // store specular intensity in gAlbedoSpec's alpha component
    gAlbedoSpec.a = 4.0;
}