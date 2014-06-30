# Hive5 SDK for Unity

아쉽게도, 아직 제대로 된 SDK 패키지가 아닙니다.
SDK를 포함한 Unity 샘플 프로젝트입니다.

Assets/Hive5 폴더만 복사하여 자신의 Unity 게임에 덮어쓰기 하시면
SDK를 이용하실 수 있습니다.


## Specification
[API Specification](http://dev.hive5.io/docs/unity/apidoc/index.html)



## 환경
•Unity 4.5 이상


## Unit Test on Visual Studio

### 필요한 환경
- Visual Studio 2013 ([Express 버전](http://www.microsoft.com/ko-kr/download/details.aspx?id=40787)은 무료입니다) 
- Unity Editor

### 테스트 실행하는 방법

1. maui-sdk.test.sln 파일을 더블클릭하거나 Visual Studio에서 File - Open Project를 통해서 엽니다.
2. 솔루션 탐색기(Solution Explorer) 창에서 maui-sdk.test 프로젝트 트리를 확장하고 Hive5UnitTest.cs를 엽니다.
3. 테스트 탐색기(Test Explorer)에서 Run All 등을 이용해서 실행합니다.
  - 테스트 탐색기가 보이지 않는 분은 [Test - Windows - Test Explorer]를 누르거나, 소스코드 중 [Test Method]란 Attribute로 장식된 메서드를 찾아 메서드 본문 아무 곳에나 커서를 이동시켜 놓고, Ctrl + R +T 를 눌러 단일 메서드 테스트를 수행하면 저절로 Test Explorer가 등장합니다.  
4. 단일 메서드 테스트 수행은 Test Explorer에서 해당 테스트에 오른쪽 클릭 후 [Run Selected Tests]를 선택하거나, 해당 메서드 본문에 커서를 위치시킨 후 Ctrl + R + T하여 테스트를 실행할 수 있습니다.

### 테스트 실행 시 빌드에러 발생한 경우
테스트를 처음 열어서 실행하려고 하는 경우, Unity Editor가 설치된 개발 환경이라 하더라도, Unity관련 참조들이 끊겨 있는 경우가 있습니다. 이럴 땐 소스 중 Assets\Hive5.unity를 Unity Editor에서 한번 열었다가 닫아주시면 손상된 참조들이 부활되는 놀라운 광경을 목격하실 수도 있습니다.


## See also
[SDK 사용가이드](https://github.com/bytecodelab/maui-sdk/wiki/Guide%20for%20Unity)


## 라이선스

### Websocket-Sharp

스파이더에서 웹소켓에 인터페이스 하기 위해서 MIT License인 Websocket-Sharp을 사용하고 있습니다.
Websocket-Sharp은 아래 링크를 통해 배포되고 있습니다.
[https://github.com/sta/websocket-sharp/](https://github.com/sta/websocket-sharp/)
