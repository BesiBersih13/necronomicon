using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class DeathZone : AreaTrigger
{
    // Dijalankan oleh OnTriggerEnter2D pada script AreaTrigger
    protected override void Enter()
    {
        // Menjalankan aksi yang ada di fungsi Enter pada AreaTrigger
        base.Enter();

        // Me-reset score
        Coin.score = 0;

        // Memuat scene yang sedang aktif (level yang sedang dimainkan)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}