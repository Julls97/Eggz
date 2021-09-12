using TMPro;
using UnityEngine;

namespace Develop.Loper.NewEggController.Scripts
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
