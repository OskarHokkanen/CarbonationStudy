using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.Management;
using Slider = UnityEngine.UI.Slider;



public class ExperimentManager : MonoBehaviour
{
    // Set the order of the scenes
    [Tooltip("Excluding the starter scene")]
    public static string sceneOrder = "SCQSDQSBQPSEQSAQSFQPSIQSGQSHQX";
    public static int participantNumber = 21;
    private static int currentSceneNumber;
    public Slider slider1;
    public Slider slider2;
    public Slider slider3;
    public Slider slider4;
    public Slider slider5;
    public Text valueText1;
    public Text valueText2;
    public Text valueText3;
    public Text valueText4;
    public Text valueText5;
    private static string questionOneScore = "50";
    private static string questionTwoScore = "50";
    private static string questionThreeScore = "50";
    private static string questionFourScore = "50";
    private static string questionFiveScore = "50";
    private static char currentSceneLetter;
    
    private static bool firstSceneLoaded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Update()
    {
         
    }
    void Start()
    {   
        
        StartCoroutine(StartXR());
        
        if (!firstSceneLoaded)
        {
            firstSceneLoaded = true;
            // Start loading scenes
            LoadNextScene();
        }

        questionOneScore = "50";
        questionTwoScore = "50";
        questionThreeScore = "50";
        questionFourScore = "50";
        questionFiveScore = "50";
        
        if (slider1 != null && slider5 != null)
        {
            // Listen to slider value changes
            slider1.onValueChanged.AddListener(OnSliderValueChanged);
            slider2.onValueChanged.AddListener(OnSlider2ValueChanged);
            slider3.onValueChanged.AddListener(OnSlider3ValueChanged);
            slider4.onValueChanged.AddListener(OnSlider4ValueChanged);    
            slider5.onValueChanged.AddListener(OnSlider5ValueChanged);
        }

        Debug.Log(currentSceneNumber);
        Debug.Log(currentSceneLetter);
        if (currentSceneLetter != 'P' && currentSceneNumber != 10  && currentSceneLetter != 'S')
        {
            StartCoroutine(SceneTimer());
            Debug.Log("After Scene Timer");    
            
        } 
    }
    
    IEnumerator StartXR()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoader();
        yield return null;
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }
    
    private IEnumerator SceneTimer()
    {
        Debug.Log("Scene Timer");
        yield return new WaitForSeconds(20f);
        // Do something after 20 seconds
        LoadNextScene();
        // Example: automatically go to next scene
        // LogAnswersAndLoadNextScene();
    }

    public void NextSceneButtonPressed()
    {
        if (currentSceneLetter != 'S')
        {
            return;
        }

        LoadNextScene();
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
    
    void OnSlider5ValueChanged(float value)
    {
        questionFiveScore = value.ToString();
        Debug.Log($"Question Five: {questionFiveScore}");
        valueText5.text = questionFiveScore;
    }

    // Called in the begining and after each time participant has answered survey,
    public void LogAnswersAndLoadNextScene()
    {
        DataLoggingManager logger = FindFirstObjectByType<DataLoggingManager>();
        Debug.Log($"{participantNumber}, {currentSceneLetter}, {questionOneScore}, {questionTwoScore}, {questionThreeScore}, {questionFourScore}, {questionFiveScore}");
        if (logger != null)
            logger.LogAnswer(participantNumber, currentSceneLetter, questionOneScore, questionTwoScore, questionThreeScore, questionFourScore, questionFiveScore );
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
            if (sceneLetter != 'Q')
            {
                currentSceneLetter = sceneLetter;
            }
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
            case 'S': // Start
                return 0;
            case 'A':
                return 1;
            case 'B':
                return 2;
            case 'C':
                return 3;
            case 'D':
                return 4;
            case 'E':
                return 5;
            case 'F':
                return 6;
            case 'G':
                return 7;
            case 'H':
                return 8;
            case 'I':
                return 9;
            case 'Q':
                return 10; // Questionnaire Scene
            case 'P':
                return 11; // Pause scene
        }
        return 12; // End scene
    }
    
}
