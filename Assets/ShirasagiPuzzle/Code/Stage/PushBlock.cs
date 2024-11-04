using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public bool collidesPlayer = false;

    // [SerializeField] ControlledCapsuleCollider m_ControlledCollider;
    // m_ControlledCollider = transform.GetComponent<ControlledCapsuleCollider>();
    private GameObject _player;
    private CharacterControllerBase m_CharacterControllerBase;
    // private AbilityModule m_AbilityModule;
    private Rigidbody _rb;
    private float prevX = 0.0f;

    private const string m_ModuleName = "PushBlock";
    private const float PUSH_SPEED = 14.0f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // m_AbilityModule = _player.GetComponent<AbilityModule>();
        m_CharacterControllerBase = _player.GetComponent<CharacterControllerBase>();
        // PlayerInput pi = m_CharacterControllerBase.GetPlayerInput().GetDirectionInput("Move");
        _rb = this.transform.GetComponent<Rigidbody>();

        prevX = transform.position.x;
    }
    void Update()
    {
        if (collidesPlayer)
        {
            bool playerIsLeft = _player.transform.position.x < transform.position.x;

            // if ((playerIsLeft || Input.GetKeyDown(KeyCode.E)) && GetDirInput("Move").m_Direction == DirectionInput.Direction.Right)
            if (playerIsLeft && GetDirInput("Move").m_Direction == DirectionInput.Direction.Right)
            {
                _rb.AddForce(new Vector3(PUSH_SPEED, 0, 0));
                // 床埋まり回避
                _rb.AddForce(new Vector3(0, 0.1f * PUSH_SPEED, 0));
            }
            // if ((!playerIsLeft || Input.GetKeyDown(KeyCode.E)) && GetDirInput("Move").m_Direction == DirectionInput.Direction.Left)
            if (!playerIsLeft && GetDirInput("Move").m_Direction == DirectionInput.Direction.Left)
            {
                _rb.AddForce(new Vector3(-PUSH_SPEED, 0, 0));
                // 床埋まり回避
                _rb.AddForce(new Vector3(0, 0.1f * PUSH_SPEED, 0));
            }
            prevX = transform.position.x;
        } else {
            transform.position = new Vector3(prevX, transform.position.y, 0);
            _rb.velocity = new Vector3(0, Mathf.Min(0,0f, _rb.velocity.y), 0);
            // _rb.AddForce(new Vector3(0, -0.1f * PUSH_SPEED, 0));
            // _rb.AddForce(new Vector3(0, 0.1f * PUSH_SPEED, 0));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collidesPlayer = true;

            // _rb.velocity = other.GetComponent<Rigidbody>().velocity;
            float add = 0.5f * transform.localScale.y + 1.0f * _player.transform.localScale.y - 6.0f;
            _player.transform.position = new Vector3(
                _player.transform.position.x,
                Mathf.Max(_player.transform.position.y, transform.position.y + add),
                0
            );
        }
    }
    // void OnTriggerStay(Collider other)
    // {
    //     switch (other.tag)
    //     {
    //         // 動く床
    //         // csse "PushBlock":
    //         // csse "LightningBox":
    //         case "Wall":
    //             // 水平方向
    //             _rb.velocity = other.GetComponent<Rigidbody>().velocity;
    //             float add = 0.5f * transform.localScale.y + 0.5f * other.transform.localScale.y - 6.0f;
    //             transform.position = new Vector3(transform.position.x, other.transform.position.y + add, 0);
    //             // 鉛直方向
    //             // _rb.velocity = other.GetComponent<Rigidbody>().velocity;
    //             // float add = 0.5f * transform.localScale.y + 0.5f * other.transform.localScale.y - 6.0f;
    //             // transform.position = new Vector3(transform.position.x, other.transform.position.y + add, 0);
    //             break;
    //     }
    // }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collidesPlayer = false;
            _rb.velocity = Vector3.zero;
        }
    }

    protected DirectionInput GetDirInput(string a_Name)
    {
        if (m_CharacterControllerBase == null)
        {
            Debug.LogError("Character controller not found for " + GetName() + "!");
            return null;
        }
        if (m_CharacterControllerBase.GetPlayerInput() == null)
        {
            Debug.LogError("Player input not found for " + GetName() + "!");
            return null;
        }
        return m_CharacterControllerBase.GetPlayerInput().GetDirectionInput(a_Name);
    }
    public string GetName()
    {
        return m_ModuleName;
    }
}
