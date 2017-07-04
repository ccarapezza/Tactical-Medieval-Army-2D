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
        LoadProjectile();
    }

    private void LoadProjectile()
    {
        GameObject go = Instantiate(projectilePrefab, transform);
        go.transform.position = projectileLoadPosition.position;
        currentProjectile = go.GetComponent<Projectile>();
    }

    public override void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
            beam.transform.Rotate(Vector3.forward * Time.deltaTime * 20);

        if (Input.GetKeyUp(KeyCode.Space)) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    public override void Move()
    {
        //throw new NotImplementedException();
    }
}
