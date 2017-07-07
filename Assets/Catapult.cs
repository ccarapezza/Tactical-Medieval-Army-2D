using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : Unit {

    public GameObject beam;
    private Rigidbody2D rigidBodyBeam;
    private float timer;
    private float power;
    public float m_catapultPower;
    private Vector3 initialRotation;

    private float fireTimer;

    private bool fire;
    private bool reloading;

    private bool reloadCoroutineExecute;

    // Use this for initialization
    void Start () {
        timer = 0;
        rigidBodyBeam = beam.GetComponent<Rigidbody2D>();
        initialRotation = beam.transform.eulerAngles;
        LoadProjectile();
    }

    private void LoadProjectile()
    {
        GameObject go = Instantiate(projectilePrefab, transform);
        go.transform.position = projectileLoadPosition.position;
        currentProjectile = go.GetComponent<Projectile>();
    }

    void FireMecanism() {
        if (fire)
        {
            fireTimer += Time.deltaTime;
            
            if ((beam.transform.rotation.eulerAngles.z < 25f && beam.transform.rotation.eulerAngles.z > 0) || beam.transform.rotation.eulerAngles.z > 330f)
            {
                float x = -50f * power;
                rigidBodyBeam.AddTorque(x, ForceMode2D.Force);
            }
            else {
                /*float interpolant = (1f / 110f) * (transform.rotation.eulerAngles.z - 220f);
                rigidBodyBeam.angularDrag = Mathf.Lerp(10f, 0f, (1f / 110f) * (transform.rotation.eulerAngles.z - 220f));
                print(interpolant);
                if (interpolant>=1)*/
                if(!reloadCoroutineExecute)
                    StartCoroutine(ReloadCatapult());
            }
        }
    }

    IEnumerator ReloadCatapult()
    {
        reloadCoroutineExecute = true;
        fire = false;
        Player.Instance.followPorjectile();
        float time = 0;
        float z = beam.transform.eulerAngles.z;

        yield return new WaitForSeconds(2f);
        reloading = true;
        while (time < 1f)
        {
            var rotation = beam.transform.eulerAngles;
            time += Time.deltaTime * 3f;
            rotation.z = Mathf.LerpAngle(z, initialRotation.z, time);

            beam.transform.eulerAngles = rotation;

            yield return null;
        }

        beam.transform.eulerAngles = initialRotation;
        rigidBodyBeam.angularVelocity = 0;
        LoadProjectile();
        reloading = false;
        reloadCoroutineExecute = false;
        yield return null;
    }

    public override void Move()
    {
        //throw new NotImplementedException();
    }

    public override void Attack()
    {
        if (reloading) return;
        if (Input.GetKey(KeyCode.Space))
            timer += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            power = timer * m_catapultPower;
            timer = 0;
            fire = true;
        }
        FireMecanism();
    }
}
