using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : Unit {
    private float powerTimer;
    public GameObject beam;
    private Rigidbody2D rb;

    void Start()
    {
        rb = beam.GetComponent<Rigidbody2D>();
    }

    public override void Attack()
    {
        if (Input.GetKey(KeyCode.A))
            beam.transform.Rotate(Vector3.forward * Time.deltaTime * 20);

        if (Input.GetKeyUp(KeyCode.A))
            rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void Move()
    {
        //throw new NotImplementedException();
    }
}
