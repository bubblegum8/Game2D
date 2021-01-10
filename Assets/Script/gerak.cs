using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gerak : MonoBehaviour
{
    //logika player
    public int kecepatan;
    public int kekuatanlompat;
    public bool balik;
    public int pindah;
    Rigidbody2D lompat;
    //variabel sensor tanah
    public bool tanah;
    public LayerMask targetlayer;
    public Transform deteksitanah;
    public float jangkauan;
    //variabel nyawa
    public int Heart;
    //variabel apple;
    public int BuahApel;
    // Start is called before the first frame update
    //Animasi
    Animator anim;

    Vector2 play;
    public bool play_again;
    //UI Variabel Heart
    Text info_Heart;
    //UI Variabel Apel
    Text info_Apel;
    void Start()
    {
        lompat=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        play = transform.position;
        info_Heart = GameObject.Find("UI_Heart").GetComponent<Text>();
        info_Apel = GameObject.Find("UI_Apel").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //UI Nyawa
        info_Heart.text = "Heart : " + Heart.ToString();
        //UI Nyawa
        info_Apel.text = "Apel : " + BuahApel.ToString();
        //logika ulang ketika kena enemi
        if (play_again==true)
        {
            transform.position = play;
            play_again = false;
        }
        //logika nyawa
        if (Heart < 0)
        {
            Destroy(gameObject);
        }
        //logika animasi
        if (tanah == false)
        {
            anim.SetBool("Lompat", true);
        }
        else
        {
            anim.SetBool("Lompat", false);
        }
        //sensor tanah
        tanah = Physics2D.OverlapCircle(deteksitanah.position, jangkauan, targetlayer);
        //control player
        if (Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector2.right * kecepatan * Time.deltaTime);
            pindah = -1;
            anim.SetBool("Lari", true);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.right * -kecepatan * Time.deltaTime);
            pindah = 1;
            anim.SetBool("Lari", true);
        }
        else
        {
            anim.SetBool("Lari", false);
        }

        if (tanah==true && Input.GetKey(KeyCode.W))
        {
            lompat.AddForce(new Vector2(0, kekuatanlompat));
        }

        //flip body
        if(pindah > 0 && !balik)
        {
            flip();
        }
        else if(pindah < 0 && balik)
        {
            flip();
        }
    }

    void flip()
    {
        balik = !balik;
        Vector3 Player = transform.localScale;
        Player.x *= -1;
        transform.localScale = Player;
    }
}
