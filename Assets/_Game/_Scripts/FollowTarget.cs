using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kwalee
{
    public class FollowTarget : MonoBehaviour
    {

        #region Variables
        [SerializeField] private Transform m_targetToFollow;
        [SerializeField] private Vector3 m_Offset;
        #endregion

        private void Start()
        {
            try
            {
                m_Offset = transform.position - m_targetToFollow.position;
            }
            catch (UnassignedReferenceException)
            {
                Debug.LogError("Target to Follow is not assigned dude");
            }

        }

        private void LateUpdate()
        {
            FollowTheTarget();
        }

        private void FollowTheTarget()
        {
            if (m_targetToFollow == null) return;
            Vector3 newPose = m_targetToFollow.position + m_Offset;
            newPose.y = transform.position.y;
            transform.position = newPose;
        }
    }
}