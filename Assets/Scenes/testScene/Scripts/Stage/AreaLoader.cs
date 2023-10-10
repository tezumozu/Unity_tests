using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;

public class AreaLoader{
    private LinkedList <I_AreaUpdatable> loadedAreaList;
    private Dictionary <E_Area, LinkedListNode<I_AreaUpdatable> > areaCacheDic;
    private const int maxCacheLength = 5;


    public AreaLoader(){
        loadedAreaList = new LinkedList<I_AreaUpdatable>();
        areaCacheDic = new Dictionary<E_Area, I_AreaUpdatable>();
    }


    public void initLoder(){

    } 


    public async UniTask<I_AreaUpdatable> loadArea (E_Area areaName){
        I_AreaUpdatable result;

        //キャッシュにデータがあるか
        if(areaCacheDic.ContainsKey(areaName)){
            areaData = getCacheArea;

        }else{
            //なければ
            areaData = await loadAreaResouse(areaName);
        }

        return result;
    }


    public I_AreaUpdatable getCacheArea(E_Area areaName){
        I_AreaUpdatable result = areaCacheDic[areaName];

        return result;
    }


    public async UniTask<I_AreaUpdatable> loadAreaResouse(E_Area areaName){
        I_AreaUpdatable result;

        //読み込まなければエラー
        if (result == null){

        }

        //現在のキャッシュ数を確認
        if(areaCacheDic.Count-1 > maxCacheLength){
            //超えていたらデータを修正

        }


    }
}
