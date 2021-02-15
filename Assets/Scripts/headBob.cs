using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headBob : MonoBehaviour {
    
    [SerializeField] private float            speed;
    [SerializeField] private float            amount;
    [SerializeField] private PlayerController controller;

    private float defaultY;
    private float timer;
    
    
    void Start() {
        defaultY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(controller.Hor) > 0.1f || Mathf.Abs(controller.Ver) > 0.1f)
        {
            timer += Time.deltaTime * speed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultY + Mathf.Sin(timer) * amount, transform.localPosition.z);
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultY, Time.deltaTime * speed), transform.localPosition.z);
        }
    }
}
