using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using EggNamespace.Serialization;
namespace EggNamespace.Cosmetic
{
    [System.Serializable]
    public class EggAvailabilityWrapper
    {
        private bool isCurrentItem = false;
        public bool IsCurrentItem { get => isCurrentItem; set => isCurrentItem = value; }
        [SerializeField] private string cosmeticDataID;
        public string CosmeticDataID { get => cosmeticDataID; set => cosmeticDataID = value; }
        public EggCosmeticData CosmeticData { get; set; }

        [SerializeField] private EggCosmeticAvailability cosmeticAvailability;
        public EggCosmeticAvailability CosmeticAvailability { get => cosmeticAvailability; set => cosmeticAvailability = value; }

    }
    [System.Serializable]
    public class EggCosmeticSerizlizedData
    {
        [SerializeField] private List<EggAvailabilityWrapper> eggAvailabilityWrapperList = new List<EggAvailabilityWrapper>();
        public List<EggAvailabilityWrapper> EggAvailabilityWrapperList { get => eggAvailabilityWrapperList; set => eggAvailabilityWrapperList = value; }

        [SerializeField] private int pickedEggCosmeticID = 0;
        public int PickedEggCosmeticID { get => pickedEggCosmeticID; set => pickedEggCosmeticID = value; }

    }
    public class CosmeticSerializationController : MonoBehaviour
    {
        private EggCosmeticSerizlizedData eggCosmeticSerizlizedData;


        private string cosmeticFileName = "CosmeticData";
        public bool LoadCosmeticData()
        {
            eggCosmeticSerizlizedData = DataSerializer.DeserializeData<EggCosmeticSerizlizedData>(cosmeticFileName);
            return eggCosmeticSerizlizedData != null;
        }
        public EggCosmeticSerizlizedData GetCosmeticSerizlizedData()
        {
            return eggCosmeticSerizlizedData;
        }

        public void SaveCosmeticData(EggCosmeticSerizlizedData eggCosmeticSerizlizedData)
        {
            this.eggCosmeticSerizlizedData = eggCosmeticSerizlizedData;
            DataSerializer.SerializeData(this.eggCosmeticSerizlizedData, cosmeticFileName);
        }
        public void SaveCosmeticData()
        {
            DataSerializer.SerializeData(eggCosmeticSerizlizedData, cosmeticFileName);
        }

        public static CosmeticSerializationController Instance;
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
    }
}