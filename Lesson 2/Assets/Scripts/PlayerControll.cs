using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class PlayerControll : MonoBehaviour {
    public float speed = 6.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 20.0f;
    public float rotationSpeed = 90;
    public Text score;
    public GameObject coin;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 moveCoin;
    private CharacterController controller;
    private int count=0;
    private bool DoubleJump=false;
    void Start() {
        controller = gameObject.GetComponent<CharacterController>();
        score.text = count.ToString();
    }
    void Update() {
        if (controller.isGrounded) {
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *=  speed;
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
                DoubleJump = true;
            }
            
        } else {
            if (DoubleJump && Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed * 2;
                DoubleJump = false;
            }
            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        }
        controller.Move(moveDirection * Time.deltaTime);
        Vector3 rotateCoords = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0);
        controller.transform.Rotate(rotateCoords);
        
        

    }
    void SetCountText()
    {
        count++;
        score.text =count.ToString();
       
    }

    public void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Coin") {
            
            Destroy(hit.gameObject);
            
            moveCoin = new Vector3(Random.Range(-50,50), Random.Range(0, 5), Random.Range(-150, 150));
            Debug.Log(moveCoin);
            GameObject gm = Instantiate(coin);
            gm.transform.position = moveCoin;
            SetCountText();

            
        }
    }
}
