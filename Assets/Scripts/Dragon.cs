using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float speed;

    private float offset;

    void Start()
    {
        // Offset must be negative!
        offset = transform.position.x;
    }

    void Update()
    {
        // Creep forward
        transform.position += new Vector3(speed * Time.deltaTime, transform.position.y);

        // Snap to left camera edge
        if (cam.position.x + offset > transform.position.x)
        {
            transform.position = new Vector3(cam.transform.position.x + offset, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
