using InfinityEngine.Attributes;
using System;
using UnityEngine;

namespace InfinityEngine.Utils
{
    /// <summary>
    /// Makes the image of the 'SpriteRenderer' component attached to the GameObject of the script always fill the screen.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(SpriteRenderer))]
    [OverrideInspector]
    public class AutoStretchSprite : MonoBehaviour
    {
        [InspectorButton("Resize", InspectorButtonLocations.Bottom)]
        public bool ExecuteOnUpdate = true;

        private SpriteRenderer spriteRenderer;

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
            spriteRenderer = this.GetComponent<SpriteRenderer>();
            Resize();
        }

        private void FixedUpdate()
        {
            if (ExecuteOnUpdate)
            {
                Resize();
            }
        }

        private void Resize()
        {
            if (!(spriteRenderer == null))
            {
                this.transform.localScale = new Vector3(1f, 1f, 1f);
                Bounds bounds = spriteRenderer.sprite.bounds;
                float x = bounds.size.x;
                bounds = spriteRenderer.sprite.bounds;
                float y = bounds.size.y;
                float num = mainCamera.orthographicSize * 2f;
                float num2 = num / (float)Screen.height * (float)Screen.width;
                Vector3 localScale = this.transform.localScale;
                localScale.x = num2 / x;
                this.transform.localScale = localScale;
                Vector3 localScale2 = this.transform.localScale;
                localScale2.y = num / y;
                this.transform.localScale = localScale2;
            }
        }

        public AutoStretchSprite()
        {
        }
    }
}
