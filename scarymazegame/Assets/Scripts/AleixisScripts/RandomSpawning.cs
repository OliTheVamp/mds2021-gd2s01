﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawning : MonoBehaviour
{
    [Header("Variables")]
    float ElapsedTime = 0.0f;
    float ResetTime = 3.0f;
    bool ResetStart = false;

    [Header("Serialized Fields")] //serializes the fields
    [SerializeField] private Transform Merchant;
    [SerializeField] private Transform DefaultSpawn;

    private void OnTriggerEnter(Collider other) // On trigger enter means when the script attached object hits the box collider of a trigger object.
    {
        if (other.CompareTag("Spawner")) //When asset hits trigger, the script compares the tag of the trigger to see if it is a spawner
        {
            ResetStart = false;
            ElapsedTime = 0.0f;
            Merchant.transform.position = other.transform.position; //Takes the position of the merchant then transforms it (changes it) to the transform of the selected position
            Physics.SyncTransforms(); //Syncs transforms so that nothing breaks, unity does a lot of the damage control
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Spawner"))
        {
            ResetStart = true;
            StartCoroutine(ElapsedTimer());
        }
    }

    void Start()
    {
        Merchant.transform.position = DefaultSpawn.transform.position;
    }

    void Update()
    {
        Debug.Log(ElapsedTime);
        Debug.Log(ResetStart);
    }

    private IEnumerator ElapsedTimer()
    {
        if(ResetStart)
        {
            while(ElapsedTime < ResetTime && ResetStart)
            {
                ElapsedTime++;
                yield return new WaitForSeconds(1f); //Waits for 1 second before adding on to ElapsedTime
            }
        }
        else
        {
            ResetStart = false;
            yield return null;
        }

        if (ElapsedTime >= ResetTime)
        {
            Merchant.transform.position = DefaultSpawn.transform.position;
        }

        yield return null;
    }
}
