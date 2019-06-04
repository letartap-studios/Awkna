﻿using UnityEngine;
namespace Enemy
{
    public class PatrollingEnemyMovement : MonoBehaviour
    {
        protected Vector3 m_MoveVector;
        protected CharacterController2D m_CharacterController2D;
        public float gravity = 10.0f;
        public float movementSpeed = 3f;
        protected Vector2 m_SpriteForward;
        public bool spriteFaceLeft = false;
        protected SpriteRenderer m_SpriteRenderer;

        private bool frontGroundCheck;
        private bool edgeCheck;
        public Vector2 frontGroundOffset;
        public Vector2 edgeOffset;
        public float radius;
        private EnemyAttack attack;

        public Vector3 moveVector { get { return m_MoveVector; } }

        private void Awake()
        {
            m_CharacterController2D = GetComponent<CharacterController2D>();
            m_SpriteRenderer = GetComponent<SpriteRenderer>();

            m_SpriteForward = spriteFaceLeft ? Vector2.left : Vector2.right;
            if (m_SpriteRenderer.flipX) m_SpriteForward = -m_SpriteForward;
            attack = GetComponent<EnemyAttack>();
        }

        void FixedUpdate()
        {
            frontGroundCheck = Physics2D.OverlapCircle((Vector2)transform.position + frontGroundOffset, radius, m_CharacterController2D.groundedLayerMask);
            edgeCheck = Physics2D.OverlapCircle((Vector2)transform.position + edgeOffset, radius, m_CharacterController2D.groundedLayerMask);

            if ((frontGroundCheck || !edgeCheck) && m_CharacterController2D.IsGrounded)
            {
                Flip();
            }

            m_MoveVector.y = Mathf.Max(m_MoveVector.y - gravity * Time.deltaTime, -gravity);

            m_CharacterController2D.Move(m_MoveVector * Time.deltaTime);

            SetHorizontalSpeed(movementSpeed);
        }

        public void SetHorizontalSpeed(float horizontalSpeed)
        {
            m_MoveVector.x = horizontalSpeed * m_SpriteForward.x;
        }

        private void Flip()
        {
            m_SpriteForward = (m_SpriteForward == Vector2.left) ? Vector2.right : Vector2.left;

            frontGroundOffset.x = -frontGroundOffset.x;
            edgeOffset.x = -edgeOffset.x;
            attack.offset.x = -attack.offset.x;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere((Vector2)transform.position + frontGroundOffset, radius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere((Vector2)transform.position + edgeOffset, radius);
        }
    }
}
