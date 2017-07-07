using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float explodePower;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.E))
            Explode();

        if (Input.GetKeyUp(KeyCode.D))
            Divide();
    }

    private void Explode() {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer("Bricks") || collider.gameObject.layer == LayerMask.NameToLayer("Bosses")) {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                Vector3 distanceVector = rb.transform.position - transform.position;
                rb.AddForce(distanceVector*explodePower, ForceMode2D.Impulse);
            }
        }
        Destroy(gameObject);
    }

    private void Divide()
    {
        GameObject clonedProjA = Instantiate(gameObject, transform.position + Vector3.up * 0.2f, transform.rotation);
        GameObject clonedProjB = Instantiate(gameObject, transform.position + Vector3.down * 0.2f, transform.rotation);

        clonedProjA.GetComponent<Projectile>().enabled=false;
        clonedProjB.GetComponent<Projectile>().enabled = false;

        Vector3 currentVelocity = transform.GetComponent<Rigidbody2D>().velocity;

        clonedProjA.GetComponent<Rigidbody2D>().velocity = currentVelocity;
        clonedProjB.GetComponent<Rigidbody2D>().velocity = currentVelocity;

        clonedProjA.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,0.5f)*20f, ForceMode2D.Impulse);
        clonedProjB.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -0.5f)*20f, ForceMode2D.Impulse);

        Vector3 newScale = transform.localScale * 0.8f;
        clonedProjA.transform.localScale = newScale;
        clonedProjB.transform.localScale = newScale;
        transform.localScale = newScale;
    }
}
