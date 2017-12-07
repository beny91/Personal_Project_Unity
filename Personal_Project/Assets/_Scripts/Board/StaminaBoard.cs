using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBoard : BaseBoard {

    [SerializeField]
    UIProgressBar ProgressBar = null;
    [SerializeField]
    UILabel Label;
    public override eBoardType BOARD_TYPE
    {
        get
        {
            return eBoardType.BOARD_STAMINA;
        }
    }

    public override void SetData(string strKey, params object[] datas)
    {
        if (strKey.Equals(ConstValue.SetData_Stamina))
        {
            double maxStamina = (double)datas[0];
            double curStamina = (double)datas[1];

            ProgressBar.value = (float)(curStamina / maxStamina);
            //Label.text = curStamina.ToString() + " / " + maxStamina.ToString();
        }
        else
            base.SetData(strKey, datas);
    }
}
