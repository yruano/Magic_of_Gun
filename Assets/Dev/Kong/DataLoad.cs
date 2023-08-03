using GoogleSheetsToUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(fileName = "DataLoad", menuName = "Scriptable Object/DataLoad_LoadTest")]
public class DataLoad : ScriptableObject
{

    [SerializeField]
    public string associatedSheet = "1mpe5Gjq7nO5HTYhLEZkTnNSrFAMFnCKSGZ0tyuJynds";
    public string associatedWorksheet = "Guns";

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
        //행교체 for문, 각 인덱스는 열을 나타냄
        for (int rows = 2; rows <= sheetData.rows.primaryDictionary.Count; rows++)
        {
            Item it = new item();
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