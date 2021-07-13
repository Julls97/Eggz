using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame.Egg
{
    public class EggManager : MonoBehaviour
    {
        [SerializeField] Collider2D deathCollider;
        [SerializeField] EggController eggController;
    }
}
