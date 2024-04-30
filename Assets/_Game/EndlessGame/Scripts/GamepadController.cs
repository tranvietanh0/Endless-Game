using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VANH.EndlessGame
{
    public class GamepadController : MonoBehaviour
    {
        public bool isOnMobile;
        public static GamepadController Instance;
        
        private bool m_canJump;

        public bool CanJump
        {
            get => m_canJump;
            set => m_canJump = value;
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (!isOnMobile)
            {
                m_canJump = Input.GetKeyDown(KeyCode.Space);
                
            }
        }
    }
}
