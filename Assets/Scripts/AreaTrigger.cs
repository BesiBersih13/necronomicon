using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class AreaTrigger : MonoBehaviour
{
    [Tooltip("Event yang akan dijalankan saat player memasuki area trigger")]
    public UnityEvent onEnter;

    [Tooltip("Event yang akan dijalankan saat player meninggalkan area trigger")]
    public UnityEvent onExit;

    // OnTriggerEnter dijalankan saat sebuah objek memasuki
    // daerah trigger yang dimiliki objek ini
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Mengecek bila objek yang masuk adalah player
        if (col.GetComponent<PlayerCharacter>())
        {
            // Menjalankan fungsi enter
            Enter();
        }
    }

    // OnTriggerExit dijalankan saat sebuah objek meninggalkan
    // daerah trigger yang dimiliki objek ini
    private void OnTriggerExit2D(Collider2D col)
    {
        // Mengecek bila objek yang masuk adalah player
        if (col.GetComponent<PlayerCharacter>())
        {
            // Menjalankan fungsi exit
            Exit();
        }
    }

    // Virtual digunakan agar fungsi dapat dimodifikasi nantinya
    protected virtual void Enter()
    {
        // Menjalankan semua action yang terhubung oleh onEnter
        onEnter.Invoke();
    }

    // Virtual digunakan agar fungsi dapat dimodifikasi nantinya
    protected virtual void Exit()
    {
        // Menjalankan semua action yang terhubung oleh onExit
        onExit.Invoke();
    }

}