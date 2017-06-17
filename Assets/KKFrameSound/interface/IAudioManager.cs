/********************************************************************
	created:	2017/05/12 		
	file base:	IAudioManager.cs	
	author:		sunjiahaoz
	
	purpose:	声音的基本管理，包括播放音乐，音效，音量设置，静音设置等
    概念：
    音乐(Music)：比较长的流音乐，比如背景音乐
    音效(Sound)：较短的，比如出牌音效

    ISingleGameAudioPart, 每个游戏自己维护的声音资源
*********************************************************************/
using UnityEngine;
using System.Collections;

namespace KK.Frame.Audio
{
    public interface IAudioManager
    {
        // 注册具体游戏的部分
        void RegisterAudioPart(ISingleGameAudioPart iPart);
        // 反注册具体游戏的部分
        void UnRegisterAudioPart(ISingleGameAudioPart iPart);

        /// <summary>
        /// 停止播放音乐
        /// </summary>
        /// <param name="fFade">为0表示立即停止，>0 表示这些秒进行淡出</param>
        void StopMusic(float fFade = 0);
        // 播放音乐
        void PlayMusic(string strName);
        // 播放音效
        void PlaySound(string strName);      

        float Volume_Music { get; set; }
        float Volume_Sound { get; set; }
        bool Mute { get; set; }
    }
}
