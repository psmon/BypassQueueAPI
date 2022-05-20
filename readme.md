# BypassQueueAPI

API 기능 : API요청완료를, 손서보장을 시킵니다.

사용목적 : 이벤트 발생(주로 웹훅을 호출하는경우)지점 수정이 불가능하고
웹훅으로 연결된 스트리밍 웹훅 API 요청완료에 대해 순차완료보장이 필요한경우 (Lock사용 불필요)


## 컨셉

![](./doc/concept0.png)

![](./doc/concept.png)
TYPE A: 직접 WebHook 받을시

![](./doc/concept2.png)
TYPE B: WebHook Target수정불가시 - ByPass+CallBack 방식으로 작동할시


##
    완료시간이 제각각이여도, 요청 순서대로 완료보장~

    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-1] Done TestCallBack 2 , Completed Time 508
    [INFO][2022-05-20 오전 6:55:09][Thread 0018][akka://akka-universe/user/group1] Received PostSpec message: http://localhost:9000/api/ByPassCallBack/test
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          PostTodoItem
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          TestCallCount 6
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          PostTodoItem
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          TestCallCount 7
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-2] Done TestCallBack 3 , Completed Time 918
    [INFO][2022-05-20 오전 6:55:10][Thread 0008][akka://akka-universe/user/group1] Received PostSpec message: http://localhost:9000/api/ByPassCallBack/test
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-3] Done TestCallBack 4 , Completed Time 724
    [INFO][2022-05-20 오전 6:55:11][Thread 0004][akka://akka-universe/user/group1] Received PostSpec message: http://localhost:9000/api/ByPassCallBack/test
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-4] Done TestCallBack 5 , Completed Time 765
    [INFO][2022-05-20 오전 6:55:11][Thread 0028][akka://akka-universe/user/group1] Received PostSpec message: http://localhost:9000/api/ByPassCallBack/test
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-5] Done TestCallBack 6 , Completed Time 1061
    [INFO][2022-05-20 오전 6:55:13][Thread 0018][akka://akka-universe/user/group1] Received PostSpec message: http://localhost:9000/api/ByPassCallBack/test
    info: QueueByPassAPI.Controllers.ByPassCallBackController[0]
          [REQNO-6] Done TestCallBack 7 , Completed Time 1332

