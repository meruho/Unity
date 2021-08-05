using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    사용방법!!

    FadeInOut.Inst.FadeIn();
    FadeInOut.Inst.FadeIn(0.5);
    FadeInOut.Inst.FadeIn(0.5,Next);

    FadeInOut.Inst.FadeOut();
    FadeInOut.Inst.FadeOut(0.5);
    FadeInOut.Inst.FadeOut(0.5,Next);

    첫번째 파라미터 : 페이드 시간 (default 1초)
    두번째 파라미터 : 전환하려는 씬 네임 (default null)
 */



public class FadeInOut : MonoBehaviour
{
    private static FadeInOut _inst = null;
    public static FadeInOut Inst
    {
        get
        {
            if(_inst == null)
            {
                _inst = FindObjectOfType(typeof(FadeInOut)) as FadeInOut;
            }
            return _inst;
        }
    }

    //
    //
    private Image fadeImage;
    //
    public void Awake()
    {
        fadeImage = this.GetComponent<Image>();
    }

    /// <summary>
    /// 씬전환 전에 페이드 인
    /// </summary>
    /// <param name="fadeTime"> FadeGap </param>
    /// <param name="sceneName"> NextSceneName </param>
    public void FadeIn(float fadeTime = 1,string sceneName = null)
    {
        StartCoroutine(cFadeIn(fadeTime, sceneName));
    }
    
    private IEnumerator cFadeIn(float fadeTime, string sceneName)
    {
        fadeImage.color = new Color(0, 0, 0, 1);
        Color fadeColor = fadeImage.color;
        float time = 0;
        while (fadeColor.a > 0.1f)
        {
            time += Time.deltaTime / fadeTime;
            fadeColor.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = fadeColor;
            yield return null;
        }

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    /// <summary>
    /// 씬전환 전에 페이드 아웃
    /// </summary>
    /// <param name="fadeTime"> FadeGap </param>
    /// <param name="sceneName"> NextSceneName </param>
    public void FadeOut(float fadeTime = 1, string sceneName = null)
    {
        StartCoroutine(cFadeOut(fadeTime, sceneName));
    }

    private IEnumerator cFadeOut(float fadeTime, string sceneName)
    {
        fadeImage.color = new Color(0, 0, 0, 0);
        Color fadeColor = fadeImage.color;
        float time = 0;
        while(fadeColor.a < 0.99)
        {
            time += Time.deltaTime / fadeTime;
            fadeColor.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = fadeColor;
            yield return null;
        }

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
