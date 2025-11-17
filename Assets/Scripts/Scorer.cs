using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Scorer : MonoBehaviour
{
 public TextMeshProUGUI scoreText;
 private int hits = 0;
 public int winScore = 10;

 AudioSource audioSource;
 public void Start()
 {
    UpdateScore(hits);
 }

 public void UpdateScore(int newScore)
  {
    hits = newScore;
    scoreText.text = hits.ToString();
  }

  public void AddScore()
  {
      hits++;
      UpdateScore(hits);
      if (hits >= winScore)
      {
          int currentIndex = SceneManager.GetActiveScene().buildIndex;
          SceneManager.LoadScene(currentIndex + 1);
      }
  }

  public void SubtractScore()
  {
      hits--;
      UpdateScore(hits);
  }
  
}
