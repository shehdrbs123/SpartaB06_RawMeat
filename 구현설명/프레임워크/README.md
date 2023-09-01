# SpartaB06_RawMeat


![이미지](ExplainImage/게임시연.gif)

## 개요
- 본 프로젝트는 스파르타코딩클럽 8기 "육회만먹는다"의 B6조 육회비빔밥 C# TextRPG 구현 포트폴리오입니다.

## 개발 개요
- 기간 : 2023.8.28 ~ 2023.9.1
- 개발 도구 : C#, Dotnet 7.0 FrameWork, Visual Studio
- 개발 내용 
  - 텍스트 RPG 게임 구현


## 역할

<table>
<tr>
  <td>이름</td><td>역할</td><td>적용기능</td>
</tr>
<tr>
  <td>노동균</td><td>팀장</td>
  <td>  
  게임 구조 설계 및 코어 제작, 입력예외처리<br>
  코드 통합, 디버깅, Git관리, 프로젝트 관리
  </td>
</tr>
<tr>
  <td>김대민</td><td>팀원</td>
  <td>
  데이터 가공 및 오브젝트 풀 구현<br>
  데이터 클래스 내 편의기능 제작<br>
  상점 기능 제작<br>
  텍스트 데이터 생산
  </td>
</tr>
<tr>
  <td>박지원</td><td>팀원</td>
  <td>
  전투 내 스킬 사용 부분 제작<br>
  인벤토리 표기, 아이템 장착 부분 제작<br>
  </td>
</tr>
<tr>
  <td>김하늘</td><td>팀원</td>
  <td>
  전투 부분 제작<br>
  </td>
</tr>
</table>


## 게임 설명

- 행동 선택형 TextRPG로, 숫자를 입력하여 행동을 선택, 행동이 반영되는 게임입니다.
### 게임의 목표
- 던전 공략을 통해 캐릭터를 성장시켜 던전의 최종 층을 정복하는 게임입니다.
- 캐릭터의 레벨업, 전직, 상점에서의 무기구입, 던전 전리품들을 통해서 캐릭터를 성장시키면 됩니다.
- 시연 동영상 링크
  - https://www.youtube.com/watch?v=x1ASmRxRd48

### 기능 설명
- 
  


## Github 사용원칙
- 하루 시작 시 Fetch Origin을 통해서 데이터 최신화 (필수!!!)
- 작업 시 브랜치 확인을 통해 겹치는 파일, 기능이 없는 지 체크 
  - https://github.com/shehdrbs123/SpartaB06_RawMeat/branches
- 코드 수정 시 브랜치 생성 필수
- 브랜치 생성 시 현재 수정 중인 코드 파일, 내용에 대한 간략한 설명 추가
  - 예시
    - Player.cs-플레이데이터추가
    - 여러 파일이 예상될 경우, Player.cs,StatusScene.cs-statusScene추가 
- Pull Request를 통한 브랜치 머지 요청
