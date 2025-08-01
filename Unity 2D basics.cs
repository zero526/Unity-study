픽셀아트 사용할 때

스프라이트 모드 -> 단위당 픽셀 = 크기에 맞게
ex)16

필터 모드 = 점(필터없음)

압축 = none 없음

컴포넌트 추가 //2D 플랫포머 기준
1.Box Collider 2D //사이즈 조절로 물리 크기 조정 가능, 밟는 바닥의 경우 이것만 있으면 됨
2.Rigidbody 2D

편집 -> 프로젝트 설정 -> 물리2D -> 기본 컨텍트 오프셋 //0.01로 되어있음. 0으로 줄이면 물체끼리 딱 붙음

Layer
Background / player 등으로 나눠서 지정

창 -> 애니메이션 -> 애니메이션 //프레임 및 속도 조절
창 -> 애미네이션 -> 애니메이터 //
Entry = 시작
애니메이터 -> 우클릭 -> 레이어 기본 상태로 설정 //Entry에 연결되어 기본적으로 실행
애니메이터 -> 블럭 클릭 -> 인터프리터 -> speed 조절로 애니메이션 속도 조절 가능

Physics Material 2D
Friction = 마찰 정도

Rigidbody 2D
선 항력 = 공기 저항
Constraints -> 회전 고정 -> Z //물체 및 캐릭터 회전 방지

단발적 키 입력 = Update() 내부에

rigid.velocity.normalized.x
normalized //벡터의 크기를 1로 만든 상태


//캐릭터 이동 및 최대속도 제한
void FixedUpdate() 
{
    float h = Input.GetAxisRaw("Horizontal");

    rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

    //최대속도 제한
    if (rigid.velocity.x > maxSpeed){   //오른쪽
        rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
    }
    else if (rigid.velocity.x < (maxSpeed*(-1))){   //왼쪽
        rigid.velocity = new Vector2((maxSpeed * (-1)), rigid.velocity.y);
    }
}

/*
//평면에서의 대각선 이동을 포함한 최대 속도 제한
if (rigid.velocity.magnitude > maxSpeed){
    rigid.velocity = rigid.velocity.normalized * maxSpeed;
}
*/


//캐릭터 이동중 빠른 정지, Update() 내부에
if (Input.GetButtonUp("Horizontal"))
{
    rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);  //빠르게 정지
    //rigid.velocity = new Vector2(0, rigid.velocity.y);                                 //급정지
}


애니메이터 -> 오브젝트 우클릭 -> 전환 만들기 -> 다음 애니메이션 지정
ex) Entry -> Player_stay -> Walk
Walk -> Player_stay //서로 연결해줘야 걷다가 멈췄을때 다시 움직임

애니메이터 -> 오브젝트 연결하는 화살표 클릭
(옵션)
종료시간 있음 //해제
겹치는 구간 조절로 부드러운 애니메이션
Conditions 매개변수 지정 //애니메이터 -> 파라미터 -> 파라미터 생성 후 지정해주기


//애니메이션 전환
if(rigid.velocity.normalized.x == 0)
{
    animator.SetBool("is_Walk", false);
}
else
{
    animator.SetBool("is_Walk", true);
}

using Mathf.Abs; //절댓값, Mathf는 유니티에서 제공하는 수학 라이브러리

//타일맵, 2D 조명 (RP? URP)


//플레이어 이동시 방향키를 입력받아 좌/우를 바꾸는 애니메이션 적용 시 실제 이동방향과 다른 애니메이션인 상태로 움직이는 경우가 있음
//사용자 입력이 아닌 실제 이동방향에 따라 애니메이션을 설정해야 오류가 안 날듯 함.
