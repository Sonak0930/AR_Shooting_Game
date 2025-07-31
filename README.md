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
## Enemy Spawn
https://github.com/user-attachments/assets/532ac959-c6b8-4f69-93ed-0ad6305db593

EnemyController spawns enemies regularly. 
When they are spawned, they move towards the player until they collide.
Collision is handled in the trigger box, and once a collision happens, it removes the gameobject from the list.
List<GameObject: Enemy> is dynamically stretched/shrunk when a new enemy is created/removed.

- EnemyController는 spawnTimer를 주기로 몬스터를 생성합니다.
- 몬스터는 플레이어에게 계속 다가가서 충돌합니다.
- 충돌이 일어나면, 해당 몬스터는 플레이어에게 데미지를 주고 사라집니다.
- 새로운 몬스터 생성/ 기존 몬스터 삭제가 일어날 때마다 Enemy List의 크기가 동적으로 변합니다.

<img width="1440" height="899" alt="image" src="https://github.com/user-attachments/assets/d74ca38f-dd2e-4668-a807-b67dc52c950e" />
Generate the enemy in a random range in a box with a width = maxRangeX, height = Y, and length = Z, which is centered at the player.

- 플레이어를 중심으로 하는 박스 안에서 랜덤한 위치에 enemy를 생성합니다.
https://github.com/Sonak0930/AR_Shooting_Game/blob/92744abf928dda9e7dcbd758942daf8d45765531/Assets/Scripts/GamePlay/EnemyController.cs#L55-L73

## Enemy Removal

Enemy Removal is done for a list and Destroy().

<img width="521" height="426" alt="image" src="https://github.com/user-attachments/assets/e3761f8f-7981-475e-a90c-060f1d20b0cb" />

1. Collision happens

<img width="574" height="328" alt="image" src="https://github.com/user-attachments/assets/7ffd5465-9a31-4871-97a3-409bb956705b" />

2. Remove the target enemy component from the list
   -> which makes the element become null
   
<img width="393" height="189" alt="image" src="https://github.com/user-attachments/assets/f400af19-111a-4a64-a3b2-17d608774060" />

3. Then remove all null elements in the list.
   -> This resizes the list, and only valid enemies are in the list.
   
<img width="355" height="231" alt="image" src="https://github.com/user-attachments/assets/8b70f63f-fa5d-450f-96a0-2c03d3e70249" />

5. As a last step, destroy the game object of the target.

https://github.com/Sonak0930/AR_Shooting_Game/blob/92744abf928dda9e7dcbd758942daf8d45765531/Assets/Scripts/GamePlay/EnemyController.cs#L154-L161

# Player Shot to Enemy

The player can shoot the enemy by touching the screen.
- 플레이어는 화면터치로 총알을 발사해 몬스터를 처치할 수 있습니다.
  
https://github.com/Sonak0930/AR_Shooting_Game/blob/7c2c11e0dd020af9571e50e74046c611777961d5/Assets/Scripts/GamePlay/PlayerRaycastToHittable.cs#L52-L67

When Enemy is shoot, it drops random item to make the game change
- 몬스터를 처치하면 랜덤아이템이 드랍되며, 해당 아이템은 게임에 다양한 변화를 줍니다.


# In-Game Items
## Clear Star

https://github.com/user-attachments/assets/8fd9f473-a235-45b6-9ff6-52ec9a1ff31e

Clear star removes all enemies in the scene.
- 별 아이템은 스테이지 내 모든 몬스터를 없앱니다.

https://github.com/Sonak0930/AR_Shooting_Game/blob/af2e22cef14624be23aa9d44a70eaddb49da9687/Assets/Scripts/GamePlay/EnemyController.cs#L104-L110

List<GameObject>: enemies are converted to a List because an IEnumerable object is not allowed to be modified at runtime.
- 동적으로 enemy prefab을 관리하려면, enemies를 List로 변환하는 과정이 필요했습니다.

<img width="1669" height="711" alt="image" src="https://github.com/user-attachments/assets/debf7ce5-8f9a-4940-992d-5755e1fc597f" />

Enemies.ToList() copies the list and iterates it instead of the original one.
Because IEnumerable Object (List) does not allow the collection to be modified at runtime, a copied list is used to prevent modification.
- ToList()를 사용하는 이유는, 기존 List는 IEnumerable하기 때문에 Iteration 도중에 수정할 수가 없습니다. 그래서 iteration에 사용할 별개의 list를 copy해서 삭제/생성 프로세스와 iteration이 독립적으로 일어나도록 했습니다.


## Acknowledgments
* Store not yet implemented (collect items ans coins/ buy guns)

- 상점과 아이템 기능은 아직 구현되지 않았습니다.
  

