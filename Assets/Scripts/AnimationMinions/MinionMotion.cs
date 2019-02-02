using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ywr.Minions
{
    public class MinionMotion : MonoBehaviour
    {
        [Header("Motion References")]
        [SerializeField] protected Transform[] EyesTransforms;
        private Vector3[] StartEyesPositions;
        [SerializeField] private Vector3 offSetYEyesUpDown;

        public void Awake()
        {
            StartEyesPositions = new Vector3[EyesTransforms.Length];
            for (int i = 0; i < EyesTransforms.Length; i++)
            {
                StartEyesPositions[i] = EyesTransforms[i].localPosition;
            }
        }

        public void Reset()
        {
            for (int i = 0; i < EyesTransforms.Length; i++)
            {
                EyesTransforms[i].localPosition = StartEyesPositions[i];
            }
        }

        public void GoDownEyesAnimation()
        {
            for (int i = 0; i < EyesTransforms.Length; i++)
            {
                EyesTransforms[i].localPosition -= offSetYEyesUpDown;
            }
        }

        public void GoUpEyesAnimation()
        {
            for (int i = 0; i < EyesTransforms.Length; i++)
            {
                EyesTransforms[i].localPosition += offSetYEyesUpDown;
            }
        }
    }
}