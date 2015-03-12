using UnityEngine;
using System.Collections;

public class MoveCharacter : MonoBehaviour
{

    private GameObject _character;
    private CharacterController _controller;
    private float _speed;
    private float _speedRotation;
    private Vector3 _motion;

    public MoveCharacter(GameObject character)
    {
        _character = character;
    }

    // Use this for initialization
    void Start()
    {
        if ( _speed == 0 )
        {
            _speed = 3; 
            Debug.Log( "Utilisation valeur par défaut" );
        }
        if ( _speedRotation == 0 )
        {
            _speedRotation = 10;
            Debug.Log( "Utilisation valeur par défaut" );
        }

        _controller = _character.GetComponents<CharacterController>()[0];

    }

    // Update is called once per frame
    void Update()
    {

        _motion.x = 0;
        _motion.y = 0;
        _motion.z = 0;

        _controller.Move( _motion * _speed * Time.deltaTime );

    }

    // Testing _motion code

    public void MoveRight()
    {
        _motion.x += _speed;
        _controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( Vector3.right ), _speedRotation * Time.deltaTime );
    }
    public void MoveLeft()
    {
        _motion.x -= _speed;
        _controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( Vector3.left ), _speedRotation * Time.deltaTime );
    }
    public void MoveUp()
    {
        _motion.z += _speed;
        _controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( Vector3.forward ), _speedRotation * Time.deltaTime );
    }
    public void MoveDown()
    {
        _motion.z -= _speed;
        _controller.transform.rotation = Quaternion.Slerp( _controller.transform.rotation, Quaternion.LookRotation( Vector3.back ), _speedRotation * Time.deltaTime );
    }
}