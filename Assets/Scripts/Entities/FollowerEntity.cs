using UnityEngine;

namespace Entities
{
    public class FollowerEntity : EntityBase, IPlayerReactive
    {
        private bool _isFollowing;
        
        private void _follow(Transform target)
        {
            transform.Translate((target.position - transform.position) * 0.01f);
        }

        public void React(PlayerController player)
        {
            // Only react once to player
            if (_isFollowing) return;
            _isFollowing = true;
            
            // Remove sway
            _sway = false;
            RB.velocity = Vector2.zero;

            // Make local copy of reference
            var following = player.LastFollower;
            player.PlayerMove += _ => _follow(following);
            
            // Sort in layer order - visual
            GetComponent<SpriteRenderer>().sortingOrder = --player.LastOrder;
            
            // Make next follower follow this one
            player.LastFollower = transform;
        }
    }
}
