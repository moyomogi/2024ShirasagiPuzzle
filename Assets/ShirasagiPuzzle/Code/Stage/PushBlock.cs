using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
    public bool collides = false;

    // [SerializeField] ControlledCapsuleCollider m_ControlledCollider;
    // m_ControlledCollider = transform.GetComponent<ControlledCapsuleCollider>();
    private GameObject _player;
    private CharacterControllerBase m_CharacterControllerBase;
    // private AbilityModule m_AbilityModule;
    private Rigidbody _rb;
    private const string m_ModuleName = "PushBlock";
    // private const float PUSH_SPEED = 10.0f;
    private const float PUSH_SPEED = 18.0f;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        // m_AbilityModule = _player.GetComponent<AbilityModule>();
        m_CharacterControllerBase = _player.GetComponent<CharacterControllerBase>();
        // PlayerInput pi = m_CharacterControllerBase.GetPlayerInput().GetDirectionInput("Move");
        _rb = this.transform.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (collides)
        {
            bool playerIsLeft = _player.transform.position.x < this.transform.position.x;

            // if ((playerIsLeft || Input.GetKeyDown(KeyCode.E)) && GetDirInput("Move").m_Direction == DirectionInput.Direction.Right)
            if (playerIsLeft && GetDirInput("Move").m_Direction == DirectionInput.Direction.Right)
            {
                _rb.AddForce(new Vector3(PUSH_SPEED, 0, 0));
            }
            // if ((!playerIsLeft || Input.GetKeyDown(KeyCode.E)) && GetDirInput("Move").m_Direction == DirectionInput.Direction.Left)
            if (!playerIsLeft && GetDirInput("Move").m_Direction == DirectionInput.Direction.Left)
            {
                _rb.AddForce(new Vector3(-PUSH_SPEED, 0, 0));
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            collides = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        switch (other.tag)
        {
            // 動く床
            case "Wall" or "PushBlock" or "LightningBox":
                _rb.velocity = other.GetComponent<Rigidbody>().velocity;
                float add = 1.0f + other.gameObject.transform.localScale.y / 2;
                transform.position = new Vector3(transform.position.x, other.gameObject.transform.position.y + add, 0);
                break;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collides = false;
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
