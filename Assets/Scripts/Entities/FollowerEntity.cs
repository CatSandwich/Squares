using UnityEngine;

namespace Entities
{
    public class FollowerEntity : EntityBase, IPlayerReactive
    {
        private bool _isFollowing;
        private PlayerController _player;

        // Update is called once per frame
        void Update()
        {
            if (_isFollowing)
            {
                transform.Translate((_player.transform.position - transform.position) * 0.01f);
            }
        }

        public void React(PlayerController player)
        {
            _isFollowing = true;
            _player = player;
            _sway = false;
        }
    }
}
