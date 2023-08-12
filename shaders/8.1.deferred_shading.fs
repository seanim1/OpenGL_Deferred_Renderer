#version 330 core
out vec4 FragColor;

in vec2 TexCoords;

uniform sampler2D gPosition;
uniform sampler2D gNormal;
uniform sampler2D gAlbedoSpec;

const int NR_POINT_LIGHTS = 780;
layout (std140) uniform GlobalUBO
{
    vec3 pointLightPositions[NR_POINT_LIGHTS];
    vec3 pointLightColors[NR_POINT_LIGHTS];
};
uniform vec3 viewPos;
uniform vec3 dirLightPos;

void main()
{             
    // retrieve data from gbuffer
    vec3 FragPos = texture(gPosition, TexCoords).rgb;
    float ShadowMask = texture(gPosition, TexCoords).a;
    vec3 Normal = texture(gNormal, TexCoords).rgb;
    float SpecularPower = 64.0; //texture(gNormal, TexCoords).a;
    vec3 Diffuse = texture(gAlbedoSpec, TexCoords).rgb;
    float SpecularStrength = texture(gAlbedoSpec, TexCoords).a;
    
    vec3 lighting  = Diffuse * 0.1; // hard-coded ambient component
    
    // phase 1: directional lighting (based on 2.6 Multiple Lights)
    vec3 dirLightCol = vec3(0.55, 0.55, 0.85);
    vec3 lightDir = normalize(dirLightPos - FragPos);
    // diffuse shading
    vec3 diffuse = max(dot(Normal, lightDir), 0.0) * dirLightCol;
    // specular shading
    vec3 viewDir  = normalize(viewPos - FragPos);
    vec3 reflectDir = reflect(-lightDir, Normal);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), SpecularPower);
    vec3 specular = dirLightCol * spec * SpecularStrength;
    // calculate shadow, only the directional light will cast shadow onto the plane.
    lighting += (1-ShadowMask) * (diffuse + specular);

    // phase 2: point lights
    for(int i = 0; i < NR_POINT_LIGHTS; i++)
    {
        // diffuse
        vec3 lightDir = normalize(pointLightPositions[i] - FragPos);
        vec3 diffuse = max(dot(Normal, lightDir), 0.0) * Diffuse * pointLightColors[i];
        // specular
        vec3 halfwayDir = normalize(lightDir + viewDir);  
        float spec = pow(max(dot(Normal, halfwayDir), 0.0), SpecularPower);
        vec3 specular = pointLightColors[i] * spec * SpecularStrength;
        // attenuation
        float distance = length(pointLightPositions[i] - FragPos);
        float attenuation = 1.0 / (1.0 + 0.7 * distance + 1.8 * distance * distance);
        diffuse *= attenuation;
        specular *= attenuation;
        lighting += diffuse + specular;
    }
    // gamma correction
    //lighting = pow(lighting, vec3(1.0/2.2));
    // hdr tonemapping
    lighting = vec3(1.0) - exp(-lighting * 1.3);
    FragColor = vec4(lighting, 1.0);
}

