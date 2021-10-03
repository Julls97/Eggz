using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EggNamespace.Cosmetic
{
    [CreateAssetMenu(fileName ="New egg data",menuName = "Egg")]
    public class EggCosmeticData : ScriptableObject
    {
        public string cosmeticId;
        public Sprite previewImage;
        public Sprite gameSprite;
    }
}