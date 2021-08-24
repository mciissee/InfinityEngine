using System;
using UnityEngine;

namespace InfinityEngine
{
    public class AutoRotate : MonoBehaviour
    {
        public float speed;
        public bool rotate;
        public Vector3 axis;

        private void Update()
        {
            if (rotate)
                transform.Rotate(axis * speed * Time.deltaTime);
        }
    }
}
