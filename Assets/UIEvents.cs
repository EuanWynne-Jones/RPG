using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public Action<string> MyEvent;

    public void CallMyEvent(string ID)
    {
        if (MyEvent != null)
            MyEvent.Invoke(ID);
    }
}
