using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformColor : MonoBehaviour
{
  public bool m_bWhite;

  // Start is called before the first frame update
  void Start()
  {
  }

  public void onStart()
  {

    if (!m_bWhite)
    {
      GetComponent<SpriteRenderer>().color = Color.black;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }
}
