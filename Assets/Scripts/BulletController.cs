using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    public int Sign;
    [SerializeField]
    private GameObject Sprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("DestroySelf", 2f);
    }

    private void Start()
    {
        Sprite.transform.rotation = Quaternion.Euler(0, 0, Sign * 90);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Sign * 17, 0);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
