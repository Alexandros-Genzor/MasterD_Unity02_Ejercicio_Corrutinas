using System.Collections;
using System.Collections.Generic;
using EngineScripts;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightController : MonoBehaviour
{
    public GameObject lightContainer;
    public List<Light> lightsList;
    public int effectNumber;
    public float speed;
    public float delayTime = 0.1f;
    
    public bool lightsToggle = true;
    public bool effectsToggle;
    [SerializeField] private bool toggleStepped;
    
    private List<LightObject> _lights;
    
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
        _lights = new List<LightObject>();
        
        lightsList.AddRange(lightContainer.GetComponentsInChildren<Light>());

        foreach (var lightInst in lightsList)
        {
            _lights.Add(new LightObject(lightInst));
            
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
                StopAllCoroutines();
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

         foreach (var lightInst in _lights)
         {
             lightInst.Light.intensity = (lightsToggle ? lightInst.DefIntensity : 0);

         }
        
    }

    private void ResetLights()
    {
        lightsToggle = true;

        foreach (var lightInst in _lights)
        {
            lightInst.ResetLight();
            
        }
        
    }

    IEnumerator Coroutine()
    {
        while (effectsToggle)
        {
            switch (effectNumber)
            {
                case 1:
                    yield return StartCoroutine(Blink());
                    
                    break;
                
                case 2:
                    yield return StartCoroutine(Fade());

                    break;
                
                case 3:
                    yield return StartCoroutine(Party());

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

    private IEnumerator Blink()
    {
        ToggleLights();
        
        yield return new WaitForSeconds(delayTime);
        
    }

    private IEnumerator Fade()
    {
        foreach (var lightInst in _lights)
        {
            lightInst.Light.intensity = lightInst.DefIntensity * Mathf.Abs(Mathf.Sin(Time.time / speed));

        }
        
        // better with longer delayTime
        if (toggleStepped)
            yield return new WaitForSeconds(delayTime);
        
    }

    private IEnumerator Party()
    {
        // better with longer delayTime & some party music :D
        foreach (var lightInst in _lights)
        {
            lightInst.Light.color = Random.ColorHSV(0, 1, 1, 1, 1, 1, 1, 1);
                        
        }

        yield return new WaitForSeconds(delayTime);
        
    }

}
