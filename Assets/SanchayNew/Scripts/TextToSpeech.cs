using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;
using System;
using System.IO;

public class TextToSpeech : MonoBehaviour
{
    [SerializeField] string APIKEY, VoiceID;

    string url;

    public AudioSource audioSource;

    public Button TTsButton;

    public string textMessage;

    public string outputPath, filename;

    Animator anim;

    private void Awake()
    {
        TTsButton.onClick.AddListener(() => startTTs(textMessage));
        outputPath = /*Application.persistentDataPath;*/  System.IO.Path.Combine(Application.persistentDataPath, filename);


        url = "https://api.elevenlabs.io/v1/text-to-speech/" + VoiceID + "/stream";
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    [System.Serializable]
    public class TTsRequest
    {
        public string text;
        public string model_id;
        public VoiceSettings voice_Settings;
    }

    [System.Serializable]
    public class VoiceSettings
    {
        public float stability;
        public float similarity_boost;
        public float style;
        public bool use_speaker_boost;
    }

    public void startTTs(string message)
    {
        TTsRequest jsonData = new TTsRequest
        {
            text = message,
            model_id = "eleven_multilingual_v2",
            voice_Settings = new VoiceSettings
            {
                stability = 0.5f,
                similarity_boost = 0.8f,
                style = 0.0f,
                use_speaker_boost = true
            }
        };

        string jsonString = JsonUtility.ToJson(jsonData);

        Debug.Log(jsonString);

        StartCoroutine(sendTTsRequest(jsonString));
    }

    IEnumerator sendTTsRequest(string jsonDatatoEncode)
    {
        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonDatatoEncode);
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);

            req.downloadHandler = new DownloadHandlerBuffer();

            req.SetRequestHeader("Content-Type", "application/json");
            req.SetRequestHeader("xi-api-key",APIKEY);

            yield return req.SendWebRequest();

            if(req.result==UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.Log("error --> send hone ke baad wala error");
                Debug.LogError($"Error: {req.error}");
            }
            else
            {
                /*string responseText = req.downloadHandler.text;

                var response = JsonUtility.FromJson<TTsResponse>(responseText);
                if(response.Success && !string.IsNullOrEmpty(response.result.media_url))
                {
                    Debug.Log("Found media url, Downloading the audio File");

                    StartCoroutine(DownloadAudio(response.result.media_url));
                }
                else
                {
                    Debug.Log("Error 2 response fetch jo hua usme khuch error hai");
                }*/
                byte[] audioData = req.downloadHandler.data;
                SaveAudioToFile(audioData, outputPath);
            }
        }


    }

    private void SaveAudioToFile(byte[] audioData, string path)
    {
        try
        {
            System.IO.File.WriteAllBytes(path, audioData);
            Debug.Log($"Audio saved successfully at {path}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to save audio: {ex.Message}");
        }

        StartCoroutine(playAudioFile(path));
    }

    IEnumerator playAudioFile(string path)
    {
        using (UnityWebRequest audioReq = UnityWebRequestMultimedia.GetAudioClip($"file://{path}", AudioType.MPEG))
        {
            yield return audioReq.SendWebRequest();

            if(audioReq.result == UnityWebRequest.Result.ConnectionError || audioReq.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Failed to load audio: {audioReq.error}");
            }

            else
            {
                AudioClip clip= DownloadHandlerAudioClip.GetContent(audioReq);
                audioSource.clip = clip;
                audioSource.Play();
                anim.SetTrigger("talking");
            }
        }
    }


    /*IEnumerator DownloadAudio(string mediaUrl)
    {
        using (UnityWebRequest audioReq = UnityWebRequestMultimedia.GetAudioClip(mediaUrl, AudioType.WAV))
        {
            yield return audioReq.SendWebRequest();

            if (audioReq.result == UnityWebRequest.Result.ConnectionError || audioReq.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Error 3 audio file download nhi ho rha error");
            }

            else
            {
                AudioClip downloadedAudio = DownloadHandlerAudioClip.GetContent(audioReq);

                audioSource.clip = downloadedAudio;
                audioSource.Play();
                Debug.Log("Everything worked voila!");
            }
        }
    }*/


    [System.Serializable]
    private class TTsResponse
    {
        public bool Success;
        public TTsResult result;
    }   
    
    [System.Serializable]
    private class TTsResult
    {
        public string media_url;
    }
}
