using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VANH.EndlessGame
{
    public class Player : MonoBehaviour, IComponentChecking
    {
        public float jumpForce;
        public LayerMask blockLayer;
        public float blockCheckingRadius;
        public float blockCheckingOffset;
        public GameObject landVfx;

        private Rigidbody2D m_rb;
        private Animator m_anim;
        private Vector3 m_centerPos;
        private int m_blockId;
        private bool m_isDead;

        
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_anim = GetComponent<Animator>();
        }
        public bool IsComponentsNull()
        {
            return m_rb == null || m_anim == null; 
        }

        private bool IsOnBlock()
        {
            m_centerPos = new Vector3(transform.position.x, transform.position.y - blockCheckingOffset, transform.position.z);
            Collider2D col = Physics2D.OverlapCircle(m_centerPos, blockCheckingRadius, blockLayer);
            return col != null;
        }

        private void OnDrawGizmos()
        {
            m_centerPos = new Vector3(transform.position.x, transform.position.y - blockCheckingOffset, transform.position.z);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_centerPos, blockCheckingRadius);
        }
    }
}