using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    Vector2 moveVelocity;
    Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        Vector2 distanceToMove = moveVelocity * Time.fixedDeltaTime;
        body.MovePosition(body.position + distanceToMove);
    }

}
