using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;

[RequireComponent(typeof(DynamicCharacterAvatar))]
public class UmaRandomizer : MonoBehaviour {

    private DynamicCharacterAvatar avatar;
    private Dictionary<string, DnaSetter> dna;

    bool ready;

    private void OnEnable()
    {
        avatar = GetComponent<DynamicCharacterAvatar>();
        avatar.CharacterCreated.AddListener(create);
    }

    private void OnDisable()
    {
        avatar.CharacterCreated.RemoveListener(create);
    }

    private void Start()
    {
        ready = false;
    }

    private void Update()
    {
        if (ready)
        {
            dna = avatar.GetDNA();
            float value;
            foreach (KeyValuePair<string, DnaSetter> entry in dna)
            {
                float u1 = Random.Range(0f, 1f); //uniform(0,1) random floats
                float u2 = Random.Range(0f, 1f);
                value = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                        Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1) using box mueller's method

                //bring values inside needed range
                value += 3.5f;
                Mathf.Clamp(value, 0.0f, 3.5f);
                value = value / 7.0f;

                entry.Value.Set(value);
            }

            avatar.BuildCharacter();
            ready = false;
        }
    }

    void create(UMAData data)
    {
        ready = true;
    }
}
