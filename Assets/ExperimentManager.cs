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
    public static string sceneOrder = "HIDECBAFGX";
    public static int participantNumber = 5;
    private static int currentSceneNumber;
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    public Text valueText1;
    public Text valueText2;
    public Text valueText3;
    public Text valueText4;
    private static string questionOneScore = "50";
    private static string questionTwoScore = "50";
    private static string questionThreeScore = "50";
    private static string questionFourScore = "50";
    private static char currentSceneLetter;
    
    private static bool firstSceneLoaded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
         
    }
    void Start()
    {   if (!firstSceneLoaded)
        {
            firstSceneLoaded = true;
            // Start loading scenes
            LoadNextScene();
        }
        // Listen to slider value changes
        slider1.onValueChanged.AddListener(OnSliderValueChanged);
        slider2.onValueChanged.AddListener(OnSlider2ValueChanged);
        slider3.onValueChanged.AddListener(OnSlider3ValueChanged);
        slider4.onValueChanged.AddListener(OnSlider4ValueChanged);
    }

    void OnSliderValueChanged(float value)
    {
        questionOneScore = value.ToString();
        Debug.Log($"Question One: {questionOneScore}");
        valueText1.text = questionOneScore;
    }

    void OnSlider2ValueChanged(float value)
    {
        questionTwoScore = value.ToString();
        Debug.Log($"Question Two: {questionTwoScore}");
        valueText2.text = questionTwoScore;
    }
    
    void OnSlider3ValueChanged(float value)
    {
        questionThreeScore = value.ToString();
        Debug.Log($"Question Three: {questionThreeScore}");
        valueText3.text = questionThreeScore;
    }
    
    void OnSlider4ValueChanged(float value)
    {
        questionFourScore = value.ToString();
        Debug.Log($"Question Four: {questionFourScore}");
        valueText4.text = questionFourScore;
    }

    // Called in the begining and after each time participant has answered survey,
    public void LogAnswersAndLoadNextScene()
    {
        DataLoggingManager logger = FindFirstObjectByType<DataLoggingManager>();
        Debug.Log($"{participantNumber}, {currentSceneLetter}, {questionOneScore}, {questionTwoScore}, {questionThreeScore}, {questionFourScore}");
        if (logger != null)
            logger.LogAnswer(participantNumber, currentSceneLetter, questionOneScore, questionTwoScore, questionThreeScore, questionFourScore );
        LoadNextScene();
    }
    public void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(sceneOrder))
        {
            char sceneLetter = sceneOrder[0];
            int sceneNumber = GetSceneNumberFromLetter(sceneLetter);
            sceneOrder = sceneOrder.Substring(1);
            currentSceneLetter = sceneLetter;
            currentSceneNumber = sceneNumber;
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
    
}
