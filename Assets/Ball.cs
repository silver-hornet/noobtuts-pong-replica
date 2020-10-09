using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 30;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }
            
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Note: 'collision' holds the collision information. If the
        // Ball collided with a racket, then:
        //   collision.gameObject is the racket
        //   collision.transform.position is the racket's position
        //   collision.collider is the racket's collider

        // Hit the left racket?
        if (collision.gameObject.name == "Racket Left")
        {
            // Calculate hit factor
            float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit the right racket?
        if (collision.gameObject.name == "Racket Right")
        {
            // Calculate hit factor
            float y = hitFactor(transform.position, collision.transform.position, collision.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
    }
}
