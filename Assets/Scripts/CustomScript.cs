using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScript : MonoBehaviour
{
    public float speed = 10f;
    public float JumpHigh = 3f;
    public float ChecDistant = 1f;
    public Rigidbody rigidbody;

    private float Zmove;
    private float Xmove;
    public bool IsGround;
    
    void Start()
    {
        
    }

    void Update()
    {
        Zmove = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Xmove = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        transform.Translate(Xmove, 0, Zmove);

        if (Input.GetKeyDown("space") && IsGround)
        {
            rigidbody.AddForce(Vector2.up * JumpHigh);
        }
        CheckGround();
    }

    void CheckGround()
    {
        Ray point = new Ray(transform.position, Vector2.down);
        RaycastHit hit;

        if (Physics.Raycast(point, out hit, ChecDistant))
        {
            IsGround = true;
        }
        else
        {
            IsGround = false;
        }

        Debug.DrawRay(transform.position, Vector2.down * ChecDistant, Color.green);
    }
}
