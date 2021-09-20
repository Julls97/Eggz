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
        [SerializeField] Vector2 endPoint = Vector2.zero;
        [SerializeField] float gravityScale = 0.5f;
        [SerializeField] EggOutOfBoundsView outOfBoundsView;
        [SerializeField] Vector2 addRotationRange = new Vector2(-30, 30);
        public bool AllowTouch { get; set; } = true;
        UnityAction<Collider2D> onTriggerEnter;
        UnityAction<Collider2D> onTriggerExit;

        private void OnBecameInvisible()
        {
            outOfBoundsView.gameObject.SetActive(true);
        }
        private void OnBecameVisible()
        {
            outOfBoundsView.gameObject.SetActive(false);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            onTriggerEnter.Invoke(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            onTriggerExit.Invoke(collision);
        }
        private void OnDestroy()
        {
            OutOfBoundsViewManager.instance.DisposeView(outOfBoundsView);
        }
        bool locked = true;
        private void OnMouseDown()
        {
            if (AllowTouch)
            {
                AllowTouch = false;
                UnlockEgg();
                ResetForce();
                Push();
                Debug.Log("Pressed");
            }
        }
        public void SetUp(UnityAction<Collider2D> onTriggerEnter, UnityAction<Collider2D> onTriggerExit)
        {
            this.onTriggerEnter = onTriggerEnter;
            this.onTriggerExit = onTriggerExit;
            outOfBoundsView = OutOfBoundsViewManager.instance.GetViewObj();
            outOfBoundsView.TrackedEgg = this.transform;
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
            Vector2 totalForce = new Vector2(force.x * forceMult, forceMult);
            float rotation = Utility.RandomRangeVector(randomForceAngle);
            eggRigitbody.angularVelocity += rotation;
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
