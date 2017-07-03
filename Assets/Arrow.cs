using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ThrowArrow(float impulse)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(transform.right * impulse, ForceMode2D.Impulse);
    }

    void Update()
    {
        if (!rb.isKinematic && rb.velocity.magnitude>0.1f) {
            Vector3 dir = rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
