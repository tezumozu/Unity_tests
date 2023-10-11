using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using UnityEngine.AddressableAssets;

public class AreaLoader{
    private LinkedList <areaCacheData> loadedAreaList;
    private Dictionary <E_Area, LinkedListNode<areaCacheData> > areaCacheDic;
    private const int maxCacheLength = 5;


    public AreaLoader(){
        loadedAreaList = new LinkedList<areaCacheData>();
        areaCacheDic = new Dictionary<E_Area, LinkedListNode<areaCacheData>>();
    }


    public void initLoder(){
        loadedAreaList.Clear();
        areaCacheDic.Clear();
    } 


    public async UniTask<I_AreaUpdatable> LoadArea (E_Area areaName){

        //キャッシュにデータがあるか
        if(areaCacheDic.ContainsKey(areaName)){
            return await Task.Run(() => getCacheArea(areaName));

        }else{
            //なければ
            return await loadAreaResouse(areaName);
        }
    }


    //キャッシュヒット時の処理
    private I_AreaUpdatable getCacheArea(E_Area areaName){
        areaCacheData result = areaCacheDic[areaName].Value;

        //リストを更新する(lock)
            //値を一度削除し、
        loadedAreaList.Remove(areaCacheDic[areaName]);

            //先頭に挿入
        loadedAreaList.AddFirst(areaCacheDic[areaName]);

        return result.area;
    }


    //読み込み時の処理
    private async UniTask<I_AreaUpdatable> loadAreaResouse(E_Area areaName){
        I_AreaUpdatable result = null;

        //読み込み処理
        var handle  = Addressables.InstantiateAsync(Enum.GetName(typeof(E_Area),areaName));
        GameObject obj = await handle.Task;
        result = obj.GetComponent<I_AreaUpdatable>();

        //読み込まなければエラー
        if (result == null){
            Debug.Log("EROR! : " + areaName);

        }else{
            //Listに追加
            var node = loadedAreaList.AddFirst(new areaCacheData(result,areaName));
            areaCacheDic.Add(areaName,node);

            //現在のキャッシュ数を確認
            if(areaCacheDic.Count > maxCacheLength){
                //超えていたらデータを修正(lock)
                var lastNode = loadedAreaList.Last;
                loadedAreaList.RemoveLast();
                areaCacheDic.Remove(lastNode.Value.areaName);
            }
        }

        

        return result;
    }



    private class areaCacheData{
        public I_AreaUpdatable area;
        public E_Area areaName;

        public areaCacheData(I_AreaUpdatable area,E_Area areaName){
            this.area = area;
            this.areaName = areaName;
        }
    }
}
