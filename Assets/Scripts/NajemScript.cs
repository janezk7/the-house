using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NajemScript : MonoBehaviour
{
    public TMPro.TextMeshProUGUI KvadraturaText;
    public Transform NajemTransform;

    private int CurrentMeters = 13;

    // Start is called before the first frame update
    void Start()
    {
        UpdateTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
            UpdateMeterLength(-1);
        else if (Input.GetKeyDown(KeyCode.Alpha0))
            UpdateMeterLength(1);
    }

    void UpdateMeterLength(int metersToAdd)
    {
        CurrentMeters += metersToAdd;
        if (CurrentMeters < 0)
            CurrentMeters = 0;
        if (CurrentMeters > 13)
            CurrentMeters = 13;

        UpdateTransform();
    }

    void UpdateTransform()
    {
        var transformLocalMeters = CurrentMeters / 13.0f;
        NajemTransform.localScale = new Vector3(
            transformLocalMeters,
            NajemTransform.localScale.y,
            NajemTransform.localScale.z
            );
        UpdateText();
    }

    void UpdateText()
    {
        var meters = CurrentMeters + 7;
        KvadraturaText.text = $"{meters}m ({meters * 12}m^2)";
    }
}
