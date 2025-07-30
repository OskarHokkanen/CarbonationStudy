using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public List<ParticleSystem> pSystems;
    public List<Renderer> renderers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateSpeed(float range)
    {
        Debug.Log("Slider value: " + range + ", " + range);
        foreach (ParticleSystem system in pSystems)
        {
            var em = system.emission;
            em.rateOverTime = (float)range * 25f;
        }
    }
    
    public void UpdateSize(float range)
    {
        Debug.Log("Slider value: " + range + ", " + range);
        foreach (ParticleSystem system in pSystems)
        {
            var main = system.main;
            main.startSize = 5 * range;
        }
    }

    public void UpdateColorRed()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.red; 
        }
    }
    
    public void UpdateColorBlue()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.blue; 
        }
    }
    public void UpdateColorMagenta()
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = Color.magenta; 
        }
    }
    
}
