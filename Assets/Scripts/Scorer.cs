using System;
using UnityEngine;
using TMPro;

public class Scorer : MonoBehaviour
{
 public TextMeshProUGUI scoreText;
 private int hits = 0;

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
  }

  public void SubtractScore()
  {
      hits--;
      UpdateScore(hits);
  }
  
}
