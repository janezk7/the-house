using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseSwitching : MonoBehaviour
{
    public List<MoveHouse> Houses;
    public TMPro.TextMeshProUGUI CurrentHouseText;

    private void Start()
    {
        ActivateHouse(0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateHouse(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateHouse(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateHouse(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateHouse(3);
        }
    }

    private void ActivateHouse(int index)
    {
        foreach(var house in Houses)
            house.gameObject.SetActive(false);

        var currentHouse = Houses[index];
        currentHouse.gameObject.SetActive(true);
        //currentHouse.ResetHouse();
        CurrentHouseText.text = currentHouse.gameObject.name;
    }
}
