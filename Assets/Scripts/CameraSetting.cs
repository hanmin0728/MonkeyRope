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
    [Header("ī�޶� �⺻�Ӽ�")]
    //ī�޶� ��ġ ĳ�� �غ�
    private Transform cameraTransform = null;

    //�÷��̾� Ÿ�� ���� ������Ʈ ĳ�� �غ�
    public GameObject objTarget = null;

    //�÷��̾� Ÿ�� ��ġ ĳ�� �غ�
    private Transform objTargetTransform = null;
    private Quaternion lookingRotationX;
    private Quaternion lookingRotationY;



    [Header("3��Ī ī�޶�")]
    //���� ī�޶� ��ġ���� Ÿ�����κ��� �ڷ� ������ �Ÿ�
    public float distance = 6.0f;
    //���� ī�޶� ��ġ���� Ÿ���� ��ġ���� �� �߰����� ����
    //public float height = 1.75f;

    //Damp�� ī�޶� �� �ʵڿ� �������� ���� ���̴�.
    //�ǹ����� Damp�� ������ ���� ���� �ð��� �����Ѵ�.

    //ī�޶� ���̿� ���� Damp �� 
    public float heightDamping = 2.0f;
    //ī�޶� y�� ȸ�������� Damp ��
    public float rotationDamping = 3.0f;


    void Start()
    {
        //ī�޶� ��ġ ĳ��
        cameraTransform = GetComponent<Transform>();

        //�÷��̾� ��ǥ ������Ʈ�� ���� �ϴٸ�.
        if (objTarget != null)
        {
            //�÷��̾� ��ǥ ������Ʈ ��ġ ĳ��
            objTargetTransform = objTarget.transform;
        }
    }

    /// <summary>
    /// 3��Ī ī�޶� �Լ�
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