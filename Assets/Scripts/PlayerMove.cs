using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float BodyRotateSpd = 2.0f;
    [SerializeField]
    private float VelocityChangeSpd = 0.1f;
    private Vector3 CurrentVelocitySpd = Vector3.zero;
    private float verticalSpd = 0f;
    Vector3 forward;
    [SerializeField]
    private float mouseSensitivity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        forward = Camera.main.transform.forward;
        forward.y = 0.0f;

        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        Vector3 pos = h * right + v * forward;

        Vector3 _vecTemp = new Vector3(0f, verticalSpd, 0f);
        rb.velocity = (pos * speed) + _vecTemp + Vector3.up * rb.velocity.y;
    }




}
