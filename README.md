<div align=center> 

<h1>BamamaMan :banana:</h1>
PC 플레이 Altf4 류 의 플랫포머 게임(platformer game)<br>
언제나 갇혀서 바깥의 넓은 하늘을 선망하던 바나나맨... 당신은 이 바나나맨과 함께 좁은 집을 탈출하여 넓은 하늘을 선사해주자!
봉제인형(Ragdoll)인 바나나맨은 움직임이 서툴다 바나나맨의 조작법을 잘 숙지하도록! :mask: <br>
<br>

<img src="https://github.com/user-attachments/assets/71f453bf-d019-4fe4-94f0-902e17df6fe0">
</div>

## :calendar: 목차
  1. [개요](#page_with_curl-개요)
  2. [플레이 영상](#movie_camera-플레이-영상)
  3. [실행 방법](#memo-실행방법)
  4. [게임 설명](#video_game-게임설명)
  5. [게임 정보](#mag_right-게임정보)
  6. [트러블 슈팅](#loop-트러블슈팅)

## :page_with_curl: 개요
 - 프로젝트 이름: BananaMan
 - 개발 기간: 2024.04.05-2024.07.08
 - 개발 목적 및 동기: <br><br>
 3D 캐릭터의 자연스러운 애니메이션 연결, Ragdoll 설정, Occlusion Culling 을 사용한 렌더링 최적화 기능 학습과 <br>
 키 설정 및 리스폰 기능 구현을 위한 Json파일 사용하기 위해 처음 만든 3D 게임을 보완하고자 제작을 결심
 
 - 개발 엔진 및 사용언어: <img src="https://img.shields.io/badge/unity-000000?style=for-the-badge&logo=unity&logoColor=white"> / <img src="https://img.shields.io/badge/-C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white">
 - :file_folder: [프로젝트 설명 PPT 다운로드](https://drive.google.com/uc?export=download&id=1OzXAbZqREu52tX2_CVcHeBXUAiSS7q3E)

## :movie_camera: 플레이 영상
[▶ 영상 보기](https://github.com/user-attachments/assets/ec17c5e1-15d5-498c-a8d3-b114a2e7a31a)

<details>
<summary>기존 프로젝트 보기</summary>
  
--------------------------------------------------------------------------------------------------------------------------------
<div align=center> 
 :x: 기존 프로젝트 내용 수정, Roll A Ball 의 스크립트 및 개발 파일들 현재 존재하지 않습니다. :x:
</div>
<br>

- 프로젝트 이름 : Roll A Ball
- 개발 기간 : 2023.02.02-2023.02.18
- 개발 엔진 및 사용언어: <img src="https://img.shields.io/badge/unity-000000?style=for-the-badge&logo=unity&logoColor=white"> / <img src="https://img.shields.io/badge/-C%23-512BD4?style=for-the-badge&logo=csharp&logoColor=white"> 

<div align=center> 
  
|<img src="https://github.com/y636367/BananaMan/assets/63005842/6dcc3e4c-2f7b-4bb9-8b28-d0543710ab46" width="800" height="480"/>|
|---|
</div>

--------------------------------------------------------------------------------------------------------------------------------
</details>

## :memo: 실행방법

  ### BananaMan
 1. :file_folder: [게임 다운로드 링크](https://drive.google.com/file/d/1E3ltclpDpQTaBOfn1ZGZVAavqFVBRf4x/view?usp=sharing)
 2. 위 링크를 클릭하여 'BananaMan' 파일을 다운로드
 3. 압축을 해제 후 'BananaMan.exe' 실행 

  ### Roll A Ball
 1. :file_folder: [게임 다운로드 링크](https://drive.google.com/file/d/1gkdSVzmIfa6w2oiV8R3KuQPSgjxxTKyV/view?usp=sharing)
 2. 위 링크를 클릭하여 'Roll A Ball' 파일을 다운로드
 3. 압축을 해제 후 'Roll A Ball.exe' 실행 

 ## :video_game: 게임설명
<details>
<summary>게임 설명 보기</summary>

<div align=center>    

|<img src="https://github.com/y636367/BananaMan/assets/63005842/5b381ee5-8bbe-4fde-9e20-8b1384f4d06b" width="400" height="240"/>|<img src="https://github.com/user-attachments/assets/df2ea5fb-809f-423f-991b-f224141593ef" width="400" height="240"/>|
|---|---|
|<div align=center>타이틀 화면</div>|<div align=center>게임 시작 버튼</div>|

게임 시작 - [새로운 게임, 이어하기] , 설정, 종료 3가지 버튼을 통해 조작 가능합니다.<br>
화면의 캐릭터를 클릭 할 시 상호작용하여 랜덤한 애니메이션이 재생되도록 구현 하였습니다. <br>
게임 시작 버튼을 클릭 후 '이어하기' 와 '새로운 게임'을 선택해 플레이 할 수 있으며<br>
'새로운 게임'을 선택 한 경우 기존의 게임 데이터가 있었다면 덧 씌워지게 됩니다.<br>

</div>

  - ### 옵션 화면

<div align=center>    
 
|<img src="https://github.com/y636367/BananaMan/assets/63005842/8bb939ba-a7ad-4d1f-9b3c-09960d36d5bf" width="400" height="240"/>|<img src="https://github.com/y636367/BananaMan/assets/63005842/1aaa4295-1f06-4477-829f-2d4136efb1c3" width="400" height="240"/>|
|---|---|
|<div align=center>키 설정 화면</div>|<div align=center>설정 화면</div>|

키 설정 화면에서 원하는 키에 액션을 할당 할 수 있습니다. 또한 설정값을 저장하여 다음에 게임을 실행 할시<br>
저장한 설정값을 그대로 유지한 채 플레이 할 수 있습니다.<br>
BGM, SFX 의 크기 조절과 현재 플레이 중인 화면의 해상도, 전체 화면 여부를 변경 할 수 있습니다.<br>

</div>

</details>

## :mag_right: 게임정보
<details>
<summary>게임 정보 보기</summary>

<div align=center>   
  
|<img src="https://github.com/y636367/BananaMan/assets/63005842/db7a3827-10ee-4b9d-bc85-80e561ad79dd" width="400" height="240"/>|<img src="https://github.com/y636367/BananaMan/assets/63005842/33b87178-f4e8-467c-aa66-462b8a73e7ed" width="400" height="240"/>|
|---|---|
|<div align=center>튜토리얼 스테이지 플레이 화면1</div>|<div align=center>튜토리얼 스테이지 플레이 화면2</div>|

첫 시작 시 튜토리얼 스테이지를 플레이 하면서 캐릭터의 조작법에 대하여 익힐 수 있습니다.<br>
(이동, 달리기, 점프, 래그돌 등의 조작을 익힐 수 있습니다.) <br><br>

|<img src="https://github.com/user-attachments/assets/9a41fc3c-696c-4659-8e18-566b07ed9185" width="400" height="240"/>|<img src="https://github.com/y636367/BananaMan/assets/63005842/d9abaac5-73d5-4623-bb39-57af20b7a0ac" width="400" height="240"/>|
|---|---|
|<div align=center>스테이지 플레이 화면1</div>|<div align=center>스테이지 플레이 화면2</div>|
|<img src="https://github.com/y636367/BananaMan/assets/63005842/b03a743c-fd43-40b1-8e97-6c9ba544d9a0" width="400" height="240"/>|<img src="https://github.com/y636367/BananaMan/assets/63005842/77dc015a-a14a-4d2f-a07d-20388f383a0d" width="400" height="240"/>|
|<div align=center>스테이지 플레이 화면3</div>|<div align=center>스테이지 플레이 화면4</div>|

</div>

</details>

## :loop: 트러블슈팅
<details>
<summary>트러블슈팅 보기 (카메라 거리 조절, 캐릭터 낙하)</summary>
  
  - ### 카메라 거리 조절 문제
<br>
<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/d0d2e0c2-3244-4406-99f3-1b40899bc5c6" width="400" height="240"/>|<img src="https://github.com/user-attachments/assets/f71af9f7-0e3b-47c3-b572-1139b61386e7" width="400" height="240"/>|
|---|---|

</div>
<br>

 - 문제 : 캐릭터와 카메라 사이 다른 오브젝트가 존재 할 시 카메라가 튀는 문제 발생
 - 원인 : Raycast로 카메라 시야에 검출되는 오브젝트 존재 여부 파악 코드 미 설계, Layer로 오브젝트 관계 미 정리
 - 해결 : 오브젝트들을 Layer별로 정리하여 구분 짓고 카메라에 Raycast를 부착하여 검출되지 않기 바라는 오브젝트 정리하고<br>
캐릭터 추적용 오브젝트를 캐릭터 안에 심어 카메라가 이를 추적하도록 설정

<br>

  - ### 낙하 시 경계면 인식 문제
<br>
<div align=center> 
  
|<img src="https://github.com/user-attachments/assets/666a3306-f06c-40d4-ad3a-9d21f87c8c6d" width="400" height="240"/>|<img src="https://github.com/user-attachments/assets/9f1bdfa3-8e01-4046-8142-6f831d298c73" width="400" height="240"/>|
|---|---|

</div>
<br>

 - 문제 : 캐릭터가 오브젝트의 모서리면(경계면)에 낙하 시 지면을 인식하지 못하고 무한 추락 판정 되는 문제 발생
 - 원인 : 기존의 Raycast 지면 검출 방식 코드의 범위 미 확장, 캐릭터 현 상태에 따른 탈출 함수의 부제
 - 해결 : 캐릭터 하단 Raycast를 확장 하여 추가 검출할 수 있도록 범위를 확장 하였고, 캐릭터의 장시간 특정 상태 유지 시<br>
확장된 Raycast로 검출된 지면으로 강제 탈출되도록 함수 구현

</details>
