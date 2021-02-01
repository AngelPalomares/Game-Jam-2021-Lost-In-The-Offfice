using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Jumper : MonoBehaviour
    {
        [SerializeField] private float jumpHeight;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(UnityTags.PLAYER))
            {
                var con = other.GetComponent<PlayerController>();
                con.velocity = new Vector2(con.velocity.x, jumpHeight);
                AudioManager.instance.PlaySFX(1);
            }
        }
    }
}