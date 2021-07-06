using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerShooting : MonoBehaviour
{
    private Vector3 m_MovementInput; //Ga naar de PlayerController script voor het wijzigen van movement :)
    [SerializeField] private Transform m_Body;

    [Header("Shooting")]
    [SerializeField] private Transform m_FirePoint = null;
    [SerializeField] private GameObject m_ProjectilePrefab = null;
    [SerializeField] private float m_ProjectileSpeed = 4f;
    [SerializeField] private float m_ShootDelay = 0.8f;
    private float m_ShootDelayOffset = 0.5f;
    private Vector3 m_AimInput;
    private float m_ShootTimer;
    private float m_ShootInput;

    [Header("Energy")]
    [SerializeField] private VitalityMeter m_EnergyMeter = null;
    [SerializeField] private float m_MaxEnergy = 30;
    [SerializeField] private float m_EnergyUsed = 3;
    [SerializeField] private float m_ChargeSpeed = 1.2f;
    private float m_Energy;

    [Header("Crosshair")]
    [SerializeField] private GameObject m_Crosshair = null;
    [SerializeField] private float m_CrosshairOffset = 3f;
    [SerializeField] private float m_TurnSpeed = 10;


    void Start()
    {
        m_ShootTimer = m_ShootDelay * m_ShootDelayOffset;
        m_Energy = m_MaxEnergy * 0.8f;
    }


    private void Update()
    {
        ShootTimer();

        if(m_Energy < m_MaxEnergy)
        {
            if(m_ShootInput == 0 || m_ShootInput != 0 && m_Energy < m_EnergyUsed)
            {
                ChargeEnergy();
            }
        }

        if (m_MovementInput == Vector3.zero) //Checkt of de speler niet beweegt :)
        {
            RotatePlayer(m_AimInput.normalized);
        }

    }

    private void ShootTimer()
    {
        if(m_ShootInput != 0)
        {
            RotatePlayer(m_AimInput);

            if (m_ShootTimer > 0)
            {
                m_ShootTimer -= Time.deltaTime;
            }

            if (m_Energy > m_EnergyUsed)
            {
                if (m_ShootTimer < 0)
                {
                    Shoot();
                }
            }
        }
        else
        {
            m_ShootTimer = m_ShootDelay * m_ShootDelayOffset;
        }
    }

    private void Shoot()
    {
        m_ShootTimer = m_ShootDelay;
        m_Energy = Mathf.Clamp(m_Energy - m_EnergyUsed, 0, m_MaxEnergy); //Houdt het getal tussen de minimum en het maximum
        m_EnergyMeter.UpdateMeter(m_Energy / m_MaxEnergy);

        GameObject projectile = Instantiate(m_ProjectilePrefab, m_FirePoint.position, m_FirePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(m_Body.forward.normalized * m_ProjectileSpeed, ForceMode.Impulse);
        Destroy(projectile, 2);
    }

    public void GetAimInput(InputAction.CallbackContext context)
    {
        m_AimInput.x = context.ReadValue<Vector2>().x;
        m_AimInput.z = context.ReadValue<Vector2>().y;
    }

    public void GetShootInput(InputAction.CallbackContext context)
    {
        m_ShootInput = context.ReadValue<float>();
    }

    public void CheckMovementInput(InputAction.CallbackContext context)
    {
        m_MovementInput.x = context.ReadValue<Vector2>().x;
        m_MovementInput.z = context.ReadValue<Vector2>().y;
    }

    private void RotatePlayer(Vector3 direction)
    {
        Vector3 lookDirection = Vector3.RotateTowards(m_Body.transform.forward, direction, m_TurnSpeed * Time.deltaTime, 0);
        m_Body.rotation = Quaternion.LookRotation(lookDirection); //Roteer alleen de model (Body) van de speler
    }

    private void ChargeEnergy()
    {
        m_Energy = Mathf.Clamp(m_Energy + Time.deltaTime * m_ChargeSpeed, 0, m_MaxEnergy); //Houdt het getal tussen de minimum en het maximum
        m_EnergyMeter.UpdateMeter(m_Energy / m_MaxEnergy);
    }
}


//m_MousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

//m_PlayerAmmo = m_MaxPlayerAmmo;
//m_AmmoBeforeReload = m_MaxAmmoBeforeReload;
//m_AmmoAmountText.text = m_AmmoBeforeReload.ToString() + " / " + m_PlayerAmmo.ToString();

//if (m_Plane.Raycast(m_MousePosition, out float hitPoint))
//        {
//            m_LookDirection = m_MousePosition.GetPoint(hitPoint) - m_Rigidbody.position;
//            m_LookDirection.y = 0;
//        }
//projectile.GetComponent<Rigidbody>().AddForce(m_Crosshair.transform.localPosition.normalized * m_ProjectileSpeed, ForceMode.Impulse);


//private void ReloadChecker()
//{
//    if(m_AmmoBeforeReload <= 0)
//    {
//        if(Input.GetKeyDown(KeyCode.R))
//        {
//            if(m_PlayerAmmo >= m_MaxAmmoBeforeReload)
//            {
//                m_PlayerAmmo -= m_MaxAmmoBeforeReload;
//                m_AmmoBeforeReload = m_MaxAmmoBeforeReload;
//                //m_AmmoAmountText.text = m_AmmoBeforeReload.ToString() + " / " + m_PlayerAmmo.ToString();
//            }
//        }
//    }
//}

//projectile.GetComponent<Rigidbody>().AddForce(m_LookDirection.normalized * m_ProjectileSpeed, ForceMode.Impulse);
//m_AmmoAmountText.text = m_AmmoBeforeReload.ToString() + " / " + m_PlayerAmmo.ToString();
//m_AmmoBeforeReload--;

//if(m_AimInput != Vector3.zero)
//{
//    m_Crosshair.SetActive(true);
//    m_Crosshair.transform.localPosition = m_AimInput * m_CrosshairOffset;
//    m_Crosshair.transform.rotation = Quaternion.identity;
//}
//else
//{
//    m_Crosshair.SetActive(false);
//}

//[Header("Ammo")]
//[SerializeField] private Text m_AmmoAmountText = null;
//[SerializeField] private float m_MaxPlayerAmmo = 60;
//private float m_PlayerAmmo;
//[SerializeField] private float m_MaxAmmoBeforeReload = 5;
//private float m_AmmoBeforeReload;

//private void RotatePlayer()
//{
//    if (Physics.Raycast(m_MousePosition, out RaycastHit hit, Mathf.Infinity))
//    {
//        m_LookDirection = hit.point - m_Rigidbody.position;
//        m_LookDirection.y = 0;
//    }

//    m_Rigidbody.MoveRotation(Quaternion.LookRotation(m_LookDirection, Vector3.up));
//}


//public void ChangeAmmoValue(float amount)
//{
//    //m_PlayerAmmo = Mathf.Clamp(m_PlayerAmmo + amount, 0, m_MaxPlayerAmmo); //Houdt het getal tussen de minimum en het maximum
//    //m_AmmoAmountText.text = m_AmmoBeforeReload.ToString() + " / " + m_PlayerAmmo.ToString();
//}


//public bool AllowAmmoPickup()
//{
//    //if(m_PlayerAmmo < m_MaxPlayerAmmo)
//    //{
//    //    return true;
//    //}
//    return false;
//}