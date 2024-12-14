/*using System;
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

        *//*var payload = new { audio = audioBase64 };*//*

        STTPayload payload = new STTPayload
        {
            Audio = audioBase64
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
        public string Audio;
    }
}
*/

/*using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SpeechToTextFileUpload : MonoBehaviour
{
    public string endpointUrl = "https://5f58800448fa48e88a.gradio.live/";
    public string audioFilePath = "path/to/your/audio_sample.wav";

    public void Start()
    {
        StartCoroutine(UploadAudioFile(audioFilePath));
    }

    private IEnumerator UploadAudioFile(string filePath)
    {
        // Create a new UnityWebRequest
        UnityWebRequest request = UnityWebRequest.PostWwwForm(endpointUrl, "");

        // Load the audio file
        byte[] audioFileData;
        try
        {
            audioFileData = System.IO.File.ReadAllBytes(filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading audio file: {ex.Message}");
            yield break;
        }

        // Create a form for the request and add the file
        WWWForm form = new WWWForm();
        form.AddBinaryData("audio", audioFileData, "audio_sample.wav", "audio/wav");
        request.uploadHandler = new UploadHandlerRaw(form.data);
        request.SetRequestHeader("Content-Type", "multipart/form-data");

        // Download handler for the response
        request.downloadHandler = new DownloadHandlerBuffer();

        // Send the request
        yield return request.SendWebRequest();

        // Handle response
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log($"Transcription Result: {responseText}");
        }
    }
}*/

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SpeechToTextUnity : MonoBehaviour
{
    public string apiEndpoint = "https://660e-34-87-135-95.ngrok-free.app/api/predict"; // Replace with your ngrok public URL
    public string audioFilePath = "path/to/your/audio_sample.wav"; // Replace with the local path to your audio file

    void Start()
    {
        StartCoroutine(UploadAudio(audioFilePath));
    }

    private IEnumerator UploadAudio(string filePath)
    {
        byte[] audioData;
        try
        {
            audioData = System.IO.File.ReadAllBytes(filePath);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Error loading audio file: {ex.Message}");
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddBinaryData("data", audioData, "audio_sample.wav", "audio/wav");

        using (UnityWebRequest request = UnityWebRequest.Post(apiEndpoint, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
            }
            else
            {
                Debug.Log($"Transcription Result: {request.downloadHandler.text}");
            }
        }
    }
}

