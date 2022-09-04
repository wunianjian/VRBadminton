using UnityEngine;

namespace OpenPose.Example
{
    public class FaceTrackMovment : MonoBehaviour
    {

        [SerializeField] Client receiver = null;
        [SerializeField] OpenPoseUserScript OpenPoseData = null;
        Transform startPos = null;

        [SerializeField] public float startingX = 0;
        [SerializeField] public float startingY = 0;
        [SerializeField] public float startingZ = 0;

        [SerializeField] public float yawCompensation = 0;
        [SerializeField] public float rollCompensation = 0;

        void Start()
        {
            startPos = transform;
            startPos.position = new Vector3(startingX, startingY, startingZ);
        }

        void Update()
        {
            // startPos.position = new Vector3(startingX + receiver.xPos, startingY, -(receiver.zPos + startingZ));
            // startPos.position = new Vector3(startingX, startingY, startingZ);
            // X,Y - 移动检测鼻子
            if (OpenPoseData != null)
            {
                /*
                Debug.Log(" --- output start ---");
                for (int index = 0; index < 1; index++) //25
                {
                    if (index == 0)
                        Debug.Log(OpenPoseData.index2name[index] + " :" + OpenPoseData.datum.poseKeypoints.Get(0, index, 0) + " " +
                            OpenPoseData.datum.poseKeypoints.Get(0, index, 1) + " " + OpenPoseData.datum.poseKeypoints.Get(0, index, 2));
                }
                Debug.Log(" --- output end ---");
                */
                double dx = (OpenPoseData.datum.poseKeypoints.Get(0, 0, 0) / 1280 - 0.5)*6;
                double dy = -(OpenPoseData.datum.poseKeypoints.Get(0, 0, 1) / 720 - 0.5)*1;
                
                startPos.position = new Vector3(startingX + receiver.xPos, startingY, -(receiver.zPos + startingZ));
                startPos.position = new Vector3(startPos.position.x + (float)dx, startPos.position.y + (float)dy, startPos.position.z);
                //Debug.Log(" startPos :"+dx+" "+dy);
            }
            startPos.rotation = Quaternion.Euler(receiver.yPos + yawCompensation, receiver.yaw+180, receiver.roll + rollCompensation);
        }
    }
}