using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ywr.Minions
{
    public class ColorChanger : MonoBehaviour
    {
        [Tooltip("If you don't want this component during the Runtime keep it TRUE")]
        [SerializeField] private bool destroyOnEnable = true;

        [Tooltip("Renderers to be colored")]
        [SerializeField] private SpriteRenderer[] renderers;

        [Tooltip("Color of the renderers")]
        [SerializeField] private Color color = Color.white;

        private void OnEnable()
        {
            if(Application.isPlaying && destroyOnEnable)
                Destroy(this);
        }

        public void SetColors()
        {
            if (renderers == null)
                return;

            if (renderers.Length <= 0)
                return;

            for (int i = 0; i < renderers.Length; i++)
            {
                if(renderers[i] != null)
                    renderers[i].color = color;
            }
        }
    }
}