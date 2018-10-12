using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float rotateSpeed = 5f;
    public float jumpForce = 6f;

    private Rigidbody rb;
    public LayerMask groundLayer;
    public CapsuleCollider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        groundLayer = LayerMask.GetMask("Default");
    }

    void Update()
    {
        var moveVert = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var moveHoriz = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var rotate = Input.GetAxis("Mouse X") * rotateSpeed;

        transform.Rotate(0, rotate, 0);
        transform.Translate(moveHoriz, 0, moveVert);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump button pressed");
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center,
            new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
            col.radius * 0.9f,
            groundLayer);
    }
}
