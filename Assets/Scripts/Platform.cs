using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpforce;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.relativeVelocity.y <= 0)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 vel = rb.linearVelocity;

            vel.y = jumpforce;
            rb.linearVelocity = vel;

        }
    }
}
