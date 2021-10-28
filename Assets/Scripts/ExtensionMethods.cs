
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace EggNamespace
{

    public static class ExtensionMethods
    {

        public static float GetAngle(Vector2 point1, Vector2 point2)
        {
            return Mathf.Atan2(point2.y - point1.y, point2.x - point1.x) * 180 / Mathf.PI;
        }
        private static System.Random rng = new System.Random();
        public static void ShiftLeft<T>(T[] arr, int shifts)
        {
            var lastVal = arr[arr.Length - 1];
            Array.Copy(arr, shifts, arr, 0, arr.Length - shifts);
            Array.Clear(arr, arr.Length - shifts, shifts);
            arr[0] = lastVal;
        }

        public static void ShiftRight<T>(T[] arr, int shifts)
        {
            var lastVal = arr[arr.Length - 1];
            Array.Copy(arr, 0, arr, shifts, arr.Length - shifts);
            Array.Clear(arr, 0, shifts);
            arr[0] = lastVal;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;

                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static List<T> GetListFromMatrix<T>(T[,] matrix)
        {
            List<T> outputList = new List<T>();
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    outputList.Add(matrix[x, y]);
                }
            }
            return outputList;
        }

        public static void DisposeObjects<T>(List<T> target, float delay = 0) where T : UnityEngine.Object
        {
            for (var i = target.Count - 1; i >= 0; i--)
            {
                T removeTarget = target[i];
                target.Remove(target[i]);
                if (Application.isPlaying)
                {
                    GameObject.Destroy((UnityEngine.Object)removeTarget, delay);
                }
                else
                {
                    Debug.Log("Object was destroyed by DestroyImmediate.");
                    GameObject.DestroyImmediate((UnityEngine.Object)removeTarget);
                }
            }
        }
        public static void DrawCross(Vector3 pos, float size, Color color, float duration = 0)
        {
            Vector3 vector = new Vector3(size, size);
            Debug.DrawLine(pos - vector, pos + vector, color, duration);
            vector = new Vector3(size, -size);
            Debug.DrawLine(pos - vector, pos + vector, color, duration);
        }
        public static Vector2Int CoordinatesOf<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y] != null && matrix[x, y].Equals(value))
                        return new Vector2Int(x, y);
                }
            }

            return new Vector2Int(-1, -1);
        }
        public static T PickRandomFromList<T>(this List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }
        public static T PickRandomFromList<T>(this List<T> list, System.Random random)
        {
            int index = random.Next(0, list.Count);
            return list[index];
        }
        public static float RandomRangeVector(Vector2 vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }
        public static int RandomRangeVector(Vector2Int vector)
        {
            return UnityEngine.Random.Range(vector.x, vector.y);
        }
        public static Vector2 RandomRangeBetweenVectors(Vector2 vectorLower, Vector2 vectorUpper)
        {
            Vector2 output = new Vector2(UnityEngine.Random.Range(vectorLower.x, vectorUpper.x), UnityEngine.Random.Range(vectorLower.y, vectorUpper.y));
            return output;
        }

        public static Vector3 GetPointInCircle(Vector3 center, float radius, float angle)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius + center.x;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius + center.y;
            return new Vector3(x, y, center.z);
        }
        public static T GetClosestObject<T>(List<T> objectToChoose, Vector3 originPos) where T : UnityEngine.Component
        {
            T tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = originPos;
            foreach (T t in objectToChoose)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
        public static bool IsBetween(double testValue, double bound1, double bound2)
        {
            return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
        }
        public static Rect FromEdgesToRect(Vector2 edgeLeftDown, Vector2 edgeRightUp)
        {
            Vector2 size = (Vector2)edgeLeftDown - (Vector2)edgeRightUp;
            Vector2 edge = new Vector2(Mathf.Min(edgeLeftDown.x, edgeRightUp.x), Mathf.Min(edgeLeftDown.y, edgeRightUp.y));
            size = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            return new Rect(edge, size);
        }
        public static bool IsBetween(double testValue, Vector2 range)
        {
            return (testValue >= Math.Min(range.x, range.y) && testValue <= Math.Max(range.x, range.y));
        }

        public static T1 GetClosestPoint<T, T1>(T target, List<T1> other) where T : UnityEngine.Component where T1 : UnityEngine.Component
        {
            T1 tMin = null;
            float minDist = Mathf.Infinity;
            Vector3 currentPos = target.transform.position;
            foreach (T1 t in other)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            return tMin;
        }
        public static (Vector2, Vector2, Vector2, Vector2) GetCornerPointsFromVectors(Vector2 vector1, Vector2 vector2)
        {
            Vector2 lowestVector = new Vector2(Math.Min(vector1.x, vector2.x), Math.Min(vector1.y, vector2.y));
            Vector2 highestVector = new Vector2(Math.Max(vector1.x, vector2.x), Math.Max(vector1.y, vector2.y));
            Vector2 upperLeft = new Vector2(lowestVector.x, highestVector.y);
            Vector2 lowerRight = new Vector2(highestVector.x, lowestVector.y);
            return (upperLeft, highestVector, lowerRight, lowestVector);

        }
        public static void SetGlobalScale(this Transform transform, Vector3 globalScale)
        {
            transform.localScale = Vector3.one;
            transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
        }
        public static Color ChangeAlpha(this Color color, float newAlpha)
        {
            return new Color(color.r, color.g, color.g, newAlpha);
        }
       /* public static T WeightedPickFromList<T>(this List<T> list) where T : IWeightedRandomItem
        {
            float totalWeight = 0;
            list.ForEach(item => totalWeight += item.GetWeight());
            float result = UnityEngine.Random.Range(0, totalWeight);
            foreach (T item in list)
            {
                result -= item.GetWeight();
                if (result <= 0)
                {
                    return item;
                }
            }
            Debug.LogWarning(nameof(WeightedPickFromList) + " failed picking item");
            return list[0];
        }
        public static T WeightedPickFromList<T>(this List<T> list, System.Random random) where T : IWeightedRandomItem
        {
            float totalWeight = 0;
            list.ForEach(item => totalWeight += item.GetWeight());
            float result = random.NextFloat(0, totalWeight);
            foreach (T item in list)
            {
                result -= item.GetWeight();
                if (result <= 0)
                {
                    return item;
                }
            }
            Debug.LogWarning(nameof(WeightedPickFromList) + " failed picking item");
            return list[0];
        }*/
        public static float NextFloat(this System.Random random, float minimum, float maximum)
        {
            return (float)(random.NextDouble() * (maximum - minimum) + minimum);
        }
        public static float SqrDistance(Vector3 position1, Vector3 position2)
        {
            Vector3 offset = position1 - position2;
            return offset.sqrMagnitude;
        }
    }
}
