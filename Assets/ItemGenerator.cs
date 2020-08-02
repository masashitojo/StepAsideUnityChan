using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour {
	//carPrefab
	public GameObject carPrefab;
	//coinPrefab
	public GameObject coinPrefab;
	//cornPrefab
	public GameObject conePrefab;
	//スタート地点
	private int startPos=-160;
	//ゴール地点
	private int goalPos=120;
	//アイテムを出すx	方向のはんい
	private float posRange=3.4f;

	//課題(追加)
	//coneオブジェクト配列(最大6×19個)
	private GameObject[] coneArray= new GameObject[114];
	//coinオブジェクト配列(最大3×19個)
	private GameObject[] coinArray= new GameObject[57];
	//carオブジェクト配列(最大3×19個)
	private GameObject[] carArray= new GameObject[57];
	//coneオブジェクト配列 index変数
	private int coneIndex = 0;
	//coinオブジェクト配列 index変数)
	private int coinIndex = 0;
	//carオブジェクト配列 index変数
	private int carIndex = 0;
	//カメラのオブジェクト
	private GameObject camera;
	//発展課題(追加)
	//unityちゃんのオブジェクト
	private GameObject unitychan;
	//直前のz方向の位置を保持
	private float prePos;

	// Use this for initialization
	void Start () {
		//カメラのオブジェクトを取得 //課題(追加)
		this.camera=GameObject.Find("Main Camera");
		//unityちゃんのオブジェクトを取得
		this.unitychan=GameObject.Find("unitychan");
		//スタート地点に設定
		this.prePos=this.startPos;
			
		//初期時に40m～50m程度先までアイテム生成//発展課題(追加)
		for(int i=startPos;i<=startPos+45;i+=15){
			//どのアイテムをだすのかランダム設定
			int num=Random.Range(1,11);
			if (num <= 2) {
				//コ－ンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
					this.coneArray [coneIndex] = cone;
					this.coneIndex++;
				}
			} else {
				//レーンごとにアイテムを生成
				for(int j=-1; j<=1; j++){
					//アイテムの種類を決める
					int item = Random.Range(1,11);
					//アイテムを置くz座標のオフセットをランダムに設定
					int offsetZ=Random.Range(-5,6);
					//60%コイン:30%車:10%何もなし
					if(1<=item && item<=6){
						//コインを生成
						GameObject coin= Instantiate(coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);
						this.coinArray [coinIndex] = coin;
						this.coinIndex++;
					} else if(7<=item && item<=9){
						GameObject car = Instantiate(carPrefab) as GameObject;
						car.transform.position=new Vector3(posRange*j, car.transform.position.y , i+offsetZ);
						this.carArray [carIndex] = car;
						this.carIndex++;
					}
				}
			}
		}



	}


	// Update is called once per frame
	void Update () {
//		//発展課題(アイテムの動的生成)
		//現在のunityちゃんから40m程度先の位置にアイテムを設定する
		float setPos = this.unitychan.transform.position.z + 40f;
		//unityちゃんが15m進むたびに40m程度先のアイテムを設定する
		if ((this.unitychan.transform.position.z> startPos) && (this.unitychan.transform.position.z - this.prePos >= 15) && (setPos<=goalPos)) {
			//現在のz方向の位置を保持
			this.prePos = this.unitychan.transform.position.z;
			//どのアイテムをだすのかランダム設定
			int num=Random.Range(1,11);
			if (num <= 2) {
				//コ－ンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, setPos);
					this.coneArray [coneIndex] = cone;
					this.coneIndex++;
				}
			} else {
				//レーンごとにアイテムを生成
				for(int j=-1; j<=1; j++){
					//アイテムの種類を決める
					int item = Random.Range(1,11);
					//アイテムを置くz座標のオフセットをランダムに設定
					int offsetZ=Random.Range(-5,6);
					//60%コイン:30%車:10%何もなし
					if(1<=item && item<=6){
						//コインを生成
						GameObject coin= Instantiate(coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, setPos + offsetZ);
						this.coinArray [coinIndex] = coin;
						this.coinIndex++;
					} else if(7<=item && item<=9){
						GameObject car = Instantiate(carPrefab) as GameObject;
						car.transform.position=new Vector3(posRange*j, car.transform.position.y , setPos+offsetZ);
						this.carArray [carIndex] = car;
						this.carIndex++;
					}
				}
			}
		}



		//課題(アイテムの破棄)
		//画面を通り過ぎたコ－ンを破棄
		foreach(GameObject i in coneArray){
			if(i!=null && i.transform.position.z < camera.transform.position.z){
				Destroy (i);
			}
		}
		//画面を通り過ぎたコインを破棄
		foreach(GameObject i in coinArray){
			if(i!=null && i.transform.position.z < camera.transform.position.z){
				Destroy (i);
			}		
		}
		//画面を通り過ぎた車を破棄
		foreach(GameObject i in carArray){
			if(i!=null && i.transform.position.z < camera.transform.position.z){
				Destroy (i);
			}		
		}



	}


}
