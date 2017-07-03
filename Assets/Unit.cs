using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {
    public float cameraZoom;
    public float cameraPositionYOffset;
    public bool active;

    public abstract void Move();
    public abstract void Attack();

    protected void Update()
    {
        if (active)
        {
            Move();
            Attack();
        }
    }
}
