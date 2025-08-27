using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public GameObject lights;
    public List<Light> lightList;
    public bool lightsToggle = true;
    public bool effectsToggle;
    public int effectSelect;
    public float speed;
    public float delayTime;

    public Color defaultColor;
    
    /*
     * Keys:
     *  - Q: Toggles lights on / off.
     *  - W: Controls lights coroutine.
     *  - 1, 2, 3: Sets the lights behaviours.
     */
    
    #region LIFECYCLE FUNCTIONS
    // Start is called before the first frame update
    void Start()
    {
        lightList.AddRange(lights.GetComponentsInChildren<Light>());
        
        Debug.Log(lightList[17].ToString().Contains("Spot Light"));
        
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
            effectSelect = 1;
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            effectSelect = 2;
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            effectSelect = 3;

    }
    
    #endregion

    private void ToggleLights()
    {
         lightsToggle = !lightsToggle;

         foreach (var lightInst in lightList)
         {
             lightInst.intensity = (lightsToggle ? 2 : 0);

         }
        
    }

    private void ResetLights()
    {
        lightsToggle = true;
        
        foreach (var light in lightList)
        {
            light.intensity = 2;
            light.color = defaultColor;

        }
        
    }

    IEnumerator Coroutine()
    {
        while (effectsToggle)
        {
            switch (effectSelect)
            {
                case 1:
                    ToggleLights();
                    yield return new WaitForSeconds(delayTime);

                    break;
                
                case 2:
                    foreach (var light in lightList)
                    {
                        light.intensity = 2;
                        light.color = Color.white;
                        light.range = 4;

                        yield return new WaitForSeconds(delayTime);

                    }

                    break;
                
                case 3:
                    // pairs well with caramelldansen
                    foreach (var light in lightList)
                    {
                        Color randColor = Random.ColorHSV();
                        
                        light.color = randColor;
                        
                    }

                    yield return new WaitForSeconds(delayTime);

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
