using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnProjectile : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject swordJoint;

    public List<GameObject> vfx = new List<GameObject>();
    public List<GameObject> ultiVFX = new List<GameObject>();

    public float[] manaSpellCost;
    public float[] manaUltiCost;

    public int spellChosen = 0;
    public int ultiChosen = 0;

    private GameObject effectToRespawn;
    public PlayerManaBar manaBar;
    private PlayerMana mana;

    private Animator animator;
    public Text manaText;
    private FadeText fadeManaText;

    public bool isUltiUsed;
    private HurtEnemy weaponDamage;

    // Start is called before the first frame update
    void Start()
    {
        effectToRespawn = vfx[spellChosen];
        animator = GetComponent<Animator>();
        fadeManaText = manaText.GetComponent<FadeText>();
        mana = GetComponentInParent<PlayerMana>();
        weaponDamage = swordJoint.GetComponent<HurtEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (mana.currentMana >= manaSpellCost[spellChosen])
            {
                animator.SetBool("isCastingSpell", true);
            }
            else
            {
                manaText.gameObject.SetActive(true);
                manaText.text = "Not enough mana...";
                fadeManaText.TextFade();
            }
        }
        else
        {
            animator.SetBool("isCastingSpell", false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isUltiUsed)
            {
                if (mana.currentMana >= manaUltiCost[ultiChosen])
                {
                    animator.SetBool("isUsingUlti", true);
                    StartCoroutine(UpdateStats());
                }
                else
                {
                    manaText.gameObject.SetActive(true);
                    manaText.text = "Not enough mana...";
                    fadeManaText.TextFade();
                }
            }
        }
        else
            animator.SetBool("isUsingUlti", false);

    }

    void SpawnVFX()
    {
        GameObject vfx;
        if (firePoint != null)
        {
            Vector3 firePointVector = new Vector3(firePoint.transform.position.x, firePoint.transform.position.y + .2f, firePoint.transform.position.z);
            vfx = Instantiate(effectToRespawn, firePointVector, Quaternion.identity);
        }
        else
        {

        }
    }

    public void CastSpellCost(float cost)
    {
        mana.currentMana -= cost;

        if (mana.currentMana <= 0)
        {
            mana.currentMana = 0;
            //Debug.Log("Not enough Mana");
        }

        manaBar.SetMana(mana.currentMana);

        //LogDamage();
    }

    private void CastSpell()
    {
        SpawnVFX();
        CastSpellCost(manaSpellCost[spellChosen]);
    }

    private void CastUltimate()
    {
        //GameObject childObject = Instantiate(ultiVFX[ultiChosen]) as GameObject;
        //childObject.transform.parent = swordJoint.transform;
        ultiVFX[ultiChosen].SetActive(true);
        CastSpellCost(manaUltiCost[ultiChosen]);
    }

    IEnumerator UpdateStats()
    {
        weaponDamage.objectDamage *= 2;

        yield return new WaitForSeconds(10.0f);

        weaponDamage.objectDamage /= 2;
        ultiVFX[ultiChosen].SetActive(false);
    }
}
