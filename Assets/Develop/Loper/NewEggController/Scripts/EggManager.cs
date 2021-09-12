using System.Collections.Generic;
using UnityEngine;

namespace Develop.Loper.NewEggController.Scripts
{
    public class EggManager : MonoBehaviour
    {
        [SerializeField] Collider2D deathCollider;
        [SerializeField] Collider2D touchZone;
        [SerializeField] EggController eggControllerPrefab;
        [SerializeField] List<EggController> eggControllersOnScene;
        [SerializeField] Transform spanwPos;
      
        public void LaunchGame()
        {
            SpawnEgg();
        }
        
        public void SpawnEgg()
        {
            EggController newEgg = Instantiate(eggControllerPrefab, spanwPos.position, Quaternion.identity, this.transform);
            newEgg.SetUp((collider) => EggEnterTrigger(newEgg, collider), (collider) => EggExitTrigger(newEgg, collider));
        }
        public void EggEnterTrigger(EggController egg, Collider2D collider)
        {
            
            if (deathCollider == collider)
            {
                eggControllersOnScene.Remove(egg);
                Destroy(egg.gameObject);
                ChechEggsPresents();
            }
            if (touchZone == collider)
            {
                egg.AllowTouch = true;
            }
        }
        public void EggExitTrigger(EggController egg, Collider2D collider)
        {
            if (touchZone == collider)
            {
                egg.AllowTouch = false;
            }
        }
        public void ChechEggsPresents()
        {
            if (eggControllersOnScene.Count == 0)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}
