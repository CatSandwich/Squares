using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    
    [NonSerialized]
    public Transform LastFollower;
    [NonSerialized]
    public int LastOrder;
    
    private IEnumerable<(MonoBehaviour mono, IPlayerReactive reactive)> _reactives;

    public event Action<PlayerController> PlayerMove = pc => { };
    
    void Start()
    {
        LastFollower = transform;
        LastOrder = GetComponent<SpriteRenderer>().sortingOrder;
        _reactives = FindObjectsOfType<MonoBehaviour>().Where(m => m is IPlayerReactive).Select(m => (m, (IPlayerReactive) m));
    }

    void Update()
    {
        var delta = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) delta += Vector2.up;
        if (Input.GetKey(KeyCode.A)) delta += Vector2.left;
        if (Input.GetKey(KeyCode.S)) delta += Vector2.down;
        if (Input.GetKey(KeyCode.D)) delta += Vector2.right;
        delta *= Speed;

        if (delta != Vector2.zero)
        {
            transform.Translate(delta * Time.deltaTime);
            PlayerMove(this);
        }
        
        foreach (var (mono, reactive) in _reactives)
        {
            if (Vector3.Distance(mono.transform.position, transform.position) < 2f)
            {
                reactive.React(this);
            }
        }
    }
}
