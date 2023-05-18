using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCallback : MonoBehaviour
{
    private bool IsLoadFirstTime = true;

    private void Update()
    {
        if (IsLoadFirstTime)
        {
            IsLoadFirstTime = false;

            Loader.LoaderCallback();
        }

    }
}
