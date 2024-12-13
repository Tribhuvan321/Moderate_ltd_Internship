using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SpeechToText : MonoBehaviour
{
 
    
    public string filePath = "path-to-your-audio-file";
    public Button SpeechToTextButton;

    public string endpointUrl;

    private void Awake()
    {
        SpeechToTextButton.onClick.AddListener(() => StartSTT());
    }

    private void Start()
    {
        StartSTT();
    }

    public void StartSTT()
    {
        StartCoroutine(SendAudioForTranscription(filePath));
    }

    private IEnumerator SendAudioForTranscription(string filePath)
    {
        byte[] audioData;
        try
        {
            audioData = System.IO.File.ReadAllBytes(filePath);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error loading audio file: {ex.Message}");
            yield break;
        }

        string audioBase64 = Convert.ToBase64String(audioData);

        /*var payload = new { audio = audioBase64 };*/

        STTPayload payload = new STTPayload
        {
            audio = audioBase64
        };

        string jsonPayload = JsonUtility.ToJson(payload);
        Debug.Log(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(endpointUrl, "POST"))
        {
           
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
            }
            else
            {
                string responseText = request.downloadHandler.text;
                var response = JsonUtility.FromJson<STTResponse>(responseText);

                Debug.Log($"Transcribed Text: {response.Text}");
            }
        }
    }

    [Serializable]
    private class STTResponse
    {
        public string Text;
    }

    [Serializable]
    private class STTPayload
    {
        public string audio;
    }
}
