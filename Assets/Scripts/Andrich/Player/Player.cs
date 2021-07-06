using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private VitalityMeter m_HealthMeter = null;
    [SerializeField] private float m_MaxPlayerHealth = 10f;
    private float m_PlayerHealth;

    [Header("Armor")]
    //[SerializeField] private VitalityMeter m_ArmorMeter = null;
    [SerializeField] private Text m_ArmorText;
    [SerializeField] private float m_MaxPlayerArmor = 5f;
    private float m_PlayerArmorAmount;
    private float m_DamageLeft;

    [SerializeField] private List<string> m_KeyCardColorsInInventory = new List<string>();


    private GameObject[] m_Enemies;

    private void Start()
    {
        m_PlayerHealth = m_MaxPlayerHealth;
        m_DamageLeft = 0;
        m_PlayerArmorAmount = 0;
        m_ArmorText.text = m_PlayerArmorAmount.ToString();
        m_KeyCardColorsInInventory.Clear();

        m_Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void ChangePlayerVitality(float amount, string type)
    {
        if (type == "Damage")
        {
            float damage = amount; //Hoeveelheid damage
            damage = -damage; //Om het een negatief getal te maken
            if(m_PlayerArmorAmount > 0) //Als de speler armor heeft
            {
                if(amount > m_PlayerArmorAmount) //Als de damage groter is dan de hoeveelheid armor
                {
                    m_DamageLeft = m_PlayerArmorAmount + damage; //Overgebleven damage is armor hoeveelheid - damge ( + omdat het een negatief getal is)
                }

                ChangeArmorAmount(damage);
                ChangeHealth(m_DamageLeft);
                m_DamageLeft = 0;
            }
            else
            {
                ChangeHealth(damage);
            }
        }
        else if(type == "Armor")
        {
            ChangeArmorAmount(amount); //Voeg toe aan armor
        }
        else if(type == "Heal")
        {
            ChangeHealth(amount); //Voeg toe aan health
        }
    }

    private void ChangeHealth(float amount)
    {
        m_PlayerHealth = Mathf.Clamp(m_PlayerHealth + amount, 0, m_MaxPlayerHealth); //Houdt het getal tussen de minimum en het maximum
        m_HealthMeter.UpdateMeter(m_PlayerHealth / m_MaxPlayerHealth);

        if(m_PlayerHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangeArmorAmount(float amount)
    {
        m_PlayerArmorAmount = Mathf.Clamp(m_PlayerArmorAmount + amount, 0, m_MaxPlayerArmor); //Houdt het getal tussen de minimum en het maximum
        m_ArmorText.text = m_PlayerArmorAmount.ToString();
        //m_ArmorMeter.UpdateMeter(m_PlayerArmorAmount / m_MaxPlayerArmor); //Nieuw (huidig) gedeeld door oud (maximum) van armor doorgeven aan de ArmorMeter
    }


    public bool AllowArmorPickup()
    {
        if(m_PlayerArmorAmount < m_MaxPlayerArmor) //Kijkt of de speler een armor pickup mag pakken
        {
            return true;
        }
        return false;
    }

    public bool AllowHealing()
    {
        if (m_PlayerHealth < m_MaxPlayerHealth) //Kijkt of de speler een health pickup mag pakken
        {
            return true;
        }
        return false;
    }

    public List<string> GetInventory() //Returned de KeyCards in de KeyCards list
    {
        return m_KeyCardColorsInInventory;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("KeyCard"))
        {
            KeyCard keyCard = collision.gameObject.GetComponent<KeyCard>();
            if(!m_KeyCardColorsInInventory.Contains(keyCard.GetColor())) //Checkt of de KeyCard nog niet in de speler zijn inventory zit
            {

                m_KeyCardColorsInInventory.Add(keyCard.GetColor()); //Pakt de string van de KeyCard
                collision.gameObject.SetActive(false);

                for (int i = 0; i < m_Enemies.Length; i++)
                {
                    if(!m_Enemies[i].activeInHierarchy) //Als de niet aan staat in Unity
                    {
                        m_Enemies[i].SetActive(true);
                        m_Enemies[i].GetComponent<EnemyA>().ResetEnemy();
                    }
                }
            }
        }
    } 
}
