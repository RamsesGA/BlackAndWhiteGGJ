using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private float parallaxEffect;

    private float length;
    private float startPosition;

    void Start()
    {
        startPosition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float parallaxEnd = (cam.transform.position.x * (1.0f - parallaxEffect)); // Relative to camera
        float distance    = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPosition + distance, transform.position.y, transform.position.z);

        // Parallax has reached positive end
        if (parallaxEnd > startPosition + length)
        {
            startPosition += length;
        }

        // Parallax has reached negative end
        else if (parallaxEnd < startPosition - length)
        {
            startPosition -= length;
        }
    }
}