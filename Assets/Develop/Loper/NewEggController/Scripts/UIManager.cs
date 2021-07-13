using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MainGame.Egg
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI scoreText;

        public void ResetScore()
        {
            scoreText.text = 0000.ToString();
        }
        public void UpdateScore(int value)
        {
            scoreText.text = value.ToString();
        }
    }
}
