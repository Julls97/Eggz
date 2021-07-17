using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MainGame.Egg
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        [SerializeField] GameObject menuScreen;
        [SerializeField] GameObject gameHolder;
        [SerializeField] EggManager eggManager;

        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }
        private void Start()
        {
            EnableMainMenu();
        }
        public void EnableMainMenu()
        {
            menuScreen.SetActive(true);
        }
        public void LaunchGame()
        {
            menuScreen.SetActive(false);
            eggManager.LaunchGame();
        }
        public void GameOver()
        {
            EnableMainMenu();
        }
    }
}