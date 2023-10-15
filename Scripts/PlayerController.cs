using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField] private float  jumpPower;
    [SerializeField] private Animator animator;


    private Vector3 _moveVelocity;
    private PlayerStatus _status;
    private MobAttack _attack;

    private CharacterController _characterController;

    private Transform _transform;

    void Start()
    {
        //Initilize and Cache
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_characterController.isGrounded ? "grounded" : "in sky");

        if(Input.GetButtonDown("Fire1")) //left click
        {
            _attack.AttackIfPossible();
        }

        if (_status.IsMovable)
        {
            _moveVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
            _moveVelocity.z = Input.GetAxis("Vertical") * 3;

            _transform.LookAt(_transform.position + new Vector3(_moveVelocity.x, 0, _moveVelocity.z));
        }
        else 
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;
        }

        if(_characterController.isGrounded)
        {
            if(Input.GetButton("Jump"))
            {
                Debug.Log("Jump!");
                _moveVelocity.y = jumpPower; //upward
            }
        }
        else
        {
            // acceleration due to grabity
            _moveVelocity.y += Physics.gravity.y * Time.deltaTime;           
        }

        //move object
        _characterController.Move(_moveVelocity * Time.deltaTime);
        animator.SetFloat("MoveSpeed", new Vector3(_moveVelocity.x, 0, _moveVelocity.z).magnitude);
        
    }   
}
