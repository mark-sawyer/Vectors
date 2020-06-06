using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Vector : MonoBehaviour {
    public SpriteRenderer sprite;
    private Observer observer;
    private Complex input;
    private Complex output;
    private Color defaultColour;
    private float MAX_MAGNITUDE = 3;

    void Start() {
        observer = GameObject.Find("observer").GetComponent<Observer>();
        input = new Complex(transform.position.x, transform.position.y);
        defaultColour = new Color(0.9451f, 0.7373f, 0.2f);
    }

    void Update() {
        sprite.color = defaultColour;
        output = input - observer.zeroes[0];
        for (int i = 1; i < observer.zeroes.Length; i++) {
            output *= input - observer.zeroes[i];
        }
        output *= 0.001;
        if (output.Magnitude < MAX_MAGNITUDE) {
            transform.localScale = new UnityEngine.Vector3((float)output.Magnitude, (float)output.Magnitude * 0.5f, 1f);
        }
        else {
            transform.localScale = new UnityEngine.Vector3(MAX_MAGNITUDE, MAX_MAGNITUDE / 2, 1f);
            float difference = (float)output.Magnitude - MAX_MAGNITUDE;
            sprite.color += new Color(0.01f * difference, -0.01f * difference, -0.01f * difference);
        }
        transform.eulerAngles = new UnityEngine.Vector3(0, 0, (float)output.Phase * Mathf.Rad2Deg);
        transform.rotation *= UnityEngine.Quaternion.AngleAxis((Mathf.Sin(observer.timer) * 0.1f +  // waving
                                                                   // moving field based on position
                                                                   Mathf.Sin(transform.position.x * 0.5f + 0.1f * observer.timer) +
                                                                   Mathf.Cos(0.5f * (transform.position.y + 0.1f * observer.timer))) *
                                                                   Mathf.Rad2Deg +
                                                                  2 * observer.timer,  // regular rotation
                                                               UnityEngine.Vector3.forward);
    }
}
