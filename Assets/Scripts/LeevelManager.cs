using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeevelManager : MonoBehaviour
{
  public ObjectColored[] m_allPlatforms;
  public List<GameObject> m_whitePlatforms;
  public List<GameObject> m_BlackPlatforms;
  public GameObject m_player;
  PlayerMovement m_playerScript;
  int m_whiteSize;
  int m_blackSize;
  bool m_bInWhite = true;

  // Start is called before the first frame update
  void Start()
  {
    m_allPlatforms = FindObjectsOfType<ObjectColored>();
    m_playerScript = FindObjectOfType<PlayerMovement>();
    m_player = m_playerScript.gameObject;
    var size = m_allPlatforms.Length;
    for (int i = 0; i < size; i++)
    {
      var obj = m_allPlatforms[i].gameObject;
      var platform = obj.GetComponent<PlatformColor>();
      if (platform)
      {
        platform.onStart();
      }
      if (m_allPlatforms[i].m_bWhite) {
        m_whitePlatforms.Add(obj);
      }
      else
        m_BlackPlatforms.Add(obj);
    }
    m_whiteSize = m_whitePlatforms.Count;
    m_blackSize = m_BlackPlatforms.Count;
    for (int i = 0; i < m_blackSize; i++)
    {
      var sprite = m_BlackPlatforms[i].GetComponent<SpriteRenderer>();
      sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.2f);
      var col = m_BlackPlatforms[i].GetComponent<BoxCollider2D>();
      col.enabled = false;
    }
    //FindObjectOfType<Camera>().backgroundColor = Color.black;
  }
  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      changeColor();
    }
  }

  public void changeColor() {
    if (m_bInWhite)
    {
      foreach (var white in m_whitePlatforms)
      {
        //white.SetActive(false);
        var sprite = white.GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.2f);
        var col = white.GetComponent<BoxCollider2D>();
        col.enabled = false;
      }
      foreach (var black in m_BlackPlatforms)
      {
        //black.SetActive(true);
        var sprite = black.GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
        var col = black.GetComponent<BoxCollider2D>();
        col.enabled = true;
      }
      //m_player.GetComponent<SpriteRenderer>().color = Color.black;

     //FindObjectOfType<Camera>().backgroundColor = Color.white;
      m_bInWhite = false;
      m_playerScript.m_inWhite = false;
    }
    else
    {
      foreach (var white in m_whitePlatforms)
      {
        //white.SetActive(true);
        var sprite = white.GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f);
        var col = white.GetComponent<BoxCollider2D>();
        col.enabled = true;
      }
      foreach (var black in m_BlackPlatforms)
      {
        //black.SetActive(true);
        var sprite = black.GetComponent<SpriteRenderer>();
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b,0.2f);
        var col = black.GetComponent<BoxCollider2D>();
        col.enabled = false;
      }
      //m_player.GetComponent<SpriteRenderer>().color = Color.white;
      //FindObjectOfType<Camera>().backgroundColor = Color.black;
      //foreach (var white in m_whitePlatforms)
      //  white.SetActive(true);
      //foreach (var black in m_BlackPlatforms)
      //  black.SetActive(false);

      m_bInWhite = true;
      m_playerScript.m_inWhite = true;
    }
  }
}
