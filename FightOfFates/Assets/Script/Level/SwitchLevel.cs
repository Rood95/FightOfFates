﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("test");

        SceneManager.LoadScene(4);
    }

}
