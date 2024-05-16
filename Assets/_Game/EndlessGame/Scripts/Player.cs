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
        private Vector2 m_centerPos;
        private bool m_isOnBlock;
        private int m_blockId;
        private bool m_isDead;

        
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (m_isDead || IsComponentsNull())
            {
                return;
            }
            Jump();
            if (m_rb.velocity.y < 0)
            {
                if (m_isOnBlock)
                {
                    m_anim.SetBool(CharacterAnim.Jump.ToString(), false);
                    m_anim.SetBool(CharacterAnim.Land.ToString(), true);
                }
                else
                {
                    m_anim.SetBool(CharacterAnim.Jump.ToString(), false);
                }
            }
        }

        private void FixedUpdate()
        {
            IsOnBlock();
        }

        public bool IsComponentsNull()
        {
            return m_rb == null || m_anim == null; 
        }

        private void IsOnBlock()
        {
            m_centerPos = new Vector3(transform.position.x, transform.position.y - blockCheckingOffset, transform.position.z);
            Collider2D col = Physics2D.OverlapCircle(m_centerPos, blockCheckingRadius, blockLayer);
            m_isOnBlock = col != null ? true : false;
        }

        public void Jump()
        {
            if (!GamepadController.Instance.CanJump || !m_isOnBlock || IsComponentsNull())
            {
                return;
            }

            GamepadController.Instance.CanJump = false;
            m_rb.velocity = Vector2.up * jumpForce;
            m_anim.SetBool(CharacterAnim.Jump.ToString(), true);
            m_anim.SetBool(CharacterAnim.Land.ToString(), false);
        }

        public void BackToIdle()
        {
            m_anim.SetBool(CharacterAnim.Land.ToString(), false);
            m_anim.SetTrigger(CharacterAnim.Idle.ToString());
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(GameTag.Block.ToString()))
            {
                Block block = other.gameObject.GetComponent<Block>();
                if (block != null)
                {
                    block.PlayerLand();
                }
                Debug.Log("Block");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameTag.DeadZone.ToString()))
            {
                Debug.Log("Deadzone");
            }
        }

        private void OnDrawGizmos()
        {
            m_centerPos = new Vector3(transform.position.x, transform.position.y - blockCheckingOffset, transform.position.z);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(m_centerPos, blockCheckingRadius);
        }
    }
}