using UnityEngine;
using System.Collections;

public class AutoPlayBgmList : MonoBehaviour {
    // 播放时机
    public enum PlayOpp
    {
        Awake,
        Enable,
        Start,
        Manual,
    }

    public AudioController _subAO;
    [Header("播放时机")]
    public PlayOpp _opp = PlayOpp.Manual;
    [Header("播放延迟，只在Awake，Enable,Start时机下有效")]
    public float _fDelay = 0;

    void Awake()
    {
        if (_opp == PlayOpp.Awake)
        {
            StartCoroutine(OnStartPlayList(_fDelay));
        }
    }

    void Start()
    {
        if (_opp == PlayOpp.Start)
        {
            StartCoroutine(OnStartPlayList(_fDelay));
        }
    }

    void OnEnable()
    {
        if (_opp == PlayOpp.Enable)
        {
            StartCoroutine(OnStartPlayList(_fDelay));
        }
    }

    public void PlayList()
    {
        AudioController.PlayMusicPlaylist(_subAO);
    }

    IEnumerator OnStartPlayList(float fDelay)
    {
        if (fDelay > 0)
        {
            yield return new WaitForSeconds(fDelay);
        }
        PlayList();
    }
}
