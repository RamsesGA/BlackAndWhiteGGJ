using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonChangeColor : ObjectColored
{
  Animator m_animator;
  SpriteRenderer m_spriteR;
  // Start is called before the first frame update
  void Start()
  {
    m_spriteR = GetComponent<SpriteRenderer>();
    m_animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    m_spriteR.color = new Color(m_spriteR.color.r, m_spriteR.color.g, m_spriteR.color.b, 1f);
    if (m_animator)
    {
      m_animator.SetBool("InWhite", m_bWhite);
    }
  }
}
