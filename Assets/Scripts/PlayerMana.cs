using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    public float maxMana = 40f; 
    public float currentMana;

    public PlayerManaBar manaBar;
    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    void Update()
    {
        if (Time.time > nextActionTime && currentMana < maxMana)
        {
            nextActionTime += period;
            currentMana += 0.2f;
            manaBar.SetMana(currentMana);
        }
    }
}
