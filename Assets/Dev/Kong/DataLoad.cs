using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "DataLoad", menuName = "Scriptable Object/DataLoad_LoadTest")]
public class DataLoad : ScriptableObject
{

    [SerializeField]
    public string associatedSheet = "1mpe5Gjq7nO5HTYhLEZkTnNSrFAMFnCKSGZ0tyuJynds";
    public string associatedWorksheet = "Guns";

    //Item 데이터 읽어서 저장할 곳
    public List<Item> itemData = new List<Item>();

    //데이터 불러오고 다른곳에 저장하라 시킴
    private void Awake()
    {
        LoadDataCall(ProcessingData);
    }

    //API메소드 호출, 실행중엔 awake만 호출 해야함
    internal void LoadDataCall(UnityAction<GstuSpreadSheet> callback, bool mergedCells = false)
    {
        //라이브러리 호출
        SpreadsheetManager.Read(new GSTU_Search(associatedSheet, associatedWorksheet), callback, mergedCells);
    }

    //ss는 읽어온 모든 데이터를 갖고있는 구조체, 위에서 호출함
    internal void ProcessingData(GstuSpreadSheet sheetData)
    {
        //여기에서 sheetDat를 통해 가져온 내용을 재단할 수 있음
        //행교체 for문, 각 인덱스는 열을 나타냄, 행값은 1부터 존재하고, 1은 열의 분류를 나타내니 2부터 시작해야함
        for (int rows = 2; rows <= sheetData.rows.primaryDictionary.Count; rows++)
        {
            //기본 데이터 입력
            //새 아이템 인스턴스를 만듦. 지금 불러올 데이터의 메인 저장소.
            Item tmpItemData = new Item();
            //itemType을 옮겨 적음
            tmpItemData.BaseData.ItemType = sheetData.rows[rows][0].value;
            //name를 옮겨적음
            tmpItemData.BaseData.Name = sheetData.rows[rows][1].value;
            //Desc를 옮겨적음
            tmpItemData.BaseData.Desc = sheetData.rows[rows][2].value;
            //image를 옮겨적음, !!경로를 어떻게 저장할 지 약속되지 않아서 실제로 읽어와 저장하면 오류발생!!
            //tmpItemData.BaseData.Image = Resources.Load<Sprite>(sheetData.rows[rows][3].value);
                
            //인스턴스를 리스트에 등록함
            itemData.Add(tmpItemData);
            //로드 완료된 걸 확인해서 로그에 남긴다
            string logMessage = "item load, name: " + tmpItemData.BaseData.Name + ", Desc: " + tmpItemData.BaseData.Desc + ", ItemType: " + tmpItemData.BaseData.ItemType;

            //4번 인덱스의 값이 존재할 때 == ItemBulletData를 읽을때
            if (sheetData.rows[rows].Count >= 5 && sheetData.rows[rows][4].value != "")
            {
                //새 ItemBulletData 인스턴스를 만들고
                ItemBulletData itemBulletData = new ItemBulletData();
                //PartType을 옮겨적음
                itemBulletData.BulletBaseData.PartType = sheetData.rows[rows][4].value;
                //Damage를 옮겨적음
                itemBulletData.BulletBaseData.Damage = int.Parse(sheetData.rows[rows][5].value);
                //Percent를 옮겨적음
                itemBulletData.BulletBaseData.Percent = float.Parse(sheetData.rows[rows][6].value);
                
                //인스턴스를 메인 저장소에 등록
                tmpItemData.ItemData = itemBulletData;

                //그것도 로그에 남긴다
                logMessage += (" (and it has bullet Data) Damage: " + itemBulletData.BulletBaseData.Damage +
                           ", PartType: " + itemBulletData.BulletBaseData.PartType +
                           ", Percent: " + itemBulletData.BulletBaseData.Percent);
            }
            //로그로 발송
            Debug.Log(logMessage);
        }

    }


}
//------
//inspector창 구성
[CustomEditor(typeof(DataLoad))]
public class DataTest : Editor
{
    DataLoad data;

    //초기화
    void OnEnable()
    {
        data = (DataLoad)target;
    }

    //버튼 구성
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //버튼설명
        GUILayout.Label("Read Data Examples");

        //버튼 정의
        if (GUILayout.Button("Pull Data Method One"))
        {
            //버튼 클릭시 불러올애들
            data.LoadDataCall(data.ProcessingData);
        }
    }
}