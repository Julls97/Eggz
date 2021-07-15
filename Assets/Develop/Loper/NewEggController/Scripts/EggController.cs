using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MainGame.Egg
{
    public class EggController : MonoBehaviour
    {
        [SerializeField] Rigidbody2D eggRigitbody;
        [SerializeField] float forceMult = 10;
        [SerializeField] Vector2 randomForceAngle = new Vector2(-30, 30);
        [SerializeField] Vector2 endPoint =Vector2.zero;
        [SerializeField] float gravityScale = 0.5f;
        [SerializeField] EggOutOfBoundsView outofBoundsView;
        public UnityAction OnEggAppearOnSceen { get; set; }
        public UnityAction OnEggDisappearOnSceen { get; set; }

        private void OnBecameInvisible()
        {
            outofBoundsView.EnableView(true);
        }
        private void OnBecameVisible()
        {
            outofBoundsView.EnableView(false);
        }
        bool locked = true;
        private void OnMouseDown()
        {
            UnlockEgg();
            ResetForce();
            Push();
            Debug.Log("Pressed");
        }
        public void UnlockEgg()
        {
            if (!locked)
                return;
            eggRigitbody.gravityScale = gravityScale;
            locked = false;
        }
        public void ResetForce()
        {
            eggRigitbody.velocity = Vector2.zero;
        }
        public void Push()
        {
            Vector2 force = Utility.GetPointInCircle(Vector2.zero, 1, Utility.RandomRangeVector(randomForceAngle));
            Debug.Log(force);
            Vector2 totalForce = new Vector2(force.x * forceMult,  forceMult);
            eggRigitbody.AddForce(totalForce);
        }
        private void Update()
        {
            endPoint = eggRigitbody.velocity;
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(this.transform.position, this.transform.position + (Vector3)(endPoint * forceMult));
        }
    }
}
