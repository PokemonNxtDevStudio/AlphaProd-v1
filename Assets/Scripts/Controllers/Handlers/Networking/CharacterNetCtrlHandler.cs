using UnityEngine;

namespace PokemonNXT.Net.Controllers {
    using Net;

    public class CharacterNetCtrlHandler: BaseNetControllerHandler {

        protected Vector3 LastMotorDirectionInput;
        protected Vector3 LastAnimatorDirectionInput;

        protected override void UpdateAnimator() {
            AnimatorCtrl.SetFloat("DIRY", LastAnimatorDirectionInput.z, 0.15f, Time.deltaTime);
            AnimatorCtrl.SetFloat("DIRX", LastAnimatorDirectionInput.x, 0.15f, Time.deltaTime);
        }

        #region Networking overrides
        protected override void SendData(PhotonStream stream, PhotonMessageInfo info) {
            base.SendData(stream, info);
            stream.SendNext(LastMotorDirectionInput);
        }

        protected override void ReceiveData(PhotonStream stream, PhotonMessageInfo info) {
            base.ReceiveData(stream, info);
            LastMotorDirectionInput = (Vector3)stream.ReceiveNext();
            LastAnimatorDirectionInput = LastMotorDirectionInput;
        }

        protected override void ExtrapolatePosition() {
            //Extrapolate based on remote input
            MotorCtrl.Move(LastMotorDirectionInput);
            //Interpolate
            if(Vector3.Distance(pos, RemotePosition) > 0.1f) {
                //When the sent user/AI input is zero and we interpolate the gameobject move on idle state
                //Lets just add a small value to our LastAnimatorDirectionInput in order to trigger the animator
                //(This would break our balls if the object was animated from the CharacterMotorCtrl.cs)
                if(LastAnimatorDirectionInput.Equals(Vector3.zero))
                    LastAnimatorDirectionInput = new Vector3(0, 0, 0.5f);
                pos = Vector3.MoveTowards(pos, RemotePosition, MotorCtrl.CurrentSpeed/3 * Time.deltaTime);
            }
        }

        protected override void InterpolateRotation() {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, RemoteRotation, 180f * Time.deltaTime);
        }
        #endregion

        #region Unity API
        protected override void Start() {
            base.Start();
            LastMotorDirectionInput = Vector3.zero;
            LastAnimatorDirectionInput = Vector3.zero;
        }
        //@TODO: Delete comments
        //float _lastUpdateTime = 0.0f;
        protected override void Update() {
            //var currentUpdateTime = Time.time - _lastUpdateTime;
            //PokemonNXT.Info("Frame time: " + currentUpdateTime);
            //_lastUpdateTime = Time.time;

            if(IsRemoteObject)
                SyncWithRemote();
            else {
                if(InputState == InputState.AI)
                    LastMotorDirectionInput = AIInputCtrl.Direction;
                else if(InputState == InputState.User)
                    LastMotorDirectionInput = UserInputCtrl.Direction;
                MotorCtrl.Move(LastMotorDirectionInput);
                LastAnimatorDirectionInput = LastMotorDirectionInput;
            }
            UpdateAnimator();//@IFDO: Comment and Uncomment the two lines in the CharacterMotorCtrl.cs to see time differences
        }
        #endregion
    }

}