using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace KK.Frame.Audio
{
    public class KKAudioManagerNor : IAudioManager
    {
#region _Instance_
    public static KKAudioManagerNor Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new KKAudioManagerNor();
            }
            return _Instance;
        }
    }
    private static KKAudioManagerNor _Instance;
#endregion	

        List<ISingleGameAudioPart> _lstParts = new List<ISingleGameAudioPart>();
        float _fVMusic = 1f;        
        float _fVSound = 1f;
        bool _bIsMute = false;
        const float MUTE_VOLUME = 0.000001f;

        public KKAudioManagerNor()
        {
            RestoreDefault();
        }

        #region _IAudioManager_

        public void RegisterAudioPart(ISingleGameAudioPart iPart)
        {
            if (_lstParts.Contains(iPart))
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + "RegisterAudioPart " + iPart + " 已经注册过了");
                return;
            }
            _lstParts.Add(iPart);

            // 初始化音量
            if (Mute)
            {
                iPart.SetVolume_Music(MUTE_VOLUME);
                iPart.SetVolume_Sound(MUTE_VOLUME);                
            }            
            else
            {
                iPart.SetVolume_Music(Volume_Music);
                iPart.SetVolume_Sound(Volume_Sound);
            }

            iPart.OnPartRegistered(this);            
        }

        public void UnRegisterAudioPart(ISingleGameAudioPart iPart)
        {
            if (!_lstParts.Contains(iPart))
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + "UnRegisterAudioPart " + iPart + " 没有注册过，不能进行反注册");
                return;
            }
            iPart.OnPartUnRegistered();
            _lstParts.Remove(iPart);
        }   

        public void StopMusic(float fFade = 0)
        {
            AudioController.StopMusic(fFade);
        }

        public void PlayMusic(string strName)
        {
            AudioController.PlayMusic(strName);
        }

        public void PlaySound(string strName)
        {
            AudioController.Play(strName);
        }
        
        public virtual float Volume_Music
        {
            get
            {
                return _fVMusic;
            }
            set
            {
                _fVMusic = value;
                SetMusicVolume(_fVMusic);
            }
        }
        public virtual float Volume_Sound
        {
            get
            {
                return _fVSound;
            }
            set
            {
                _fVSound = value;
                SetSoundVolume(_fVSound);
            }
        }
        public virtual bool Mute
        {
            get
            {
                return _bIsMute;
            }
            set
            {
                _bIsMute = value;                
                if (_bIsMute)
                {
                    SetMusicVolume(MUTE_VOLUME);
                    SetSoundVolume(MUTE_VOLUME);
                }
                else
                {
                    SetMusicVolume(Volume_Music);
                    SetSoundVolume(Volume_Sound);                    
                }                
            }
        }
        #endregion


        #region _Inner_

        void RestoreDefault()
        {
            Volume_Music = 1f;
            Volume_Sound = 1f;
            Mute = false;
        }

        void SetMusicVolume(float fVolume)
        {
            for (int i = 0; i < _lstParts.Count; ++i)
            {
                _lstParts[i].SetVolume_Music(fVolume);                
            }
        }
        
        void SetSoundVolume(float fVolume)
        {
            for (int i = 0; i < _lstParts.Count; ++i)
            {
                _lstParts[i].SetVolume_Sound(fVolume);                
            }
        }
        #endregion
    }
}
