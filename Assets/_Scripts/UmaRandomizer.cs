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
            float value = 0f;
            value = Random.Range(-1.0f, 1.0f);
            if (value < 0)
            {
                avatar.ChangeRace("HumanFemaleDCS");
                avatar.BuildCharacter();
            }

            dna = avatar.GetDNA();

            string[] part = { "height", /*"headSize",*/ "headWidth", "neckThickness", /*"armLength",*/ "forearmWidth", /*"handsSize",*/
                              /*"feetSize",*/ "legSeparation", "upperMuscle", "lowerMuscle", "upperWeight", "lowerWeight",
                              /*"legsSize",*/ "belly", "waist", "gluteusSize", "earsSize", "earsPosition", "earsRotation",
                              "noseSize", "noseCurve", "noseWidth", "noseInclination", "nosePosition", "nosePronounced",
                              "noseFlatten", "chinSize", "chinPronounced", "chinPosition", "mandibleSize", "jawsSize",
                              "jawsPosition", "cheekSize", "cheekPosition", "lowCheekPronounced", "lowCheekPosition",
                              "foreheadSize", "foreheadPosition", "lipsSize", "mouthSize", "eyeRotation", "eyeSize",
                              "breastSize" };

            for (int i = 0; i < part.Length; i++)
            {
                value = Random.Range(0.0f, 1.0f);
                dna[part[i]].Set(value);
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
