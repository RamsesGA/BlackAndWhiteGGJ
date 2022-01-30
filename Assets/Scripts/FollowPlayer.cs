using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    float m_y;
    public Vector3 offset;

    private void Start()
    {
        m_y = transform.position.y;
    }
    // Update is called once per frame

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z); // Camera follows the player with specified offset position
        //if (player.transform.position.y > m_y - offset.y)
        //{
        //}
        //else
        //{
        //    transform.position = new Vector3(player.position.x + offset.x, m_y, transform.position.z); // Camera follows the player with specified offset position
        //}
    }
}
