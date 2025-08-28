using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightController : MonoBehaviour
{
    public GameObject lightContainer;
    public List<Light> lightsList;
    public int effectNumber;
    public float speed;
    public float delayTime;
    
    public bool lightsToggle = true;
    public bool effectsToggle;
    
    private List<LightDefaults> _lightsDefaultsList; 

    // [Header("-- DEFAULTS --")]
    // [SerializeField] private Color defaultColor;
    // [SerializeField] private float defaultIntensity;
    // [SerializeField] private float defaultRange;
    
    /*
     * Keys:
     *  - Q: Toggles lights On / Off.
     *  - W: Controls lights coroutine.
     *  - 1, 2, 3: Sets the lights behaviours.
     */
    
    #region LIFECYCLE FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {
        _lightsDefaultsList = new List<LightDefaults>();
        
        lightsList.AddRange(lightContainer.GetComponentsInChildren<Light>());

        foreach (Light lightInst in lightsList)
        {
            _lightsDefaultsList.Add(new LightDefaults(lightInst));
            
        }
        
        Debug.Log(lightsList[17].ToString().Contains("Spot Light"));
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ToggleLights();

        if (Input.GetKeyDown(KeyCode.W))
        {
            effectsToggle = !effectsToggle;
            
            if (effectsToggle)
                StartCoroutine(Coroutine());
            else
            {
                StopCoroutine(Coroutine());
                ResetLights();
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            effectNumber = 1;
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            effectNumber = 2;
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            effectNumber = 3;

    }
    
    #endregion

    private void ToggleLights()
    {
         lightsToggle = !lightsToggle;

         /*foreach (var lightInst in lightsList)
         {
             lightInst.intensity = (lightsToggle ? defaultIntensity : 0);

         }*/

         for (int i = 0; i < lightsList.Count; i++)
         {
             lightsList[i].intensity = (lightsToggle ? _lightsDefaultsList[i].Intensity : 0);
             
         }
        
    }

    private void ResetLights()
    {
        lightsToggle = true;
        
        /*foreach (var lightInst in lightsList)
        {
            lightInst.intensity = defaultIntensity;
            lightInst.color = defaultColor;
            lightInst.range = defaultRange;

        }*/

        for (int i = 0; i < lightsList.Count; i++)
        {
            lightsList[i].intensity = _lightsDefaultsList[i].Intensity;
            lightsList[i].range = _lightsDefaultsList[i].Range;
            lightsList[i].color = _lightsDefaultsList[i].Colour;

        }
        
    }

    IEnumerator Coroutine()
    {
        while (effectsToggle)
        {
            switch (effectNumber)
            {
                case 1:
                    ToggleLights();
                    yield return new WaitForSeconds(delayTime);

                    break;
                
                case 2:
                    /*foreach (var light in lightsList)
                    {
                        light.intensity = 1;
                        light.color = Color.white;
                        light.range = 4;

                        yield return new WaitForSeconds(delayTime);

                        if (!effectsToggle)
                            break;

                    }*/

                    /*foreach (var lightInst in lightsList)
                    {
                        lightInst.intensity = defaultIntensity * Mathf.Abs(Mathf.Sin(Time.time / speed));

                    }*/

                    for (int i = 0; i < lightsList.Count; i++)
                    {
                        lightsList[i].intensity = _lightsDefaultsList[i].Intensity * 
                                                  Mathf.Abs(Mathf.Sin(Time.time / speed));
                        
                    }

                    break;
                
                case 3:
                    // better with longer delayTime
                    foreach (var lightInst in lightsList)
                    {
                        lightInst.color = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
                        
                    }

                    yield return new WaitForSeconds(delayTime);

                    break;
                
                default:
                    Debug.Log("No effect selected.");
                    effectsToggle = false;

                    break;
                
            }

            yield return null;

        }

        yield return null;

    }

}

/*Light[] flickers;
                    
if (lightList[lightElement].ToString().Contains("Spot Light"))
    flickers = lightList[lightElement].transform.parent.GetComponentsInChildren<Light>();
else
    flickers = lightList[lightElement].GetComponentsInChildren<Light>();

for (var i = 0; i < 6; i++)
{
    foreach (var flicker in flickers)
    {
        flicker.intensity = (i % 2 == 0 ? 1 : 2);

    }
                        
    yield return new WaitForSeconds(delayTime);
                        
}*/
