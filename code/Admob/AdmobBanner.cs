using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobBanner : MonoBehaviour
{
    private readonly string unitID = "ca-app-pub-3940256099942544/6300978111";
    private readonly string test_unitID = "ca-app-pub-3940256099942544/6300978111";

    private readonly string test_deviceID = ""; //생략가능

    private BannerView banner;

    public AdPosition position;

    private void Start()
    {
        //초기화되고 애드몹 배너 생성하게 ( 코드 안전성 )
        //명시적으로 모바일 셋팅
        MobileAds.Initialize(
            (initStatus) =>
            {
                InitAd();

                //var statusMap = initStatus.getAdapterStatusMap();

                //foreach (var status in statusMap)
                //{
                //    //현재 상테가 키,벨류 형태로 출력
                //    //Debug.Log($"{status.Key} : {status.Value}");
                //}
            }
        );
    }
    private void InitAd()
    {
        //테스트 빌드 할것인지
        string id = Debug.isDebugBuild ? test_unitID : unitID;

        banner = new BannerView(id, AdSize.SmartBanner, position);

        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request = new AdRequest.Builder().AddTestDevice(test_deviceID).Build(); //생략가능

        banner.LoadAd(request);
        //banner.Show();    //배너표시
        //banner.Hide();    //배너감추기
        //banner.Destroy(); //배너파괴
    }
}