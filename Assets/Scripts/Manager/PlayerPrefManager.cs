using UnityEngine;
using System.Collections;

public class PlayerPrefManager : MonoBehaviour {

    public static int GetAudio()
    {
        return PlayerPrefs.GetInt("audio", 1);
    }
    
    public static void SetAudio(int value)
    {
        PlayerPrefs.SetInt("audio", value);
    }

    public static int GetMusic()
    {
        return PlayerPrefs.GetInt("music", 1);
    }

    public static void SetMusic(int value)
    {
        PlayerPrefs.SetInt("music", value);
    }



}
