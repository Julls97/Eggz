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
            List<EggCosmeticData> cosmeticDataList = GlobalCosmetiicManager.Instance.GetDataList();
            foreach (EggCosmeticData cosmeticData in cosmeticDataList)
            {
                UICosmeticCellItem cellItem = Instantiate(cosmeticCellPrefab, scrollableContent);
                cellItem.SetUp(cosmeticData, () => OnSomeItemPicked(cellItem));
                cosmeticOnPanel.Add(cellItem);
            }
        }
        private void OnSomeItemPicked(UICosmeticCellItem item)
        {
            GlobalCosmetiicManager.Instance.SetNewCurrentCosmetic(item.GetCosmeticData());
            Debug.Log("Picked " + item.name);
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
