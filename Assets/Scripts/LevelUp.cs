using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public GameObject buttonActive;
    public GameObject buttonDeactivated;

    public enum statType { Ability, Ultimate, Base}
    public statType currentType = statType.Base;

    public Text pointsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void UseXPPoint()
    {
        buttonActive.SetActive(true);
        buttonDeactivated.SetActive(false);

        GameController.instance.expEarned -= 1;
        pointsText.text = "Points left: " + GameController.instance.expEarned;
    }

    void returnXPPoint()
    {
        buttonActive.SetActive(false);
        buttonDeactivated.SetActive(true);

        GameController.instance.expEarned += 1;
        pointsText.text = "Points left: " + GameController.instance.expEarned;
    }


}
