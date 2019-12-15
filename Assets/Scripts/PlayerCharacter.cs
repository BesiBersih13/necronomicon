using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
	
    //////////////////////////////
    //      PUBLIC VARIABLES    //
    //////////////////////////////
    [Tooltip("Mengontrol kecepatan gerak karakter")]
    public float moveSpeed = 7;

     [Tooltip("Mengontrol tinggi loncatan karakter")]
    public float jumpHeight = 3;
 
    // Event yang dijalankan saat meloncat
    public UnityEvent onJump;

    [Tooltip("Tag yang dimiliki semua objek ground")]
    public string groundTag;

    //////////////////////////////
    //      PRIVATE VARIABLES   //
    //////////////////////////////
    
    // Variabel boolean yang mengindikasi bahwa objek ada di tanah
    private bool onGround;
    
    // Variabel yang menunjukkan arah gerak horizontal pemain
    private float moveDirection;

    // Referensi terhadap komponen rigidbody pemain
    private Rigidbody2D rb2d;

    // Referensi terhadap komponen sprite pemain
    private SpriteRenderer spr;

    // Referensi terhadap komponen animator pemain
    private Animator anim;

    // Kekuatan loncatan - Kecepatan awal loncatan agar tinggi loncatan tercapai
    private float jumpStrength;
   
    // Variabel boolean yang mengindikasi bahwa objek akan meloncat
    private bool doJump;
    
    // OnCollisionEnter2D - dijalankan tiap kali player mulai bersentuhan dengan collider lain
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mengecek bila collider yang disentuh adalah tanah
        if (collision.gameObject.tag == groundTag)
        {
            // Player diasumsikan ada di tanah
            onGround = true;
        }
    }

    // OnCollisionExit2D - dijalankan tiap kali player berhenti bersentuhan dengan collider lain
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Mengecek bila collider yang ditinggalkan adalah tanah
        if (collision.gameObject.tag == groundTag)
        {
            // Player diasumsikan sudah tidak di tanah
            onGround = false;
        }
    }
    
    // Start - dijalankan pada awal dijalankannya script ini
    // alias pada konteks ini saat awal permainan
    private void Start()
    {
        // Menyimpan referensi komponen Rigidbody 2D & Sprite
        rb2d = GetComponent<Rigidbody2D>();

        spr = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animator>();

    

    // Menghitung kecepatan awal loncatan dengan rumus h-max = (vo^2) / 2g - Hayoloh... Masih inget ga?
        jumpStrength = Mathf.Sqrt(-2 * jumpHeight * rb2d.gravityScale * Physics2D.gravity.y);
    }
 
    // Update - dijalankan pada setiap frame / tiap kali ada penggambaran citra di layar
    private void Update()
    {
        // Mengecek bila tombol loncat dipencet dan player ada di tanah
        if (Input.GetButtonDown("Jump") && onGround)
        {
            // Meloncat
            doJump = true;
        }
    }

    // FixedUpdate - dijalankan setiap 1/50 detik (nilai default, dapat dikonfigurasi)
    private void FixedUpdate()
    {
        // Menyimpan arah derak player
        moveDirection = Input.GetAxis("Horizontal");

        // Menge-flip sprite ketika menghadap kiri-kanan
        // & menjalankan animasi gerak
        if (moveDirection > 0)
        {
            spr.flipX = false;
            anim.SetBool("Run", true);
        }
        else if (moveDirection < 0)
        {
            spr.flipX = true;
            anim.SetBool("Run", true);
        }
        else    // moveDirection == 0
        {
            anim.SetBool("Run", false);
        }

        // Menghitung kecepatan gerak pemain
        rb2d.velocity = new Vector2(moveDirection * moveSpeed, rb2d.velocity.y);
        
        // Mengecek bila sedang meloncat
        if (doJump)
        {
            // Merubah kecepatan vertikal sehingga seakan meloncat
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpStrength);
 
            // Memanggil semua aksi yang di-trigger event loncat
            onJump.Invoke();
 
            // Menghentikan loncatan
            doJump = false;

            // Melakukan animasi loncat
            anim.SetTrigger("Jump");
        }
    }
}