using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class ARCoreCtrl : MonoBehaviour
{
    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    //
    void Update()
    {
        //이미지추적
        Session.GetTrackables<AugmentedImage>(m_TempAugmentedImages, TrackableQueryFilter.Updated);

        //
        foreach (var image in m_TempAugmentedImages) //여러이미지들중
        {
            if (image.TrackingState == TrackingState.Tracking) //하나가 인식 성공하면
            {
                Anchor anchor = image.CreateAnchor(image.CenterPose); //이미지 중앙에 앵커생성
                vPosImage = image.CenterPose.position; //이미지 중간값 받고
                nIdxImage = image.DatabaseIndex; //이미지 인덱스 받고
                tt.text = "트래킹성공"; //성공 텍스트
                break;
            }
        }

        //평면인식
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        //if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit)){}
        if (Frame.Raycast(Screen.width / 2, Screen.height / 2, raycastFilter, out hit))
        {
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(camera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                //가장 작은 Y축값 찾기(갱신)
                if (hit.Pose.position.y < nMinY)
                {
                    nMinY = hit.Pose.position.y;
                    if (gMap.activeSelf)
                    {
                        //맵이 생성되어 있다면 가장 바닥으로 맵을 갱신
                        gMap.transform.position = new Vector3(gMap.transform.position.x, nMinY, gMap.transform.position.z);
                    }
                }


                //앵커포인트(기본)
                Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

                //플랜
                if (hit.Trackable is DetectedPlane) //플랜
                {
                    float range = ((DetectedPlane)hit.Trackable).ExtentZ; //넓이
                    if (((DetectedPlane)hit.Trackable).PlaneType == DetectedPlaneType.HorizontalUpwardFacing) //형태(수직,수평)
                    {

                    }
                }

                //플랜거리에따라서 이미지 출력!!
                if (hit.Trackable is DetectedPlane) // 1. 플랜
                {
                    if (((DetectedPlane)hit.Trackable).PlaneType == DetectedPlaneType.Vertical) // 2. 수직
                    {
                        //if (nMinY < -0.1f)
                        //{
                        float dis = Vector3.Distance(vPosImage, hit.Pose.position);
                        if (dis < 0.1f && !isMap)
                        {
                            var anchor = hit.Trackable.CreateAnchor(hit.Pose);
                            if (!gMap.activeSelf) gMap.SetActive(true);
                            gMap.transform.parent = anchor.transform;

                            //이미지에 따른 포인트 위치
                            Vector3 vWayPointPos = -gWayPoints.transform.GetChild(nIdxImage).transform.localPosition;
                            pos = vWayPointPos;

                            gMap.transform.position = new Vector3(vWayPointPos.x, nMinY, vWayPointPos.z);
                            //gMap.transform.rotation = Quaternion.Euler(0, 0, 0);
                            t2.text = "맵생성 성공";
                            isMap = true;
                        }
                        // }
                    }
                }
                //
            }
        }

        //화면터치
        Touch touch;
        if (Input.touchCount >= 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = camera.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit2;
            if (Physics.Raycast(ray, out hit2))
            {
                //
            }
        }
    }
}
