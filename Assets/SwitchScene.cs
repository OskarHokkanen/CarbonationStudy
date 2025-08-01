using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{
    public GameObject menuUI;
    public float holdThreshold = 3.0f;

    private InputDevice leftHand;
    private InputDevice rightHand;

    private float holdTimer = 0f;
    private bool menuVisible = false;
    private bool toggledDuringHold = false;
    public string QUESTION_ONE_TEXT = "Question one";
    
    void Start()
    {
        TryInitializeDevices();
        Transform textTransform = menuUI.transform.Find("Interactive Controls/Question text");
        if (textTransform != null)
        {
            Text tmpText = textTransform.GetComponent<Text>();
            if (tmpText != null)
            {
                tmpText.text = QUESTION_ONE_TEXT;
            }
        }
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

        if (leftGrip && rightGrip)
        {
            holdTimer += Time.deltaTime;

            if (holdTimer >= holdThreshold && !toggledDuringHold)
            {
                menuVisible = !menuVisible;
                menuUI.SetActive(menuVisible);
                toggledDuringHold = true;
                // Set text
                Transform textTransform = menuUI.transform.Find("Interactive Controls/Question text");
                if (textTransform != null)
                {
                    Text tmpText = textTransform.GetComponent<Text>();
                    if (tmpText != null)
                    {
                        tmpText.text = QUESTION_ONE_TEXT;
                    }
                }
            }
        }
        else
        {
            // Reset if grip is released
            holdTimer = 0f;
            toggledDuringHold = false;
        }
    }
    public void SwitchTo(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
    


}
