using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillUi : MonoBehaviour
{
    private GameObject melee;
    private GameObject range;
    private GameObject heal;
    private GameObject buf;
    private GameObject dodge;

    private PlayerController player;

    void Awake()
    {
        melee = transform.GetChild(0).gameObject;
        range = transform.GetChild(1).gameObject;
        heal = transform.GetChild(2).gameObject;
        buf = transform.GetChild(3).gameObject;
        dodge = transform.GetChild(4).gameObject;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        updateSKill();
    }

    void updateSKill()
    {
        melee.GetComponent<Image>().fillAmount = player.getMeleeCool() / 10f;
        range.GetComponent<Image>().fillAmount = player.getRangeCool() / 10f;
        heal.GetComponent<Image>().fillAmount = player.getHealCool() / 10f;
        buf.GetComponent<Image>().fillAmount = player.getBufCool() / 10f;
        dodge.GetComponent<Image>().fillAmount = player.getDodgeCool() / 10f;
    }
}
