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
                StartCoroutine(ICoroutine());

            else
            {
                StopCoroutine(ICoroutine());
                ResetLights();
                
            }
            
        }

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
        
        foreach (var lightInst in lightList)
        {
            lightInst.intensity = 2;

        }
        
    }

    IEnumerator ICoroutine()
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
                    

                    break;
                
                case 3:
                    

                    break;
                
            }

            yield return null;

        }

        yield return null;

    }
    
}
