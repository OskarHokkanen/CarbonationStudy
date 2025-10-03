using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SwitchScene : MonoBehaviour
{
    public GameObject menuUI;
    public float holdThreshold = 3.0f;

    private InputDevice leftHand;
    private InputDevice rightHand;

    private float holdTimer = 0f;
    private bool menuVisible = false;
    private bool toggledDuringHold = false;
    private string QUESTION_ONE_TEXT = "How fizzy does this environment feel?";
    private string QUESTION_TWO_TEXT = "How confident are you in that rating?";
    private string QUESTION_THREE_TEXT = "How sharp or tingly did the mouth sensation feel?";
    private string QUESTION_FOUR_TEXT = "How confident are you in that rating?";
    private string QUESTION_FIVE_TEXT = "How well did the environment match the sensation in your mouth?";
    private Transform QuestionSetOne;
    private Transform QuestionSetTwo;
    private Transform QuestionSetThree;
    
    
    void Start()
    {
        if (menuUI == null)
            return;
        
        TryInitializeDevices();
        Transform textTransform = menuUI.transform.Find("Interactive Controls/QuestionSetOne/Question text");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_ONE_TEXT;
            }
        }
        textTransform = menuUI.transform.Find("Interactive Controls/QuestionSetOne/Question text2");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_TWO_TEXT;
            }
        }
        textTransform = menuUI.transform.Find("Interactive Controls/QuestionSetTwo/Question text3");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_THREE_TEXT;
            }
            //textTransform.SetSiblingIndex(1);
        }
        textTransform = menuUI.transform.Find("Interactive Controls/QuestionSetTwo/Question text4");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_FOUR_TEXT;
            }
        }
        textTransform = menuUI.transform.Find("Interactive Controls/QuestionSetThree/Question text5");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_FIVE_TEXT;
            }
        }
        
        
        
    }

    private void Awake()
    { 
        Debug.LogError("HEEERREEEE");
        RandomizeQuestionOrder();
    }
    /*
 * Be part one out of 2 or three.
 * Can we change the level of carbonation based on visual and audio.
 * Can different levels of carbonation change what people see and hear?
 * Based on these two we can create the system.
 *
 * This would be no this CHI but the next one. 
 * We should get the participants and do the test.
 *
 * Thursday: Studies
 * Create Doodle (Create for both)
 * Oskar:
 *  Finish the VR
 *  Create Doodle and gather participants
 *  Keep on writing on the
 *  
 * 
 * 
 */


    public void RandomizeQuestionOrder()
    {
        
        QuestionSetOne = menuUI.transform.Find("Interactive Controls/QuestionSetOne");
        if (QuestionSetOne == null)
        {
            return;
        }
        QuestionSetTwo = menuUI.transform.Find("Interactive Controls/QuestionSetTwo");
        QuestionSetThree = menuUI.transform.Find("Interactive Controls/QuestionSetThree");
        System.Random rnd = new System.Random();

        int[] numbers = { 1, 2, 3 };
        numbers = numbers.OrderBy(x => rnd.Next()).ToArray();

        Debug.LogError(numbers);
        QuestionSetOne.SetSiblingIndex(numbers[0]);
        QuestionSetTwo.SetSiblingIndex(numbers[1]);
        QuestionSetThree.SetSiblingIndex(numbers[2]);
    }  
    void TryInitializeDevices()
    {
        var leftHandDevices = new List<InputDevice>();
        var rightHandDevices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (leftHandDevices.Count > 0) leftHand = leftHandDevices[0];
        if (rightHandDevices.Count > 0) rightHand = rightHandDevices[0];
    }

    public void ToggleMenu()
    {
        menuVisible = !menuVisible;
        menuUI.SetActive(menuVisible);    
    }
    void Update()
    {
        if (!leftHand.isValid || !rightHand.isValid)
        {
            TryInitializeDevices();
        }

        bool leftGrip = false;
        bool rightGrip = false;

        leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out leftGrip);
        rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out rightGrip);
        
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand)
                .TryGetFeatureValue(CommonUsages.secondaryButton, out bool bButtonPressed) && bButtonPressed)
        {
            ExperimentManager em = FindFirstObjectByType<ExperimentManager>();
            em.NextSceneButtonPressed();
        }
        
        if (leftGrip)
        {
            menuVisible = !menuVisible;
            menuUI.SetActive(menuVisible);
        }
        
        // if (leftGrip && rightGrip)
        // {
        //     holdTimer += Time.deltaTime;
        //
        //     if (holdTimer >= holdThreshold && !toggledDuringHold)
        //     {
        //         menuVisible = !menuVisible;
        //         menuUI.SetActive(menuVisible);
        //         toggledDuringHold = true;
        //         // Set text
        //         // Transform textTransform = menuUI.transform.Find("Interactive Controls/Question text");
        //         // if (textTransform != null)
        //         // {
        //         //     Text tmpText = textTransform.GetComponent<Text>();
        //         //     if (tmpText != null)
        //         //     {
        //         //         tmpText.text = QUESTION_ONE_TEXT;
        //         //     }
        //         // }
        //     }
        // }
        // else
        // {
        //     // Reset if grip is released
        //     holdTimer = 0f;
        //     toggledDuringHold = false;
        // }
    }
    
    public void SwitchTo(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    


}
