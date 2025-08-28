using UnityEngine;

public struct LightDefaults
{
    public Color Colour { get; private set; }
    public float Intensity { get; private set; }
    public float Range { get; private set; }

    public LightDefaults(Light light)
    {
        Colour = light.color;
        Intensity = light.intensity;
        Range = light.range;
            
    }
    
}