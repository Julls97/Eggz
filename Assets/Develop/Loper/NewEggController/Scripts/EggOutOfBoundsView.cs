using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MainGame.Egg
{
    public class EggOutOfBoundsView : MonoBehaviour
    {
        [SerializeField] Transform eggView;
        [SerializeField] Vector2 sizeRange = new Vector2(1, 0.4f);
        [SerializeField] float maxDistance = 5f;
        [SerializeField] Vector2 viewXMoveRange;
        public Transform TrackedEgg { get; set; }
        private void Update()
        {
            float distance = Vector3.Distance(TrackedEgg.position, this.transform.position);
            distance = Mathf.Clamp(distance, 0, maxDistance);
            float distanceLerp = distance / maxDistance;
            eggView.transform.localScale = Vector3.one* Mathf.Lerp(sizeRange.x, sizeRange.y, distanceLerp);
            eggView.transform.localRotation = TrackedEgg.localRotation;
            float clampedMove = Mathf.Clamp(TrackedEgg.position.x, viewXMoveRange.x, viewXMoveRange.y);
            this.transform.position = new Vector3(clampedMove, this.transform.position.y);
        }
    }
}

