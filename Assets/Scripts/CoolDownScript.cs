using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDownScript : MonoBehaviour
{
    public float cooldownTime = 2f;
    private float cooldownTimeChanged;
    bool isCooldown;
    public Abilities.spellTypes spellTypes;
    public Image imageCooldown;
    public Text cdText;

    public Text message;
    private FadeText fadecdText;

    public GameObject player;
    private SpawnProjectile spellProjectiles;
    private PlayerMana mana;

    void Start()
    {
        cdText.text = cooldownTime.ToString();
        spellProjectiles = player.GetComponentInChildren<SpawnProjectile>();
        mana = player.GetComponent<PlayerMana>();
        fadecdText = message.GetComponent<FadeText>();
        cooldownTimeChanged = cooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && spellTypes == Abilities.spellTypes.normalSpell)
        {
            if (!isCooldown)
            {
                if (mana.currentMana >= spellProjectiles.manaSpellCost[spellProjectiles.spellChosen])
                {
                    isCooldown = true;
                    imageCooldown.gameObject.SetActive(true);
                }
            }
            else
            {
                message.gameObject.SetActive(true);
                message.text = "Spell not ready...";
                fadecdText.TextFade();
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && spellTypes == Abilities.spellTypes.ultimate)
        {
            if (!isCooldown)
            {
                if (mana.currentMana >= spellProjectiles.manaUltiCost[spellProjectiles.ultiChosen])
                {
                    isCooldown = true;
                    spellProjectiles.isUltiUsed = true;
                    imageCooldown.gameObject.SetActive(true);
                }
            }
            else
            {
                message.gameObject.SetActive(true);
                message.text = "Spell not ready...";
                fadecdText.TextFade();
            }
        }

        if (isCooldown)
        {
            imageCooldown.fillAmount -= 1 / cooldownTime * Time.deltaTime;

            if (cooldownTimeChanged >= 0)
            {
                cooldownTimeChanged -= Time.deltaTime;
                DisplayTime(cooldownTimeChanged);
            }

            if (imageCooldown.fillAmount <= 0)
            {
                imageCooldown.fillAmount = 1;
                imageCooldown.gameObject.SetActive(false);
                cooldownTimeChanged = cooldownTime;
                isCooldown = false;

                if (spellTypes == Abilities.spellTypes.ultimate)
                {
                    spellProjectiles.isUltiUsed = false;
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        cdText.text = seconds.ToString();
    }
}
