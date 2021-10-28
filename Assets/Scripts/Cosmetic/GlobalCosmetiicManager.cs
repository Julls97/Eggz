using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EggNamespace.Cosmetic
{
    public class EggAvailabilityWrapper
    {
        public EggCosmeticData CosmeticData {get;set;}
        public EggCosmeticAvailability CosmeticAvailability { get; set; }
    }
    public class GlobalCosmetiicManager : MonoBehaviour
    {
        [SerializeField] private List<EggCosmeticData> cosmeticDataList = new List<EggCosmeticData>();
        [SerializeField] private List<EggAvailabilityWrapper> EggAvailabilityWrapperList;
        private static EggCosmeticData currentCosmetic;
        public EggCosmeticData CurrentCosmetic => currentCosmetic;

        public static GlobalCosmetiicManager Instance;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);

            }
            else
            {
                Instance = this;
            }
        }

        public List<EggCosmeticData> GetDataList()
        {
            return cosmeticDataList;
        }
        public void SetNewCurrentCosmetic(EggCosmeticData newCosmetic)
        {
            if (newCosmetic != null)
                currentCosmetic = newCosmetic;
            else
                Debug.LogError("New Cosmetic property was null");
        }
        public void SetNewCurrentCosmetic(string newID)
        {
            EggCosmeticData pickedCosmetic = cosmeticDataList.FirstOrDefault(c => c.cosmeticId == newID);
            SetNewCurrentCosmetic(pickedCosmetic);
        }
    }
}