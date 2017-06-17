/********************************************************************
	created:	2017/05/12 		
	file base:	ISingleGameAudioPart.cs	
	author:		sunjiahaoz
	
	purpose:	audio 部分接口
*********************************************************************/
using UnityEngine;
using System.Collections;

namespace KK.Frame.Audio
{
    public interface ISingleGameAudioPart
    {
        // 注册成功之后的回调
        void OnPartRegistered(IAudioManager parent);
        // 反注册后的回调
        void OnPartUnRegistered();

        // 设置音效音量
        void SetVolume_Sound(float fV);
        // 设置音乐音量
        void SetVolume_Music(float fV);
    }
}
