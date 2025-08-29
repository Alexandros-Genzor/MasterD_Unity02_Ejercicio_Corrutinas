using UnityEditor.Search;
using UnityEngine;

namespace EngineScripts
{
    /// <summary>
    /// Contains an instance of a light source and its relevant editor-defined values as default values.
    /// </summary>
    
    public struct LightObject
    {
        public Light Light;
        // public LightDefaults Defaults;
        
        public Color DefColor { get; private set; }
        public float DefIntensity { get; private set; }
        public float DefRange { get; private set; }

        public LightObject(Light light)
        {
            Light = light;
            // Defaults = new LightDefaults(Light);

            DefColor = light.color;
            DefIntensity = light.intensity;
            DefRange = light.range;

        }

        public void ResetLight()
        {
            // Light.color = Defaults.Colour;
            // Light.intensity = Defaults.Intensity;
            // Light.range = Defaults.Range;
            Light.color = DefColor;
            Light.intensity = DefIntensity;
            Light.range = DefRange;

        }

    }
    
}