using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ywr.Minions
{
    public class AbominationMotion : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Animator myAnimator;

        [Header("Anchors")]
        [SerializeField] private Transform anchorEyesUpper;
        [SerializeField] private Transform anchorEyesBottom;

        [Header("Offset Eyes Movement")]
        [SerializeField] private Vector2 offsetUpDownHeadFirstAnimation;
        [SerializeField] private Vector2 offsetUpDownHeadSecondAnimation;
        [SerializeField] private Vector2 offsetUpJawLeftAnimation;
        [SerializeField] private Vector2 offsetUpJawRightAnimation;

        private int idleCyclesCount = 0;
        private const string animationName = "idleCycles";


        #region Motion, all called by the Animator class

        public void MoveJawLeft()
        {
            anchorEyesBottom.localPosition = offsetUpJawLeftAnimation;
        }

        public void MoveJawRight()
        {
            anchorEyesBottom.localPosition = offsetUpJawRightAnimation;
        }

        public void MoveJawCenter()
        {
            anchorEyesBottom.localPosition = Vector3.zero;
        }

        public void MoveHeadUpFirstHeight()
        {
            anchorEyesUpper.localPosition = offsetUpDownHeadFirstAnimation;
            anchorEyesBottom.localPosition = Vector3.zero;
        }

        public void MoveHeadUpSecondHeight()
        {
            anchorEyesUpper.localPosition = offsetUpDownHeadSecondAnimation;
        }

        public void MoveHeadCenter()
        {
            idleCyclesCount++;
            anchorEyesUpper.localPosition = Vector3.zero;
            myAnimator.SetInteger(animationName, idleCyclesCount);

            if (idleCyclesCount > 5)
                idleCyclesCount = 0;
        }
        
        #endregion
    }
}