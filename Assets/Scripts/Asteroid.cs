using UnityEngine;
using System.Collections.Generic;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;

    [SerializeField] private float speed = 30.0f;

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
    }
    public void SetTrajectory(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}

