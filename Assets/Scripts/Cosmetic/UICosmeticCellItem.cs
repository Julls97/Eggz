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
        EggAvailabilityWrapper currentEggCosmeticDataWraped;
        public EggAvailabilityWrapper CurrentEggCosmeticDataWraped => currentEggCosmeticDataWraped;
        private void UpdateAvalability()
        {
            switch (currentEggCosmeticDataWraped.CosmeticAvailability)
            {

                case EggCosmeticAvailability.Unlocked:
                    {
                        background.color = Color.green;
                        break;
                    }
                case EggCosmeticAvailability.Locked:
                    {
                        switch (currentEggCosmeticDataWraped.CosmeticData.unlockRequirement)
                        {
                            case UnlockRequirement.Challange:
                                {
                                    background.color = Color.red;
                                    break;
                                }
                            case UnlockRequirement.TapAction:
                                {
                                    background.color = Color.cyan;
                                    break;
                                }
                            case UnlockRequirement.None:
                                {
                                    background.color = Color.gray;
                                    break;
                                }
                        }
                    }
                    break;
            }
        }

        public void HighlightItem(bool highlight )
        {
            if (highlight)
            {
                background.color = Color.yellow;
            }
            else
            {
                UpdateAvalability();
            }

        }

        public void SetUp(EggAvailabilityWrapper eggCosmeticDataWraped, Action onPickedItemCallback)
        {
            currentEggCosmeticDataWraped = eggCosmeticDataWraped;
            onPickedItem = onPickedItemCallback;
            UpdateAvalability();
            UpdateUI();
        }
        private void ItemPicked()
        {
            onPickedItem?.Invoke();
        }
        private void UpdateUI()
        {
            //    availabilityText.text = currentEggCosmeticData.eggCosmeticAvailability.ToString();
            eggImage.sprite = currentEggCosmeticDataWraped.CosmeticData.previewImage;
            //    eggText.text = currentEggCosmeticData.cosmeticName;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ItemPicked();
        }
    }
}
