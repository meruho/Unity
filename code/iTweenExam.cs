using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTweenExam : MonoBehaviour {

    private Hashtable ht1 = new Hashtable();
    private Hashtable ht2 = new Hashtable();
    //
    void Start () {
        ht1.Add("amount", new Vector3(0, -90, 0));  // 변할 값을 정의한다.
        ht1.Add("time", 0.3f);  //몇초동안 할건지.
        ht1.Add("easetype", iTween.EaseType.easeInOutElastic);  //어떤느낌으로 할건지.
        iTween.RotateAdd(this.gameObject, ht1); // ht1의 값으로 회전을 추가 하겟다.
        //그러면 Y축 0.3초동안 Y축 -90도로 회전한다.

        ht2.Add("x", 5.0f);
        ht2.Add("time", 3.0f);
        ht2.Add("delay", 2.0f);
        ht2.Add("onupdate", "myUpdateFunction");    //업데이트가 되면 적힌 함수를 실행하라
        ht2.Add("easeType", iTween.EaseType.linear);
        ht2.Add("looptype", iTween.LoopType.none); //루프타입 : none , loop , pingpong
        iTween.ScaleTo(gameObject, ht2);
        iTween.MoveTo(gameObject, ht2);
    }
}
