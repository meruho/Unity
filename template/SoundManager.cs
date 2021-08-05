using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] audioClips;
    public Dictionary<string, AudioSource> SoundDic;
    //
    void Start()
    {

        SoundDic = new Dictionary<string, AudioSource>();

        int Len = audioClips.Length;

        for (int i = 0; i < Len; i++)
        {
            this.gameObject.AddComponent<AudioSource>();
        }

        AudioSource[] _as = this.GetComponentsInChildren<AudioSource>();

        for (int i = 0; i < Len; i++)
        {
            _as[i].clip = audioClips[i];
            _as[i].playOnAwake = false;
            SoundDic.Add(_as[i].clip.name, _as[i]);
        }
    }

    public void Play(string name, bool loop = false, float volume = 1)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.loop = loop;
            aS.volume = volume;
            aS.Play();
        }
    }

    public void PlayOneShot(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.PlayOneShot(aS.clip);
        }
    }

    public void Stop(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            aS.Stop();
        }
    }

    public bool isPlay(string name)
    {
        if (SoundDic.ContainsKey(name))
        {
            AudioSource aS = SoundDic[name];
            return aS.isPlaying;
        }
        return false;
    }
}

// 사운드매니저 어떻게 하면 편하게 사용할수 있다가 생각하다가?
// 스크립트 하나만 컴포넌트 하면 다 해결할수 있는 방향으로 생각하다가
// 등록한 사운드 클립만큽 오디오소스를 컴포넌트 해서 관리!!
