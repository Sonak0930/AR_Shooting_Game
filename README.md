# Gathering Gun
Android-based AR shooting game

- 안드로이드 기반 AR 슈팅게임입니다.
# Getting Started

This project is a small project developed by three Ajou University students as a "Immersive Media Programming" class project.

- 이 프로젝트는 아주대학교 "몰입형 미디어 프로그래밍" 과목에서 진행한 미니 게임입니다. 
# Prerequisites
To bulid this project, PC and Android devices are needed.
- PC에서 빌드를 통해 안드로이드 기기에서 플레이 할 수 있습니다.
  
# Promotional Video
<img width="{100%}" src="https://user-images.githubusercontent.com/62582611/117240323-86216000-ae6b-11eb-9a6c-ba20287a2e05.gif"/>

# Deployment

Do not distribute without permission

# Role
3 members engaged in this team, and each member has a different role.
My role was to develop the EnemySpawn & Attack system and UI update based on GameData(Health and Score)

- 이 팀은 3명의 멤버로 구성되어 있고, 제가 맡은 역할은 몬스터 스폰,움직임 조정, 플레이어 공격, 그리고 UI 업데이트 였습니다.

# EnemyController



https://github.com/user-attachments/assets/532ac959-c6b8-4f69-93ed-0ad6305db593

EnemyController spawns enemies regularly. 
When they are spawned, they move towards the player until they collide.
Collision is handled in the trigger box, and once a collision happens, it removes the gameobject from the list.
List<GameObject: Enemy> is dynamically stretched/shrunk when a new enemy is created/removed.

- EnemyController는 spawnTimer를 주기로 몬스터를 생성합니다.
- 몬스터는 플레이어에게 계속 다가가서 충돌합니다.
- 충돌이 일어나면, 해당 몬스터는 플레이어에게 데미지를 주고 사라집니다.
- 새로운 몬스터 생성/ 기존 몬스터 삭제가 일어날 때마다 Enemy List의 크기가 동적으로 변합니다.

https://github.com/Sonak0930/AR_Shooting_Game/blob/92744abf928dda9e7dcbd758942daf8d45765531/Assets/Scripts/GamePlay/EnemyController.cs#L55C1-L73C6


## Acknowledgments
* Store not yet implemented (collect items ans coins/ buy guns)

- 상점과 아이템 기능은 아직 구현되지 않았습니다.
  

