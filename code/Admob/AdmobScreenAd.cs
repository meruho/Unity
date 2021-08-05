using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobScreenAd : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-3940256099942544/1033173712";
    private readonly string test_unitID = "ca-app-pub-3940256099942544/1033173712";

    private readonly string test_deviceID = "";

    private InterstitialAd screenAd;

    private void Start()
    {
        InitAd();
        //임시로 5초뒤에 쇼
        Invoke("Show",5f);
    }

    private void InitAd()
    {
        string id = Debug.isDebugBuild ? test_unitID : unitID;
        screenAd = new InterstitialAd(id);
        AdRequest request = new AdRequest.Builder().Build();

        screenAd.LoadAd(request);
        screenAd.OnAdClosed += (sender, e) => Debug.Log("광고가 닫힘");  //콜백함수 : 광고가 닫히는 순간 액션
        screenAd.OnAdLoaded += (sender, e) => Debug.Log("광고가 로드됨");//콜백함수 : 광고다 로드되는 순간 액션

        //로드하고 바로 쇼하면 안됨 , 지연시간때문에
        //screenAd.Show();
    }

    //전면광고 원하면 쇼함수 호출
    public void Show()
    {
        StartCoroutine("ShowScreenAd");
    }
    private IEnumerator ShowScreenAd()
    {
        while (!screenAd.IsLoaded())
        {
            yield return null;
        }
        screenAd.Show();
    }
}