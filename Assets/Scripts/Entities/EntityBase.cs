using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class EntityBase : MonoBehaviour
    {
        protected bool _sway = true;
        public Rigidbody2D RB;

        void Start()
        {
            RB = GetComponent<Rigidbody2D>();
        }
    
        // Update is called once per frame
        void FixedUpdate()
        {
            if(_sway) RB.velocity += new Vector2(Mathf.Cos(Time.time) * 0.01f, 0f);
        }
    }
}
