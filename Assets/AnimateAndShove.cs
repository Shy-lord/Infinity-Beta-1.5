using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateAndShove : MonoBehaviour
{
    [SerializeField] Sprite[] sprite;
    [SerializeField] float animationSpeed;
    SpriteRenderer sr;
    int frame;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>(); 
        frame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (time > animationSpeed)
        {
            time = 0;
            frame = (frame+1)%sprite.Length;
            sr.sprite = sprite[frame];
        }
        time += Time.deltaTime;
    }
}
