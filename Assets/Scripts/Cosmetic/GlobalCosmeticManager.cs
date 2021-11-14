using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EggNamespace.Cosmetic
{

    public class GlobalCosmeticManager : MonoBehaviour
    {
        [SerializeField] private List<EggCosmeticData> cosmeticDataList = new List<EggCosmeticData>();
        [SerializeField] private EggCosmeticSerizlizedData cosmeticSerizlizedData;
        private List<EggAvailabilityWrapper> eggAvailabilityWrapperList => cosmeticSerizlizedData.EggAvailabilityWrapperList;
        [SerializeField] private CosmeticSerializationController cosmeticSerialization;
        private static EggAvailabilityWrapper currentCosmetic;
        public EggAvailabilityWrapper CurrentCosmetic => currentCosmetic;
        public static GlobalCosmeticManager Instance;
        private void FirstLaunchCheck()
        {
            if (cosmeticSerialization.LoadCosmeticData() == false)
            {
                Debug.Log("First launch. Creating cosmetic save");
                SaveData();
            }
        }
        private void LoadData()
        {
            eggAvailabilityWrapperList.Clear();
            cosmeticSerialization.LoadCosmeticData();
            cosmeticSerizlizedData = cosmeticSerialization.GetCosmeticSerizlizedData();
            foreach (EggAvailabilityWrapper eggDataWraped in eggAvailabilityWrapperList)
            {
                EggCosmeticData cosmeticData = cosmeticDataList.FirstOrDefault(cd => cd.cosmeticId == eggDataWraped.CosmeticDataID);
                if (cosmeticData != default)
                {
                    eggDataWraped.CosmeticData = cosmeticData;
                }
                else
                {
                    Debug.LogWarning("Can't find cosmetic data with ID=" + eggDataWraped.CosmeticDataID);
                }
            }
            SetNewCurrentCosmetic(cosmeticSerizlizedData.PickedEggCosmeticID);
        }
        public void SaveData()
        {
            cosmeticSerialization.SaveCosmeticData(cosmeticSerizlizedData);
        }

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
            FirstLaunchCheck();
            LoadData();
        }

        public List<EggAvailabilityWrapper> GetDataList()
        {
            return eggAvailabilityWrapperList;
        }
        public bool SetNewCurrentCosmetic(EggAvailabilityWrapper newCosmetic)
        {

            if (newCosmetic != null && newCosmetic.CosmeticAvailability == EggCosmeticAvailability.Unlocked)
            {

                if (currentCosmetic != null)
                    currentCosmetic.IsCurrentItem = false;
                currentCosmetic = newCosmetic;
                currentCosmetic.IsCurrentItem = true;
                cosmeticSerizlizedData.PickedEggCosmeticID = cosmeticSerizlizedData.EggAvailabilityWrapperList.IndexOf(currentCosmetic);
                return true;
            }
            else
                Debug.LogWarning("New Cosmetic property was null or cantBePicked");
            return false;

        }
        public bool SetNewCurrentCosmetic(int index)
        {
            return SetNewCurrentCosmetic(eggAvailabilityWrapperList[index]);
        }
        public bool SetNewCurrentCosmetic(string newID)
        {
            EggAvailabilityWrapper pickedCosmetic = eggAvailabilityWrapperList.FirstOrDefault(c => c.CosmeticDataID == newID);
            return SetNewCurrentCosmetic(pickedCosmetic);
        }

    }
}