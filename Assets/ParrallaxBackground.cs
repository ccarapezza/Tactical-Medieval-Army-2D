using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrallaxBackground : MonoBehaviour {

    public Transform leftSprite;
    public Transform centerSprite;
    public Transform rightSprite;

    public float spriteOffset;

    private Transform mainCamera;
    private float centerSpriteX;

    void Start () {
        mainCamera = Camera.main.transform;
    }
	
	void Update () {
        float limitRight = centerSprite.transform.position.x + (spriteOffset / 2) ;
        float limitLeft = centerSprite.transform.position.x - (spriteOffset / 2);

        if (mainCamera.position.x > limitRight)
            MoveRight();

        if (mainCamera.position.x < limitLeft)
            MoveLeft();
    }

    void MoveLeft() {
        Transform spriteMoveToLeft = rightSprite;
        spriteMoveToLeft.transform.position = new Vector3(leftSprite.transform.position.x - spriteOffset, spriteMoveToLeft.transform.position.y, 0);
        rightSprite = centerSprite;
        centerSprite = leftSprite;
        leftSprite = spriteMoveToLeft;
    }

    void MoveRight() {       
        Transform spriteMoveToRight = leftSprite;
        spriteMoveToRight.transform.position = new Vector3(rightSprite.transform.position.x + spriteOffset, spriteMoveToRight.transform.position.y, 0);
        leftSprite = centerSprite;
        centerSprite = rightSprite;
        rightSprite = spriteMoveToRight;
    }
}