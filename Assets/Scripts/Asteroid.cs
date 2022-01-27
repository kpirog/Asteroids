using UnityEngine;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private float speed = 30.0f;
    [SerializeField] private float maxLifeTime = 10.0f;

    [HideInInspector] public float maxSize = 1.5f;
    [HideInInspector] public float minSize = 0.5f;
    [HideInInspector] public float size = 1.0f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
        transform.rotation = Quaternion.AngleAxis(Random.value * 360f, Vector3.forward);

        transform.localScale = Vector3.one * size;
        rb.mass = size;

        Destroy(gameObject, maxLifeTime);
    }
    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet") && size >= maxSize / 2)
        {
            Split();
            Split();
        }

        Destroy(gameObject);
    }
    private void Split()
    {
        Vector3 spawnPosition = Random.insideUnitCircle / 2;
        Vector3 position = transform.position;
        position += spawnPosition;

        Asteroid asteroid = Instantiate(this, position, Quaternion.identity);
        asteroid.size = this.size / 2;

        asteroid.SetTrajectory(position);
    }
}

