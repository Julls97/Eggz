using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EggNamespace.Cosmetic
{
    public class UICosmeticCellItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] Image eggImage;
    //    [SerializeField] TextMeshProUGUI eggText;
        [SerializeField] TextMeshProUGUI availabilityText;
        [SerializeField] Image background;
        Action onPickedItem;
        EggCosmeticData currentEggCosmeticData;
        public EggCosmeticData GetCosmeticData() => currentEggCosmeticData;
        public void OnPointerDown(PointerEventData eventData)
        {
            ItemPicked();
        }
        public void SetAvalability(EggCosmeticAvailability cosmeticAvailability)
        {
            switch (cosmeticAvailability)
            {
                case EggCosmeticAvailability.Current:
                    {
                        background.color = Color.green;
                        break;
                    }
                case EggCosmeticAvailability.Avalable:
                    {
                        background.color = Color.yellow;
                        break;
                    }
                case EggCosmeticAvailability.CanBeUnlocked:
                    {
                        background.color = Color.cyan;
                        break;
                    }
                case EggCosmeticAvailability.Locked:
                    {
                        background.color = Color.gray;
                        break;
                    }
            }
        }

        public void SetUp(EggCosmeticData eggCosmeticData, Action onPickedItemCallback)
        {
            currentEggCosmeticData = eggCosmeticData;
            onPickedItem = onPickedItemCallback;
          
            UpdateUI();
        }
        private void ItemPicked()
        {
            onPickedItem?.Invoke();
        }
        private void UpdateUI()
        {
        //    availabilityText.text = currentEggCosmeticData.eggCosmeticAvailability.ToString();
            eggImage.sprite = currentEggCosmeticData.previewImage;
        //    eggText.text = currentEggCosmeticData.cosmeticName;
        }

    }
}
