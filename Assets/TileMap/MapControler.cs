using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Linq;

public class MapControler : MonoBehaviour
{
	/// <summary>
	/// マップチップのサイズ 
	/// </summary>
	[SerializeField]
	private Vector2 _mapchpSize;
	/// <summary>
	/// csvテキストデータ 
	/// </summary>
	[SerializeField]
	private TextAsset _csvText;
	/// <summary>
	/// マップチップのプレハブ 
	/// </summary>
	[SerializeField]
	private Mapchip _mapchipPrefab;
	/// <summary>
	/// 番号と画像が紐づいたリスト 
	/// </summary>
	[SerializeField]
	private List<SpriteData> _spriteDataList;

	/// <summary>
	/// パースされたデータが入るリスト
	/// </summary>
	private List<string[]> _csvDatas = new List<string[]> ();

	/// <summary>
	/// 画像番号からデータを取り出すディクショナリー 
	/// </summary>
	private Dictionary<int,SpriteData> _spriteNumToData = new Dictionary<int, SpriteData> ();

	// Use this for initialization
	private void Start ()
	{
		//csvの中身をログで表示
		Debug.Log (_csvText.text);
		//ListをDictionaryに変換
		_spriteNumToData = _spriteDataList.ToDictionary (k => k.num, v => v);
		//パースする
		CSVParser (_csvText.text);
		//１行ずつ読み出す
		for (int i = 0; i < _csvDatas.Count; i++) {
			//１行分
			string[] strArray = _csvDatas [i];
			//y軸
			int y = i;
			//１行からカンマ区切りで分割された文字を取り出す
			for (int j = strArray.Length - 1; j > 0; j--) {
				//１文字分
				string s = strArray [j];
				//x軸
				int x = j;
				//-1なら何もしない
				if (s == "-1") {
					continue;
				} else {
					//画像番号(文字列を数値に変換）
					int spriteNum = int.Parse (s);
					//マップチップ生成
					Mapchip mapchipObj = Instantiate (_mapchipPrefab, transform);
					//位置を決定
					mapchipObj.transform.localPosition = new Vector2 (x * _mapchpSize.x, y * _mapchpSize.y);
					//画像サイズを調整
					mapchipObj.GetComponent<RectTransform> ().sizeDelta = _mapchpSize;
					//コライダーのサイズを調整　
					mapchipObj.GetComponent<BoxCollider2D> ().size = _mapchpSize;
					//データ取得
					SpriteData data = _spriteNumToData [spriteNum];
					//初期化
					mapchipObj.Initialize (data.sprite);
					//レイヤー設定
					mapchipObj.gameObject.layer = data.layerMask;
				}
			}
		}
	}

	/// <summary>
	/// CSVをパースする関数 
	/// </summary>
	private void CSVParser (string csvText)
	{
		//どんな処理が行われているかきちんと調べておきましょう
		StringReader reader = new StringReader (csvText);
		int height = 0; // CSVの行数
		while (reader.Peek () > -1) {
			string line = reader.ReadLine ();
			_csvDatas.Add (line.Split (',')); // リストに入れる
			height++; // 行数加算
		}
	}

	/// <summary>
	/// 敵生成の関数 
	/// </summary>
	private void CreateEnemy ()
	{
		//==== 敵生成のサンプル===//	
		//表示させる範囲
		Vector2 range = new Vector2 (-10, 10);
		//敵を表示する位置を生成
		Vector3 pos = new Vector3 (
			              Random.Range (range.x, range.y),
			              Random.Range (range.x, range.y),
			              Random.Range (range.x, range.y)
		              );
		//エネミーを出現させる予定の位置
		Vector3 enemyPos = default(Vector3);
		//プレイヤーの位置
		Vector3 playerPos = default(Vector3);
		//距離を測る
		float length =	Vector3.Distance (enemyPos, playerPos);
		//距離が一定以上なら
		if (length > 10) {
			GameObject obj = Instantiate (gameObject);
			obj.transform.position = pos;
		}
		//===================//
	}

}