using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Unit {

    public float sensibility;
    public float maxPowerTime;
    public float maxArrowPower;
    public Transform archerTop;
    private float currentPowerTimer;
    private Arrow currentArrow;

    void Start () {
        currentPowerTimer = 0f;
        currentArrow = GetComponentInChildren<Arrow>();
    }

    public override void Move()
    {
        archerTop.Rotate(new Vector3(0, 0, Input.GetAxis("Vertical")) * Time.deltaTime * sensibility);
    }

    public override void Attack()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentPowerTimer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            float impulseMultiply = Mathf.Clamp((currentPowerTimer / maxPowerTime) * maxArrowPower, 0, maxArrowPower);
            currentArrow.ThrowArrow(impulseMultiply);
        }
    }
}