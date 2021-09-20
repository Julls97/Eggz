using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
namespace MainGame.Egg
{
    public class OutOfBoundsViewManager : MonoBehaviour
    {
        public static OutOfBoundsViewManager instance;
        [SerializeField] List<EggOutOfBoundsView> activeOutofboundsView;
        [SerializeField] EggOutOfBoundsView prefabView;
        [SerializeField] float yPosition = 0;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }
        public EggOutOfBoundsView GetViewObj()
        {
            EggOutOfBoundsView eggView = Instantiate(prefabView, new Vector3(0, yPosition), Quaternion.identity, this.transform);
            activeOutofboundsView.Add(eggView);

            return eggView;
        }
        public void DisposeView(EggOutOfBoundsView eggView)
        {
            activeOutofboundsView.Remove(eggView);
            Destroy(eggView.gameObject);
        }
        /* 
         *    [SerializeField] List<EggOutOfBoundsView> disabledOutofboundsView;
         * public void DisableView(Transform target)
         {
             EggOutOfBoundsView targetView = activeOutofboundsView.FirstOrDefault(view => view.TrackedEgg == target);
             if (targetView != default)
             {
                 targetView.TrackedEgg = null;
                 activeOutofboundsView.Remove(targetView);
                 targetView.gameObject.SetActive(false);
                 disabledOutofboundsView.Add(targetView);
             }
             else
             {
                 Debug.LogWarning("Out Of Bounds View with target " + target.gameObject.name + " does not exist");
             }

         }
         public void EnableView(Transform target)
         {
             EggOutOfBoundsView targetView;
             if (disabledOutofboundsView.Count > 0)
             {
                 targetView = disabledOutofboundsView[0];
                 disabledOutofboundsView.Remove(targetView);
             }
             else
             {
                 targetView = Instantiate(prefabView, new Vector3(0, yPosition), Quaternion.identity, this.transform);
             }
             targetView.TrackedEgg = target;
             targetView.gameObject.SetActive(true);
             activeOutofboundsView.Add(targetView);
         }*/
    }
}