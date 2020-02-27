# CsvStringTable 사용가이드

적용스텝

1. 가장 상위 오브젝트에 StringTableManager를 넣는다
2. StringTable이 필요한 패널마다 StringTable을 넣어준다
3. 문자데이터를 적용하고싶은 곳에 전부 StringKey를 넣어준다
4. StringTable이 있는 오브젝트의 하위오브젝트가 아닌 곳에 StringKey를 적용시킬 경우 StringTableManager에 해당 StringKey를 따로 넣어준다
5. 각각 StringTableManager와 StringTable에서 데이터의 키값을 넣어준다.
6. EWcsv를 켜서 SetData버튼을 누르면 끝

------------------------------------------------------------------------------------------------------

EWcsv 각 버튼 설명
 - SetData : CsvFiles 안에 있는 파일들을 전부 StringData로 변환시키고, StringTableManager, StringTable, StringKey들을 찾아서 연결해준다
 - Setting : 데이터들은 건드리지 않고 StringTableManager, StringTable과 StringKey들만 찾아서 연결시켜준다.
 - ChangeLanguage : 
    1. Select Language : 코드에 설정한 Enum값을 토대로 적용할 언어 선택
    2. Change : 선택한 언어로 StringKey가 적용된 모든 Text들을 저장도니 데이터로 바꿔준다
 - Save : 예전버전에서 수정한 데이터값이 있을 경우에 수정된 데이터들을 따로 저장한다.
 
 ------------------------------------------------------------------------------------------------------
 
특정 폴더 설명
 - Data/CsvFiles : stringData로 변환시킬 csvFile들을 저장하는 곳
 - Data/ScriptableObjs : stringData로 변환된 파일들을 저장하는 곳
  
  ------------------------------------------------------------------------------------------------------
  
버전업데이트를 내려받아서 StringTableManager, StringTable, StringKey의 참조가 끊겼을 경우
 - 수정된 데이터가 있다면 Save버튼을 눌러 수정된 데이터들을 저장한다
 - Setting 버튼을 눌러서 다시 참조해준다
