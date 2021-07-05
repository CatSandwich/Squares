using System.Collections;
using UnityEngine;

namespace Entities
{
    public class ScaredEntity : EntityBase, IPlayerReactive
    {
        public float ScaredSpeed;
        public float ScaredTransitionTime;
        public Color ScaredColor;
        public SpriteRenderer SR;
        
        private bool _isReacting;
        
        private float _reactionStartTime;
        private Color _reactionStartColor;
        
        public void Update()
        {
            // Set the color
            if (!_isReacting) return;
            SR.color = Color.Lerp(
                _reactionStartColor, 
                ScaredColor, 
                (Time.time - _reactionStartTime) / ScaredTransitionTime);
        }
        
        public void React(PlayerController player)
        {
            if (_isReacting) return;
            _isReacting = true;
            _reactionStartTime = Time.time;
            _reactionStartColor = SR.color;
            StartCoroutine(_run(player));
        }

        private IEnumerator _run(PlayerController player)
        {
            yield return new WaitForSeconds(ScaredTransitionTime);
            RB.velocity += (Vector2) (
                    transform.position - 
                    player.transform.position).normalized * ScaredSpeed;
        }
    }
}
