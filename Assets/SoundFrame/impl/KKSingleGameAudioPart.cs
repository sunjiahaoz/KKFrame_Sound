/********************************************************************
	created:	2017/05/13 		
	file base:	KaKaSingleGameAudioPart.cs	
	author:		sunjiahaoz
	
	purpose:	AudioPart的实现。
    一般为没有游戏场景一个标记为Addtional的AudioController，此脚本挂接在该AO对象上
    并设置音乐目录与音效目录，主要用于设置音量等操作。
    在Start时注册进IAudioManager,这个注册主要是对默认音量的一个设置等。
*********************************************************************/
using UnityEngine;
using System.Collections;

namespace KK.Frame.Audio
{
    public class KKSingleGameAudioPart : MonoBehaviour, ISingleGameAudioPart
    {
        [Header("为None表示从当前对象获取")]
        public AudioController _subAO;

        public string[] _strMusicCategories;
        public string[] _strSoundCategories;
        protected virtual void Start()
        {
            KKAudioManagerNor.Instance.RegisterAudioPart(this);
            if (_subAO == null)
            {
                _subAO = GetComponent<AudioController>();
            }
            if (_subAO == null)
            {
                Debug.LogWarning("<color=orange>[Warning]</color>---" + "找不到AudioController引用！", this);
            }
        }
        protected virtual void OnDestroy()
        {
            KKAudioManagerNor.Instance.UnRegisterAudioPart(this);
        }

        public virtual string[] MusicCategories
        {
            get
            {
                return _strMusicCategories;
            }
        }

        public virtual string[] SoundCategories
        {
            get { return _strSoundCategories; }
        }


        #region _ISingleGameAudioPart_
        public virtual void OnPartRegistered(IAudioManager parent)
        {

        }
        public virtual void OnPartUnRegistered()
        {

        }

        public virtual void SetVolume_Music(float fV)
        {
            for (int i = 0; i < MusicCategories.Length; ++i)
            {
                AudioController.SetCategoryVolume(MusicCategories[i], fV);
            }
        }

        public virtual void SetVolume_Sound(float fV)
        {
            for (int i = 0; i < SoundCategories.Length; ++i)
            {
                AudioController.SetCategoryVolume(SoundCategories[i], fV);
            }
        }
        #endregion
    }
}
