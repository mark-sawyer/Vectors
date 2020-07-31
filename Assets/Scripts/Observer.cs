using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Observer : MonoBehaviour {
    public GameObject vector;
    public Complex[] zeroes;
    public float timer;
    private float[] angles;

    public void Start() {
        int numberOfZeroes = 4;
        zeroes = new Complex[numberOfZeroes];
        angles = new float[numberOfZeroes];
        for (int i = 0; i < angles.Length; i++) {
            angles[i] = Random.Range(0, 2 * Mathf.PI);
        }

        for (float i = -12f; i <= 12f; i = i + 0.2f) {
            for (float j = -6f; j <= 6f; j = j + 0.2f) {
                Instantiate(vector, new UnityEngine.Vector3(i, j, 0f), UnityEngine.Quaternion.identity);
            }
        }
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        timer += Time.deltaTime;
        for (int i = 0; i < zeroes.Length; i++) {
            zeroes[i] += new Complex(Mathf.Cos(angles[i]), Mathf.Sin(angles[i])) * 0.01;
            angles[i] += Random.Range(-0.1f, 0.1f);
            if (zeroes[i].Magnitude > 12.5) {
                angles[i] += Mathf.PI;
            }
        }
    }
}
