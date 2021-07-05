using UnityEngine;

namespace Entities
{
    public class ScaredEntity : EntityBase, IPlayerReactive
    {
        public float ScaredSpeed;
        private bool _isReacting;
        public void React(PlayerController player)
        {
            if (_isReacting) return;
            _isReacting = true;
            RB.velocity += (Vector2)(transform.position - player.transform.position).normalized * ScaredSpeed;
        }
    }
}
