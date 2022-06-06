using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    public Transform _target;
    public float detailX = 5f;
    public float detailY = 5f;
    public float height = 1.8f;
    public float rotateX;
    public float rotateY;
    [SerializeField]
    private float mouseSensitivity;
    [Header("카메라 기본속성")]
    //카메라 위치 캐싱 준비
    private Transform cameraTransform = null;

    //플레이어 타겟 게임 오브젝트 캐싱 준비
    public GameObject objTarget = null;

    //플레이어 타겟 위치 캐싱 준비
    private Transform objTargetTransform = null;
    private Quaternion lookingRotationX;
    private Quaternion lookingRotationY;



    [Header("3인칭 카메라")]
    //현재 카메라 위치까지 타겟으로부터 뒤로 떨어진 거리
    public float distance = 6.0f;
    //현재 카메라 위치까지 타겟의 위치보다 더 추가적인 높이
    //public float height = 1.75f;

    //Damp란 카메라가 몇 초뒤에 따라갈지에 대한 값이다.
    //실무에선 Damp값 설정에 대해 오랜 시간을 투자한다.

    //카메라 높이에 대한 Damp 값 
    public float heightDamping = 2.0f;
    //카메라 y축 회전에대한 Damp 값
    public float rotationDamping = 3.0f;


    void Start()
    {
        //카메라 위치 캐싱
        cameraTransform = GetComponent<Transform>();

        //플레이어 목표 오브젝트가 존재 하다면.
        if (objTarget != null)
        {
            //플레이어 목표 오브젝트 위치 캐싱
            objTargetTransform = objTarget.transform;
        }
    }

    /// <summary>
    /// 3인칭 카메라 함수
    /// </summary>
    void ThirdCamera()
    {

        float objHeight = objTargetTransform.position.y + height;

        float nowHeight = cameraTransform.position.y;


        nowHeight = Mathf.Lerp(nowHeight, objHeight, heightDamping * Time.deltaTime);
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        var angle = lookingRotationX.eulerAngles;
        
        angle.y += x * mouseSensitivity * Time.deltaTime;

        lookingRotationX.eulerAngles = angle;

        cameraTransform.position = objTargetTransform.position;

        cameraTransform.position -= lookingRotationX * Vector3.forward * distance;

        cameraTransform.position = new Vector3(cameraTransform.position.x, nowHeight, cameraTransform.position.z);


        cameraTransform.LookAt(objTargetTransform);

        angle = lookingRotationY.eulerAngles;
        angle.x += y * mouseSensitivity * Time.deltaTime;
        lookingRotationY.eulerAngles = angle;

        cameraTransform.eulerAngles = angle;



    }



    private void LateUpdate()
    {


        ThirdCamera();
    }

}