using UnityEngine;
using System.Collections;
using KK.Frame.Audio;

public class Demo : MonoBehaviour {
    public string _strMusic;
    public string _strSFX;

    void OnGUI()
    {
        if (GUILayout.Button("Click_Music", GUILayout.Width(100), GUILayout.Height(100)))
        {
            KKAudioManagerNor.Instance.PlayMusic(_strMusic);
        }
        if (GUILayout.Button("Click_SFX", GUILayout.Width(100), GUILayout.Height(100)))
        {
            KKAudioManagerNor.Instance.PlaySound(_strSFX);
        }
    }
}
