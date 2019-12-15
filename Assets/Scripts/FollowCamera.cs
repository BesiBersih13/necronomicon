using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [Tooltip("Objek yang akan diikuti kamera")] 
    public Transform followedObject;

    [Tooltip("Besar penghalusan gerakan kamera")]
    public float smoothing = 0.1f;

    // Jarak antara objek dengan kamera
    private float offset;

    // Untuk perhitungan smoothing (tidak perlu dihiraukan)
    private Vector3 currentVelocity;

    // Start dijalankan saat objek muncul
    private void Start()
    {
        // Menghitung jarak antar kamera dan objek pada sumbu z
        offset = followedObject.position.z - transform.position.z;
    }

    // FixedUpdate digunakan karena gerakan lain pada scene dilakukan pada FixedUpdate
    void FixedUpdate()
    {
        // Menghitung gerakan yang dihaluskan menuju objek
        transform.position = Vector3.SmoothDamp(transform.position, followedObject.position + Vector3.back * offset, ref currentVelocity, 0.1f);
    }
}