using UnityEngine;
using System.Collections;

public partial class AudioController : SingletonMonoBehaviour<AudioController>
{
    public static void StopMusic(AudioController ao, float fFadeOut = 0)
    {
        ao._StopMusic(fFadeOut);
    }

    public static void PauseMusic(AudioController ao, float fFadeOut = 0)
    {
        ao._PauseMusic(fFadeOut);
    }

    public static void SetVolume(AudioController ao, float fVolume)
    {
        ao.Volume = fVolume;
    }

    public static AudioObject PlayMusicPlaylist(AudioController ao)
    {
        if (ao == null)
        {
            Debug.LogWarning("<color=orange>[Warning]</color>---" + "AO is Null , Play Default List");
            return AudioController.PlayMusicPlaylist();
        }
        if (ao.musicPlaylist == null
            || ao.musicPlaylist.Length == 0)
        {
            return null;
        }
        AudioController.SetMusicPlaylist(ao.musicPlaylist);
        AudioController.Instance.loopPlaylist = ao.loopPlaylist;
        AudioController.Instance.shufflePlaylist = ao.shufflePlaylist;
        AudioController.Instance.crossfadePlaylist = ao.crossfadePlaylist;
        AudioController.Instance.delayBetweenPlaylistTracks = ao.delayBetweenPlaylistTracks;
        return AudioController.PlayMusicPlaylist();
    }

    public static AudioObject PlayMusicPlaylist(string[] strMusicNames, bool bLoop = false, bool bShuffle = false, bool bCrossFade = false, float fDelayBetweenTrack = 0)
    {
        if (strMusicNames == null
           || strMusicNames.Length == 0)
        {
            return null;
        }
        AudioController.SetMusicPlaylist(strMusicNames);
        AudioController.Instance.loopPlaylist = bLoop;
        AudioController.Instance.shufflePlaylist = bShuffle;
        AudioController.Instance.crossfadePlaylist = bCrossFade;
        AudioController.Instance.delayBetweenPlaylistTracks = fDelayBetweenTrack;
        return AudioController.PlayMusicPlaylist();
    }
}
