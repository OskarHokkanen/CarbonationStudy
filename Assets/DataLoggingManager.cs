using System.IO;
using UnityEngine;

public class DataLoggingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private string filePath;
    void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "CarbonationStudyLog.csv");

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "ParticipantNumber, SceneLetter, PerceivedCarbonation, Confidence\n");
        }
    }
    
    public void LogAnswer(int ParticipantNumber, char  SceneLetter, string PerceivedCarbonation, string Confidence)
    {
        File.AppendAllText(filePath, $"{ParticipantNumber}, {SceneLetter}, {PerceivedCarbonation}, {Confidence}\n");
    } 

    // Update is called once per frame
    void Update()
    {
        
    }
}
