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

    private float fireTimer;

    private bool fire;

    // Use this for initialization
    void Start () {
        timer = 0;
        rigidBodyBeam = beam.GetComponent<Rigidbody2D>();
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
                //rigidBodyBeam.angularDrag = Mathf.Lerp(10f, 0f, beam.transform.rotation.eulerAngles.z / (330f - 220f));
                rigidBodyBeam.angularDrag = Mathf.Lerp(10f, 0f, (1f / 110f) * (transform.rotation.eulerAngles.z - 220f));
            }
        }
    }

    public override void Move()
    {
        //throw new NotImplementedException();
    }

    public override void Attack()
    {
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
