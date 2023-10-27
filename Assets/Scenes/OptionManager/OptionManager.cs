using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MyInputSystems;

public class OptionManager{
    private I_OptionUIUpdatable optionUIManager;
    private OptionLoader optionLoder;

    private Dictionary<E_OptionPage, Dictionary<E_OptionItem,int>> optionDataDic;


    public OptionManager (I_OptionUIUpdatable manager){
        optionUIManager = manager;
        optionLoder = new OptionLoader();

        //データの初期化
        var data = optionLoder.GetOptionData();
        optionUIManager.SetOptionData(data);

        optionUIManager.SubscribeExit((x) => {
            saveOption();
        });

    }

    public void ManagerUpdata(InputData[] input){
        //入力をUIマネージャに伝達
    }

    private void saveOption (){
        optionLoder.SaveOptionData(optionDataDic);
    }
}
