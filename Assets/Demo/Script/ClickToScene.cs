using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickToScene : MonoBehaviour {

    public string[] strToScene;

    void OnGUI()
    {
        for (int i = 0; i < strToScene.Length; ++i)
        {
            if (GUILayout.Button(strToScene[i], GUILayout.Width(100), GUILayout.Height(100)))
            {
                SceneManager.LoadScene(strToScene[i]);
            }
        }        
    }
}
