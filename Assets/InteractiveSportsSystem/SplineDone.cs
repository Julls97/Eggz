using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveSportsSystem
{
    public class SplineDone : MonoBehaviour
    {

        private static readonly Vector3 normal2D = new Vector3(0, 0, -1f);

        public event EventHandler OnDirty;

        [SerializeField] private Vector3 normal = new Vector3(0, 0, -1);
        [SerializeField] private bool closedLoop;
        [SerializeField] private List<Anchor> anchorList;

        private float moveDistance;
        private float pointAmountInCurve;
        private float pointAmountPerUnitInCurve = 2f;


        private List<Point> pointList;
        private float splineLength;

        private void Awake()
        {
            splineLength = GetSplineLength();
            SetupPointList();
        }

        private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            var ab = Vector3.Lerp(a, b, t);
            var bc = Vector3.Lerp(b, c, t);

            return Vector3.Lerp(ab, bc, t);
        }

        private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
        {
            var abc = QuadraticLerp(a, b, c, t);
            var bcd = QuadraticLerp(b, c, d, t);

            return Vector3.Lerp(abc, bcd, t);
        }
        class AnchorAndIndex
        {
            public int index;
            public float distance;
        }

        public Vector3 GetAdvPositionAt(float t)
        {
            if (t == 0)
                return GetPositionAt(0);
            if (t >= 1)
                return GetPositionAt(1);
            var units = splineLength * t;

            return GetPositionAtUnits(units);
        }
        public Vector3 GetAdvForwardAt(float t)
        {
            var units = splineLength * t;
            if (t == 0)
                return GetForwardAt(0);
            if (t >= 0.99)
                return GetForwardAt(0.99f);
            return GetForwardAtUnits(units);
        }
        public Vector3 GetPositionAt(float t)
        {
            if (t == 1)
            {
                // Full position, special case
                Anchor anchorA, anchorB;
                if (closedLoop)
                {
                    anchorA = anchorList[anchorList.Count - 1];
                    anchorB = anchorList[0];
                }
                else
                {
                    anchorA = anchorList[anchorList.Count - 2];
                    anchorB = anchorList[anchorList.Count - 1];
                }
                return transform.position + CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, t);
            }
            else
            {
                var addClosedLoop = (closedLoop ? 1 : 0);
                var tFull = t * (anchorList.Count - 1 + addClosedLoop);
                var anchorIndex = Mathf.FloorToInt(tFull);
                var tAnchor = tFull - anchorIndex;
                Anchor anchorA, anchorB;
                if (anchorIndex < anchorList.Count - 1)
                {
                    anchorA = anchorList[anchorIndex + 0];
                    anchorB = anchorList[anchorIndex + 1];
                }
                else
                {
                    // anchorIndex is final one, either don't link to "next" one or loop back
                    if (closedLoop)
                    {
                        anchorA = anchorList[anchorList.Count - 1];
                        anchorB = anchorList[0];
                    }
                    else
                    {
                        anchorA = anchorList[anchorIndex - 1];
                        anchorB = anchorList[anchorIndex + 0];
                        tAnchor = 1f;
                    }
                }

                return transform.position + CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, tAnchor);
            }
        }

        public Vector3 GetForwardAt(float t)
        {
            var pointA = GetPreviousPoint(t);

            int pointBIndex;

            pointBIndex = (pointList.IndexOf(pointA) + 1) % pointList.Count;
            var pointB = pointList[pointBIndex];

            return Vector3.Lerp(pointA.forward, pointB.forward, (t - pointA.t) / Mathf.Abs(pointA.t - pointB.t));
        }

        public Point GetPreviousPoint(float t)
        {
            var previousIndex = 0;
            for (var i = 1; i < pointList.Count; i++)
            {
                var point = pointList[i];
                if (t < point.t)
                {
                    return pointList[previousIndex];
                }
                else
                {
                    previousIndex++;
                }
            }
            return pointList[previousIndex];
        }

        public Point GetClosestPoint(float t)
        {
            var closestPoint = pointList[0];
            foreach (var point in pointList)
            {
                if (Mathf.Abs(t - point.t) < Mathf.Abs(t - closestPoint.t))
                {
                    closestPoint = point;
                }
            }
            return closestPoint;
        }

        public Vector3 GetPositionAtUnits(float unitDistance, float stepSize = .01f)
        {
            var splineUnitDistance = 0f;

            var lastPosition = GetPositionAt(0f);

            var incrementAmount = stepSize;

            for (float t = 0; t < 1f; t += incrementAmount)
            {
                splineUnitDistance += Vector3.Distance(lastPosition, GetPositionAt(t));

                lastPosition = GetPositionAt(t);

                if (splineUnitDistance >= unitDistance)
                {
                    /*
                    float remainingDistance = splineUnitDistance - unitDistance;
                    Debug.Log(remainingDistance + " " + unitDistance + " " + splineUnitDistance + " " + t);
                    Debug.Log(t - (remainingDistance / splineLength));
                    return GetPositionAt(t - (remainingDistance / splineLength));
                    */
                    var direction = (GetPositionAt(t) - GetPositionAt(t - incrementAmount)).normalized;
                    return GetPositionAt(t) + direction * (unitDistance - splineUnitDistance);
                }
            }

            // Default
            var anchorA = anchorList[0];
            var anchorB = anchorList[1];
            return CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, unitDistance / splineLength);
        }

        public Vector3 GetForwardAtUnits(float unitDistance, float stepSize = .01f)
        {
            var splineUnitDistance = 0f;

            var lastPosition = GetPositionAt(0f);

            var incrementAmount = stepSize;
            var lastDistance = 0f;

            for (float t = 0; t < 1f; t += incrementAmount)
            {
                lastDistance = Vector3.Distance(lastPosition, GetPositionAt(t));
                splineUnitDistance += lastDistance;

                lastPosition = GetPositionAt(t);

                if (splineUnitDistance >= unitDistance)
                {
                    var remainingDistance = splineUnitDistance - unitDistance;
                    return GetForwardAt(t - ((remainingDistance / lastDistance) * incrementAmount));
                }

            }

            // Default
            var anchorA = anchorList[0];
            var anchorB = anchorList[1];
            return CubicLerp(anchorA.position, anchorA.handleBPosition, anchorB.handleAPosition, anchorB.position, unitDistance / splineLength);
        }

        private void SetupPointList()
        {
            pointList = new List<Point>();
            pointAmountInCurve = pointAmountPerUnitInCurve * splineLength;
            for (float t = 0; t < 1f; t += 1f / pointAmountInCurve)
            {
                pointList.Add(new Point
                {
                    t = t,
                    position = GetPositionAt(t),
                    normal = normal,
                });
            }

            pointList.Add(new Point
            {
                t = 1f,
                position = GetPositionAt(1f),
            });

            UpdateForwardVectors();
        }

        private void UpdatePointList()
        {
            if (pointList == null) return;

            foreach (var point in pointList)
            {
                point.position = GetPositionAt(point.t);
            }

            UpdateForwardVectors();
        }

        private void UpdateForwardVectors()
        {
            if (pointList.Count == 0)
            {
                Debug.Log("Point list was empty for some reason. Redoing setup...");
                SetupPointList();
            }
            // Set forward vectors
            for (var i = 0; i < pointList.Count - 1; i++)
            {
                pointList[i].forward = (pointList[i + 1].position - pointList[i].position).normalized;
            }
            // Set final forward vector
            if (closedLoop)
            {
                pointList[pointList.Count - 1].forward = pointList[0].forward;
            }
            else
            {
                pointList[pointList.Count - 1].forward = pointList[pointList.Count - 2].forward;
            }
        }



        public float GetSplineLength(float stepSize = .01f)
        {
            var splineLength = 0f;
            var lastPosition = GetPositionAt(0f);

            for (float t = 0; t < 1f; t += stepSize)
            {
                splineLength += Vector3.Distance(lastPosition, GetPositionAt(t));
                lastPosition = GetPositionAt(t);
            }

            splineLength += Vector3.Distance(lastPosition, GetPositionAt(1f));

            return splineLength;
        }

        public List<Anchor> GetAnchorList()
        {
            return anchorList;
        }

        public void AddAnchor()
        {
            if (anchorList == null || anchorList.Count <= 0)
            {
                anchorList = new List<Anchor>();
                anchorList.Add(new Anchor
                {
                    position = Vector3.zero + new Vector3(1, 1, 0),
                    handleAPosition = Vector3.zero + new Vector3(1, 1, 0),
                    handleBPosition = Vector3.zero + new Vector3(1, 1, 0),
                });
            }

            var lastAnchor = anchorList[anchorList.Count - 1];
            anchorList.Add(new Anchor
            {
                position = lastAnchor.position + new Vector3(1, 1, 0),
                handleAPosition = lastAnchor.handleAPosition + new Vector3(1, 1, 0),
                handleBPosition = lastAnchor.handleBPosition + new Vector3(1, 1, 0),
            });
        }

        public void RemoveLastAnchor()
        {
            if (anchorList == null) anchorList = new List<Anchor>();

            anchorList.RemoveAt(anchorList.Count - 1);
        }


        public List<Point> GetPointList()
        {
            return pointList;
        }

        public bool GetClosedLoop()
        {
            return closedLoop;
        }

        public void SetAllZZero()
        {
            foreach (var anchor in anchorList)
            {
                anchor.position = new Vector3(anchor.position.x, anchor.position.y, 0f);
                anchor.handleAPosition = new Vector3(anchor.handleAPosition.x, anchor.handleAPosition.y, 0f);
                anchor.handleBPosition = new Vector3(anchor.handleBPosition.x, anchor.handleBPosition.y, 0f);
            }
        }

        public void SetAllYZero()
        {
            foreach (var anchor in anchorList)
            {
                anchor.position = new Vector3(anchor.position.x, 0f, anchor.position.z);
                anchor.handleAPosition = new Vector3(anchor.handleAPosition.x, 0f, anchor.handleAPosition.z);
                anchor.handleBPosition = new Vector3(anchor.handleBPosition.x, 0f, anchor.handleBPosition.z);
            }
        }
        public void FlipPoints()
        {
            foreach (var anchor in anchorList)
            {
                anchor.handleAPosition = new Vector3(-anchor.handleAPosition.x, anchor.handleAPosition.y, anchor.handleAPosition.z);
                anchor.handleBPosition = new Vector3(-anchor.handleBPosition.x, anchor.handleBPosition.y, anchor.handleBPosition.z);
                anchor.position = new Vector3(-anchor.position.x, anchor.position.y, anchor.position.z);

            }
        }
        public void SetDirty()
        {
            splineLength = GetSplineLength();

            UpdatePointList();

            OnDirty?.Invoke(this, EventArgs.Empty);
        }


        [Serializable]
        public class Point
        {
            public float t;
            public Vector3 position;
            public Vector3 forward;
            public Vector3 normal;
        }

        [Serializable]
        public class Anchor
        {
            public Vector3 position;
            public Vector3 handleAPosition;
            public Vector3 handleBPosition;
        }

    }
}
