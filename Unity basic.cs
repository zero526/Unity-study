Unity 기본 골드메탈 유니티 기초강화 1~14 中

카메라 컨트롤 Q W E R T + Mouse

콘솔 분리

프로젝트 -> 우클릭 -> 생성 -> C#스크립트 -> 오브젝트 클릭 -> 인스펙터에 옮기기

Debug.Log("hello"); //콘솔창에 출력

void Awake(){
  //게임 오브젝트 생성 시, 최초 실행, 한번만 실행
}

void OnEnalbe(){
  //게임 오브젝트가 활성화 되었을 때 실행
}

void Start(){
  //업데이트 시작 직전에 한번만 실행
}

void FixedUpdate(){
  //물리 연산 업데이트, 물리 연산하기 전에 실행, 고정된 실행주기로 반복적으로 계속 실행
}

void Update(){
  //게임 로직 업데이트, 물리연산 제외 주기적으로 변하는 로직 입력
}

void LateUpdate(){
  //모든 업데이트가 끝난 후 마지막으로 호출, 보통 캐릭터를 따라가는 카메라, 후처리 등
}

void OnDisable(){
  //오브젝트 비활성화 되었을 때 실행
}

void OnDestroy(){
  //오브젝트 삭제될 때 실행
}


<<Input Class>>
//Unity에서 키보드 마우스 등의 입력을 컨트롤 ex)Input.GetKeyDown(KeyCode.S);
anyKeyDown anyKey anyKeyup  //bool 반환, 키보드 누를때 누르고있을때 키보드뗄때
GetKeyDown() GetKey() GetKeyUp() //bool 반환, 파라미터로 KeyCode.~~ [ex)KeyCode.Return = 엔터 / KeyCode.LeftArrow = 화살표 왼쪽 등]
GetMouseButtonDown() GetMouseButton() GetMouseButtonUp() //bool 반환, 파라미터로 0 = 좌클릭, 1 = 우클 

에디터 -> 프로젝트 매니저 -> 인풋매니저 //입력 사용자 지정 설정 KeyCode.~~ 대신 "Jump", "Horizontal" 등으로 대체 가능

Horizontal, Vertical 에만 적용
GetAxis()  //수평, 수직 입력 받으면 0~1의 값을 가지는 float 반환, 가중치가 있어 꾹 누르면 0에서 1까지 상승
GetAxisRaw()  //가중치 없이 입력받으면 -1, 0, 1 반환 (왼쪽 -1, 양쪽 0, 오른쪽 1)


<<Transform Class>>
//오브젝트 이동에 사용, 오브젝트는 변수 transform을 항상 기본적으로 갖고 있음
Translate() //오브젝트 파라미터 값의 방향과 크기 만큼 이동
ex) Vector3 vec = new Vector3(1, 2, 3); transform.Translate(vec);

<<vector3 Class>>
MoveTowards()  //등속이동, 파라미터로 현재위치, 목적지, 이동 속도 
//ex)Vector3 target = new Vector3(8, 1.5f, 0); transform.position = Vector3.MoveTowards(transform.position, target, 2f);
SmoothDamp() //파라미터로 현재위치, 목적지, 참조 속도, 이동 속도, 참조속도는 보통 Vector3.zero
Lerp() //선형 보간, 파라미터로 현재위치, 목적지, 이동 속도
SLerp()  //구면 성형 보간, 포물선으로 이동, 파라미터로 현재위치, 목적지, 이동 속도

Time.deltaTime  //프레임에 따른 업데이트 주기 불균형을 보완하기 위해 사용
/*ex)
transform.Translate(Vec * Time.deltaTime);
Vector3.Lerp(Vec1, Vec2, T * Time.deltaTime);
*/




컴퓨넌트 ≒ 속성 
오브젝트 -> 컴포넌트 추가 -> RigidBody  //물리효과를 받기 위한 컴포넌트
RigidBody
Mass = 무게
Use Gravity = 중력 적용
Is Kinematic = 외부 물리 효과 무시

Sphere Collider  //충돌 효과, 실제 보이는것보다 크거나 작게 출동 면적 정할수있음

프로젝트 -> 생성 -> 머터리얼... -> 오브젝트에 끌어 넣기
알베도 = 색상  //알베도 왼쪽 빈칸에 사진 넣으면 적용 가능
이미션 = 발광
타일링 = 같은 무늬 반복 패턴 생성


프로젝트 -> 생성 -> 물리 머터리얼 -> 오브젝트에 끌어 넣기
Sphere Collider에 들어감
바운스 정도 = 튕기는 정도
바운스 결합 = 다음 탄성 정도 계산



움직임

Rigidbody rigid;  //Rigidbody 선언

rigid.velocity = new Vector3(1, 2, 3);  //설정한 벡터로 계속 이동함.
rigid.AddForce(Vector3.up, ForceMode.Impulse);  //특정 벡터로 힘을 줘 움직이게 하는것, ForceMode.Inpulse를 제일 많이 사용한다 함.

//sample
//FixedUpdate 내부에 점프 구현시 키 씹힘 현상이 있어 Update에서 키 입력을 인식하고 FixedUpdate에서 반영하도록 작성
bool jumpInput = false;
void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))  //GetKeyDown누른 프레임에만 인식되어 씹힘 현상이 있을수 있음.
        {
            jumpInput = true;  // 점프 입력을 true로 설정
        }
    }

void FixedUpdate(){  //물리적인 동작은 여기에 쓰도록 권장됨. 
    if (jumpInput)
        {
            rigid.AddForce(Vector3.up * 5, ForceMode.Impulse);
            Debug.Log(rigid.velocity);
            jumpInput = false;  // 점프 입력을 다시 false로 설정
        }

        Vector3 vec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));  //화살표 키를 눌러서 상하좌우로 움직이기
        rigid.AddForce(vec / 2, ForceMode.Impulse);

        rigid.AddTorque(Vector3.up);  //제자리에서 회전. 다른 값을 주면 특정 방향으로 회전력을 가짐.
    }

}


물리 충돌 이벤트
//오브젝트 Mesh Renderer 및 Material 기져오기
MeshRenderer mesh;
Material matl
mesh = GetComponent<MeshRenderer>();
mat = mesh.material;

private void OnCollisionEnter(Collision collision){  //충돌했을 때
}
private void OnCollisionStay(Collision collision){   //충돌중 = 붙어있을 때
}
private void OnCollisionExit(Collision collision)   //충돌후 떨어졌을 때
}
//color = 기본 색상 클래스
//color32 = 255 색상 클래스

mat.color = new Color(0, 0, 0);  //0~255

//ex  부딛히면 색상 변경
if(collision.gameObject.name == "ObjectName"){  //바닥도 오브젝트이기 때문에 특정 오브젝트와 충돌했을 때 동작하기 위해
    mat.color = new Color(255, 0, 0);
}

//오브젝트 반투명
Material -> Rendering Mode = Transparent -> 알베도 -> A알파 값 변경

//오브젝트 통과
Box Collider -> trigger 트리거 체크

private void OnTriggerEnter(Collider other){  //겹치는 동시에
}
private void OnTriggerStay(Collider other){  //겹쳐 있을 때
}
private void OnTriggerExit(Collider other){  //겹치는게 끝났을 때
}

//ex  특정 공간 안에서 위로 힘을 주기
private void OnTriggerStay(Collider other){
    if (other.gameObject.name == "Cube"){
        rigid.AddForce(Vector3.up, ForceMode.Impulse);
    }
}

// Collision != Collider 서로 다른 클래스
