using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    private IEnumerable<(MonoBehaviour mono, IPlayerReactive reactive)> _reactives;

    void Start()
    {
        _reactives = FindObjectsOfType<MonoBehaviour>().Where(m => m is IPlayerReactive).Select(m => (m, (IPlayerReactive) m));
    }

    void Update()
    {
        foreach (var (mono, reactive) in _reactives)
        {
            if (Vector3.Distance(mono.transform.position, transform.position) < 2f)
            {
                reactive.React(this);
            }
        }
        
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Time.deltaTime * Speed * Vector3.up);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Time.deltaTime * Speed * Vector3.left);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Time.deltaTime * Speed * Vector3.down);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Time.deltaTime * Speed * Vector3.right);
    }
}
