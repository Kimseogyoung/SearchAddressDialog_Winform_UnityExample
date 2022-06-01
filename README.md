## About
**C# Winform library that provides place keyword search, address search (with. Example of using Unity)**<br>
장소 키워드 검색과 주소 검색을 제공하는 c# Winform 라이브러리 (with. 유니티 적용 예제)

## Requirements
more than .NET Framework 2.0


## Library Instructions 
1. Refer to the SA_Dialog.dll file for your project.<br>
    SA_Dialog.dll파일을 프로젝트에 참조하세요.


2. Register the Kakao Map api key. Be sure to do it at the top of the code.  <br>
    카카오 맵 api 키를 등록합니다. 반드시 코드의 맨위에서  수행하세요.
    ```
    FormManager.SetAPIKey("YOUR_KAKAOAPI_RESTFUL_APP_KEY");
    ```
#### Dialog Open
```
FormManager fm = new FormManager();
fm.OpenSearchForm();
```

#### ExitHandler
```
fm.OnExit += [YOUR EVENT HANDLER];
```
When you click the OK button in the dialog, register the event handler to run. <br>
You must declare a function that has the Locale class as a factor because it return the Locale class that you select.<br>
다이얼로그에서 확인 버튼을 클릭했을 때, 실행될 이벤트 핸들러를 등록하세요. <br>
사용자가 선택한 Locale 클래스를 전달하기 때문에 Locale 클래스를 인자로 갖는 함수를 선언해야합니다.


## Unity Example
An example of using an Address Search library to display Google Maps and characters moving and fighting on top of it.<br>
Google API APP key is required.<br>
주소 검색 라이브러리를 사용하여 구글맵을 표시하고 그 맵 위에서 캐릭터가 이동하고 전투하는 예제입니다.<br>
구글 api APP key 가 필요합니다<br>
