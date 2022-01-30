using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class FallSpikes : MonoBehaviour
{
  public float m_fallSpeed = 1.0f; 
  public float m_gravity = 9.81f; 
  BoxCollider2D m_boxCollider;
  public bool m_bPlayerDown =  false;
  Vector3 m_colSize;

  [SerializeField] private LayerMask m_playerLayer;

  // Start is called before the first frame update
  void Start()
  {
    m_boxCollider = GetComponent<BoxCollider2D>();
    m_colSize = m_boxCollider.bounds.size;
  }

  // Update is called once per frame
  void Update()
  {
    if (!m_bPlayerDown)
    {
      m_bPlayerDown = checkPlayerBottom();
    }
    else
    {
      fall();
    }
  }

  bool checkPlayerBottom()
  {
    RaycastHit2D cast =
     Physics2D.BoxCast(m_boxCollider.bounds.center, m_colSize, 0, Vector2.down, 10000.0f, m_playerLayer);
    return cast.collider != null;
  }

  void fall()
  {
    float posY = gameObject.transform.position.y + (m_fallSpeed * -m_gravity * Time.deltaTime);
    gameObject.transform.position = new Vector3(gameObject.transform.position.x, posY, gameObject.transform.position.z);
  }
}
