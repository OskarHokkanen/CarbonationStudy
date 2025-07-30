using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class ExperimentManager : MonoBehaviour
{
    // Set the order of the scenes
    [Tooltip("Excluding the starter scene")]
    public static string sceneOrder = "BCDEFGHIX";
    public static int participantNumber = 1;
    private int currentSceneNumber;
    public Slider slider1;
    public Slider slider2;
    public Text valueText1;
    public Text valueText2;
    private string perceivedCarbonation;
    private string confidence;
    private char currentSceneLetter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
         
    }
    void Start()
    {
        // Listen to slider value changes
        slider1.onValueChanged.AddListener(OnSliderValueChanged);
        slider2.onValueChanged.AddListener(OnSlider2ValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        perceivedCarbonation = value.ToString();
        valueText1.text = perceivedCarbonation;
    }

    void OnSlider2ValueChanged(float value)
    {
        confidence = value.ToString();
        valueText2.text = confidence;
    }

    // Called in the begining and after each time participant has answered survey,
    public void LogAnswersAndLoadNextScene()
    {
        DataLoggingManager logger = FindObjectOfType<DataLoggingManager>();
        if (logger != null)
            logger.LogAnswer(participantNumber, currentSceneLetter, perceivedCarbonation, confidence );
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneOrder))
        {
            char sceneLetter = sceneOrder[0];
            Debug.Log(sceneLetter);
            int sceneNumber = GetSceneNumberFromLetter(sceneLetter);
            sceneOrder = sceneOrder.Substring(1);
            Debug.Log(sceneOrder);
            
            currentSceneNumber = sceneNumber;
            Debug.Log(sceneNumber);
            SceneManager.LoadScene(sceneNumber);
            
        }
        else 
        {
            // Load endscene when done
            SceneManager.LoadScene(9);
        }
        
        // Remove current scene from list
        // Load the next scene
    }

    private int GetSceneNumberFromLetter(char letter)
    {
        switch (letter)
        {
            case 'A':
                return 0;
            case 'B':
                return 1;
            case 'C':
                return 2;
            case 'D':
                return 3;
            case 'E':
                return 4;
            case 'F':
                return 5;
            case 'G':
                return 6;
            case 'H':
                return 7;
            case 'I':
                return 8;
        }
        return 9;
    }

    
    public void UpdateText(int value)
    {
        valueText1.text = value.ToString();
    }
    
    
}
