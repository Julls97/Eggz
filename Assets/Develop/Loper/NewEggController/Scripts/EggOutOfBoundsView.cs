using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame.Egg
{
    public class EggOutOfBoundsView : MonoBehaviour
    {
        [SerializeField] Transform eggView;
        [SerializeField] Transform trackedEgg;
        [SerializeField] Vector2 sizeRange = new Vector2(1, 0.4f);
        [SerializeField] float maxDistance = 5f;
        [SerializeField] Vector2 viewXMoveRange;

        [SerializeField]  bool viewEnabled = false;
        public void EnableView(bool enable)
        {
            viewEnabled = enable;
            this.gameObject.SetActive(enable);
        }

        private void Update()
        {
            if (!viewEnabled)
                return;
            float distance = Vector3.Distance(trackedEgg.position, this.transform.position);
            distance = Mathf.Clamp(distance, 0, maxDistance);
            float distanceLerp = distance / maxDistance;
            eggView.transform.localScale = Vector3.one* Mathf.Lerp(sizeRange.x, sizeRange.y, distanceLerp);
            eggView.transform.localRotation = trackedEgg.localRotation;
            float clampedMove = Mathf.Clamp(trackedEgg.position.x, viewXMoveRange.x, viewXMoveRange.y);
            this.transform.position = new Vector3(clampedMove, this.transform.position.y);

        }
    }
}

