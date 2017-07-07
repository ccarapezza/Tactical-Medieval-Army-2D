using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : Unit {
    public GameObject beam;
    private Rigidbody2D rb;
    private bool throwed;
    private Vector3 initialRotation;
    private bool reloading;

    void Start()
    {
        rb = beam.GetComponent<Rigidbody2D>();
        LoadProjectile();
        throwed = false;
        initialRotation = transform.eulerAngles;
        reloading = false;
    }

    private void LoadProjectile()
    {
        GameObject go = Instantiate(projectilePrefab, transform);
        go.transform.position = projectileLoadPosition.position;
        currentProjectile = go.GetComponent<Projectile>();
    }

    public override void Attack()
    {
        if (reloading) return;
        if (Input.GetKey(KeyCode.Space))
            beam.transform.Rotate(Vector3.forward * Time.deltaTime * 25);

        if (Input.GetKeyUp(KeyCode.Space))
            rb.bodyType = RigidbodyType2D.Dynamic;

        if (Input.GetKeyUp(KeyCode.R))
            StartCoroutine(ReloadTrebuchet());
    }

    public override void Move()
    {
        //throw new NotImplementedException();
    }

    IEnumerator ReloadTrebuchet()
    {
        yield return new WaitForSeconds(2f);
        reloading = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        while (beam.transform.eulerAngles != initialRotation) {
            print("rotate");
            //beam.transform.Rotate(Vector3.forward * Time.deltaTime * 25);
            beam.transform.rotation = Quaternion.identity * Quaternion.Euler(0f,0f,0.1f);
            yield return new WaitForEndOfFrame();
        }
        LoadProjectile();
        reloading = false;
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        if (beam.transform.eulerAngles.z < 315f && beam.transform.eulerAngles.z >270f) {
            if (!throwed) {
                throwed = true;
                currentProjectile.GetComponent<Rigidbody2D>().velocity = currentProjectile.GetComponent<Rigidbody2D>().velocity * 1.6f;
                rb.angularDrag = 10;
                StartCoroutine(ReloadTrebuchet());
            }
        }
    }
}
