using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Coin : AreaTrigger
{
    // Nilai score yang disimpan secara 'global'
    public static float score = 0;

    [Tooltip("Tambahan score ketika koin diambil")]
    public float value;

    // Dijalankan oleh OnTriggerEnter2D pada script AreaTrigger
    protected override void Enter()
    {
        // Menjalankan aksi yang ada di fungsi Enter pada AreaTrigger
        base.Enter();

        // Menambahkan score: score = score + value;
        score += value;

        // Mencetak score pada console
        print("Score: " + score);

        // Menghancurkan koin
        Destroy(gameObject);
    }
}