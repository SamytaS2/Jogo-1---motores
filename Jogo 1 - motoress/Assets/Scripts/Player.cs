using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;


    private Rigidbody2D rig;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move(){
        Vector3 moviment = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += moviment * Time.deltaTime * Speed;

        if(Input.GetAxis("Horizontal") > 0f){
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f); 
        }

        if(Input.GetAxis("Horizontal") < 0f){
            anim.SetBool("Walk", true);
            transform.eulerAngles = new Vector3(0f,180f,0f); 
        }

        if(Input.GetAxis("Horizontal") == 0){
            anim.SetBool("Walk", false);
        }
       
    }
    
    void Jump(){
        if(Input.GetButtonDown("Jump")){
            if(!isJumping){
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("Jump", true);
            }
            else{
                if(doubleJump){
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping =false;
            anim.SetBool("Jump", false);
        }

        if(collision.gameObject.tag == "Espinhos"){
            GameController.instance.ShowGameOver();
            //Destroy(gameObject);
        }

        if(collision.gameObject.tag == "BolaEspinho"){
            GameController.instance.ShowGameOver();
            //Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Princesa"){
           GameController.instance.ExibirMsgFinal(); 






           //so pra ver o que acontece
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.layer == 8){
            isJumping = true;
        }
    }
}
