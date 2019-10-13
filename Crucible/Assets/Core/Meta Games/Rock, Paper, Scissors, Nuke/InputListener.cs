using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputListener : UnitySingleton<InputListener>
{
    void ChooseWeaponListen()
    {
        // player 1 choices


        // player 2 choices
    }

    IEnumerator ListenLoop(bool b)
    {
        yield return new WaitForSeconds(2);

    }
}
