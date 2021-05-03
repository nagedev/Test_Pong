using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Pong
{
    public sealed class MultiplayerPhoton : MonoBehaviourPunCallbacks, IMultiplayer
    {
        private LevelController _levelController;
        
        private const byte SyncEvent = 1;
        
        public void Inject(LevelController levelController)
        {
            _levelController = levelController;

            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
        }
        
        public override void OnConnectedToMaster()
        {
            Debug.Log("Photon - Connected to master");

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 2;
            PhotonNetwork.JoinOrCreateRoom("defaultRoom", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            if (PhotonNetwork.IsMasterClient)
                return;

            Debug.Log("Photon - Joined as 2nd player");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            var batTopId = _levelController.GetBat(BatPlace.Top).GetComponent<PhotonView>().ViewID;
            var batBottomId = _levelController.GetBat(BatPlace.Bottom).GetComponent<PhotonView>().ViewID;
            var ballId = _levelController.GetBall().GetComponent<PhotonView>().ViewID;
            var ballType = (int) _levelController.GetBall().BallType;

            object[] content = { batTopId, batBottomId, ballId, ballType };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(SyncEvent, content, raiseEventOptions, SendOptions.SendReliable);
            
            Debug.Log("Photon - 2nd player joined your room");
        }
        
        private void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (eventCode == SyncEvent)
            {
                object[] data = (object[]) photonEvent.CustomData;
                int batTopId = (int) data[0];
                int batBottomId = (int) data[1];
                int ballId = (int) data[2];
                BallType ballType = (BallType) data[3]; 

                Sync(batTopId, batBottomId, ballId, ballType);
            }
        }

        private void Sync(int batTopId, int batBottomId, int ballId, BallType ballType)
        {
            var batTop = _levelController.GetBat(BatPlace.Top);
            batTop.GetComponent<PhotonView>().ViewID = batTopId;
            if (PhotonNetwork.IsMasterClient)
            {
                batTop.SetBatControl(BatControlType.Other);
            }
            else
            {
                batTop.GetComponent<PhotonView>().RequestOwnership();
                batTop.SetBatControl(BatControlType.Player);
            }

            var batBottom = _levelController.GetBat(BatPlace.Bottom);
            batBottom.GetComponent<PhotonView>().ViewID = batBottomId;
            if (PhotonNetwork.IsMasterClient)
            {
                batBottom.GetComponent<PhotonView>().RequestOwnership();
                batBottom.SetBatControl(BatControlType.Player);
            }
            else
            {
                batBottom.SetBatControl(BatControlType.Other);
            }

            Ball ball = _levelController.GetBall();
            ball.GetComponent<PhotonView>().ViewID = ballId;
            if (PhotonNetwork.IsMasterClient)
            {
                ball.GetComponent<PhotonView>().RequestOwnership();
            }
            else
            {
                ball.ReconfigureBall(ballType, false);
            }
        }

        public void RegisterSyncedObject(GameObject obj)
        {
            PhotonNetwork.AllocateViewID(obj.GetComponent<PhotonView>());
        }
    }
}