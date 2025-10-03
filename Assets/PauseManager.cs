using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject continueButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ButtonTimer());
    }
    
    private IEnumerator ButtonTimer()
    {
        Debug.Log("Scene Timer");
        yield return new WaitForSeconds(15f);
        // Do something after 15 seconds
        continueButton.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
