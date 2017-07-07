using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public float hp;
    public List<Sprite> spritesDamageProgression;
    private float currentHp;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float spriteChangeHpCount;

    // Use this for initialization
    void Start () {
        currentHp = hp;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteChangeHpCount = hp / spritesDamageProgression.Count;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float damagePoints;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            damagePoints = rb.velocity.magnitude * 8;
        }
        else
        {
            damagePoints = collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 8;
        }

        currentHp -= damagePoints;
        if (currentHp < 0)
            DestroyBlock();
        else
            UpdateSpriteAccordingHp();

    }

    private void UpdateSpriteAccordingHp() {
        int spriteIndex = Mathf.RoundToInt(currentHp / spriteChangeHpCount);
        int lastIndex = spritesDamageProgression.Count-1;
        spriteIndex = Mathf.Clamp(spriteIndex, 0, lastIndex);
        spriteRenderer.sprite = spritesDamageProgression[lastIndex - spriteIndex];
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
