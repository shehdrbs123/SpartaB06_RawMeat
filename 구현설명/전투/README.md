# SpartaB06_RawMeat


![이미지](ExplainImage/게임시연.gif)

## 개요
전투 관련 씬들 구현 내용 입니다

## 씬 목록

<table>
<tr>
  <td>이름</td><td>씬 기능</td><td>구현목록</td>
</tr>
<tr>
  <td>BattleScene</td><td>전투 씬</td>
  <td>  
  적이 없을 때(클리어) 새로운 던전 층 생성
  적 정보, 플레이어 정보, 공격/스킬/아이템/나가기 선택지
  </td>
</tr>
  <tr>
  <td>BattleSkillSelectScene</td><td>스킬 선택 씬</td>
  <td>  
  스킬 정보 및 스킬 선택 기능
  </td>
</tr>
<tr>
  <td>BattleSelectScene</td><td>적 선택 씬</td>
  <td>
  BattleScene에서 공격이나 BattleSkillSelectScene에서 적 지정 스킬 사용 시 대상 적 선택
  </td>
</tr>
<tr>
  <td>RealBattleScene</td><td>전투로그 씬</td>
  <td>
  플레이어의 선택에 따른 전투 결과를 최종 출력
  몬스터와 플레이어의 공,방 치명, 회피에 따른 전투 결과를 계산
  </td>
</tr>
<tr>
  <td>RewardScene</td><td>전투 보상 씬</td>
  <td>
  모든 몬스터 처치 시 클리어 보상 획득 및 출력
  </td>
</tr>
</table>

### 전투기능 설명
메인 씬에서 전투 선택 시 BattleScene으로 이동합니다
만약에 적이 없다면 새로운 던전 층을 생성하고
살아있는 몬스터들의 정보와 플레이어 정보 출력 후
행동 선택지를 제시합니다

스킬을 선택하면 BattleSkillSelectScene으로 이동합니다
사용할 수 있는 스킬들의 정보가 표시되고 선택할 수 있습니다
선택한 스킬이 대상을 지정할 수 없는 스킬이라면 RealBattleScene으로
대상 지정이 가능하면 BattleSelectScene으로 이동합니다

BattleScene에서 공격이나 BattleSkillSelectScene에서 대상 지정 스킬을 골랐다면
BattleSelectScene으로 이동하여 대상 적을 선택합니다
적을 선택했으면 RealBattleScene으로 이동하여 실제 전투 로그를 출력합니다

몬스터가 모두 죽게 되면 RealBattleScene에서 RewardScene으로 이동합니다
RewardScene에서는 전투의 보상을 출력하고 메인 씬으로 돌아갈지 전투 씬으로 돌아갈지 선택합니다
