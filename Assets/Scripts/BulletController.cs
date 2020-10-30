using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    public int Sign;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("DestroySelf", 2f);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Sign * 17, 0);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("Ground"))
        //    Destroy(gameObject);
    }
}
