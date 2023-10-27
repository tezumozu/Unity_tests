using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

using UnityEngine.AddressableAssets;

public class GenericLoader<T,E> 
where E : Enum {
    private LinkedList <genericCacheData<T,E>> loadedAsetList;
    private Dictionary <E, LinkedListNode<genericCacheData<T,E>> > asetCacheDic;
    private readonly int maxCacheLength;


    //キャッシュ有りのパターン
    public GenericLoader(int num){
        loadedAsetList = new LinkedList<genericCacheData<T,E>>();
        asetCacheDic = new Dictionary<E, LinkedListNode<genericCacheData<T,E>>>();

        maxCacheLength = num;
    }

    public void initLoder(){
        loadedAsetList.Clear();
        asetCacheDic.Clear();
    } 


    public async UniTask<T> LoadAset (E asetName){

        //キャッシュにデータがあるか
        if(asetCacheDic.ContainsKey(asetName)){
            return getCacheAset(asetName);

        }else{
            //なければ
            return await loadResouseForCache(asetName);
        }
 
    }


    //キャッシュヒット時の処理
    private T getCacheAset(E asetName){
        genericCacheData<T,E> result = asetCacheDic[asetName].Value;

        //リストを更新する(lock)
            //値を一度削除し、
        loadedAsetList.Remove(asetCacheDic[asetName]);

            //先頭に挿入
        loadedAsetList.AddFirst(asetCacheDic[asetName]);

        return result.Aset;
    }


    //キャッシュ未使用で読み込む スタティックメソッド
    public static async UniTask<T> loadAsetResouse (E asetName){
        T result;

        //読み込み処理
        var handle  = Addressables.InstantiateAsync(Enum.GetName(typeof(E),asetName));
        GameObject obj = await handle.Task;
        result = obj.GetComponent<T>();

        //読み込まなければエラー
        if (result == null){
            Debug.Log("EROR! : " + asetName);
        }

        return result;
    }


    //キャッシュ使用
    private async UniTask<T> loadResouseForCache(E asetName){
        T result;

        //読み込み処理
        var handle  = Addressables.InstantiateAsync(Enum.GetName(typeof(E),asetName));
        GameObject obj = await handle.Task;
        result = obj.GetComponent<T>();

        //読み込まなければエラー
        if (result == null){
            Debug.Log("EROR! : " + asetName);

        }else{
            //Listに追加
            var node = loadedAsetList.AddFirst(new genericCacheData<T,E>(result,asetName));
            asetCacheDic.Add(asetName,node);

            //現在のキャッシュ数を確認
            if(asetCacheDic.Count > maxCacheLength){
                //超えていたらデータを修正(lock)
                var lastNode = loadedAsetList.Last;
                loadedAsetList.RemoveLast();
                asetCacheDic.Remove(lastNode.Value.AsetName);
            }
        }

        return result;
    }



    private class genericCacheData<t,e>
    where e : Enum {
        public t Aset;
        public e AsetName;

        public genericCacheData(t aset,e asetName){
            Aset = aset;
            AsetName = asetName;
        }
    }
}

