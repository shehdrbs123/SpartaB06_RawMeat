# SpartaB06_RawMeat


![이미지](ExplainImage/게임시연.gif)

## 개요
- 상점 구현입니다.

## 개발 내용
- 1 구매하기
- 1-1 상점 리셋
- 1-2 아이템 구매
- 2 판매하기
- 2-1 상황별 아이템 보기
- 2-2 아이템 판매

### 기능 설명
- 1-1. 상점 초기설정 및 리셋
  <br> 게임 시작 시 아이템별로 상점에서 올라올 개수, 상대적 등장비율을 상점테이블에 저장 후
  <br>random을 이용하여 0~모든 등장비율을 더한 값중 한 값을 뽑아 리스트를 순회하여 인덱스를 추출한다
  <br>![화면 캡처 2023-09-01 131645](https://github.com/shehdrbs123/SpartaB06_RawMeat/assets/68540137/b356ff37-8c8b-40da-b4a4-c87d03ab471f)
  <br>추출한 인덱스를 이용해 상점 리스트에 데이터를 등록하는걸 지정한 반복횟수만큼 실행한다
  <br>리셋은 상점아이템 리스트를 클리어한후 위의 코드를 실행한다(random부터)
  
- 1-2. 아이템 구매
<br> 구매할 아이템 인덱스를 선택하면 상점 리스트에서 서칭 후 아이템을 가져온다.
<br>아이템의 개수가 2개 이상이라면 구매시 아이템의 갯수 또한 입력할 수 있도록 제어한다.
<br>상점에 등록된 아이템의 개수보다 많은 아이템을 구매하려고 하면 최대치로 자동으로 변환해준다.
<br>가진 골드보다 더 비싼 아이템을 구매하는 기능을 막고 낱개구매시 최대치로 구매되도록 제어한다.
<br>아이템별로 최대개수를 가지고있으며 최대개수를 초과할 시 아이템이 한개 더 생긴다(초과한 개수를 가진다).
<br>![화면 캡처 2023-09-01 131342](https://github.com/shehdrbs123/SpartaB06_RawMeat/assets/68540137/2ec1e896-dd4b-43a6-a8ca-4d3c77880e0d)
<br>구매한 아이템은 앞에서부터 정렬된다.
<br>즉 100개가 최대인 아이템의 1번칸에 97개가 있고 10개를 구매하면 1번칸에 100개 2번칸이 생성되고 7개의 구조
<br>![화면 캡처 2023-09-01 131524](https://github.com/shehdrbs123/SpartaB06_RawMeat/assets/68540137/57117362-3ef9-40e1-88d1-2dced0ca9f06)

- 2-1 상황별 아이템 보기
<br>장비아이템만, 소비아이템만, 전체아이템을 따로따로 볼 수 있도록 하는 기능을 만들었다.
<br>그에 맞춰 인덱싱이 된다.

- 2-2 아이템 판매
<br>상황별 아이템 보기에 따라 다른 인덱싱을 하기 때문에 서칭도 다른 방식으로 해야한다는 생각으로 접근.
<br>각 타입을 받아서 그 타입 내의 범위를 순회하여 인덱스를 찾는 방식을 이용했다. 
<br>![화면 캡처 2023-09-01 130838](https://github.com/shehdrbs123/SpartaB06_RawMeat/assets/68540137/52348385-be2d-4b51-8ca7-473893ff840c)
<br>^+전체타입 순회 방식
<br>판매시 아이템클래스의 남은 개수가 0이되면 아이템칸을 삭제하도록 구현했다.
<br>판매 시 아이템의 최대 갯수를 넘어가게 입력하면 최대 갯수로 자동맞춤되어 판매되고 골드가 반환된다
<br>![화면 캡처 2023-09-01 131127](https://github.com/shehdrbs123/SpartaB06_RawMeat/assets/68540137/347e6494-af19-487b-972d-485fa1fd941b)

