using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace EggNamespace.Cosmetic
{
    public class EggCosmeticController : MonoBehaviour
    {
        private List<UICosmeticCellItem> cosmeticOnPanel = new List<UICosmeticCellItem>();
        [SerializeField] private UICosmeticCellItem cosmeticCellPrefab;

        [SerializeField] private Transform scrollableContent;
        public void InitializeCosmetic()
        {
            ClearCosmeticOnPanel();
            List<EggAvailabilityWrapper> cosmeticWrapedDataList = GlobalCosmeticManager.Instance.GetDataList();
            foreach (EggAvailabilityWrapper cosmeticDataWraped in cosmeticWrapedDataList)
            {
                UICosmeticCellItem cellItem = Instantiate(cosmeticCellPrefab, scrollableContent);
                cellItem.SetUp(cosmeticDataWraped, () => OnSomeItemPicked(cosmeticDataWraped));
                cosmeticOnPanel.Add(cellItem);
            }
            HighlightItem(GlobalCosmeticManager.Instance.CurrentCosmetic);
        }
        private void HighlightItem(EggAvailabilityWrapper item)
        {
            cosmeticOnPanel.ForEach(i => i.HighlightItem(i.CurrentEggCosmeticDataWraped == item));
        }
        private void OnSomeItemPicked(EggAvailabilityWrapper item)
        {
            if (GlobalCosmeticManager.Instance.SetNewCurrentCosmetic(item))
            {
                HighlightItem(item);
                Debug.Log("Picked " + item.CosmeticDataID);
            }

        }
        private void ClearCosmeticOnPanel()
        {
            ExtensionMethods.DisposeObjects(cosmeticOnPanel.Select(g => g.gameObject).ToList());
            cosmeticOnPanel.Clear();
        }
        private void Start()
        {
            InitializeCosmetic();
        }

    }
}
