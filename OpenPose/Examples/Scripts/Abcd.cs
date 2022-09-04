using UnityEngine;
using Intel.RealSense;
namespace OpenPose.Example
{
    public class Abcd : MonoBehaviour
    {

        [SerializeField] OpenPoseUserScript OpenPoseData = null;
        [SerializeField] RealSenseUserScript RealSenseData = null;
        public Vector3 RElbowOut; //3
        public Vector3 RWristOut; //4

        float lastRElbowx = 0.5f;
        float lastRWristx = 0.5f;
        float lastRElbowy = 0.5f;
        float lastRWristy = 0.5f;
        float lastRElbowdep = 0.5f;
        float lastRWristdep = 0.5f;
        void Start()
        {
            RElbowOut = new Vector3(0, 1, 0);
            RWristOut = new Vector3(0, 0, 0);
        }

        void Update()
        {
            if (OpenPoseData != null)
            {
                Vector2 screenSize = new Vector2(1280, 720);
                Vector2 realsenseSize = new Vector2(640, 480);
                //Debug.Log("size : " + " " + screenSize.x + " " + screenSize.y + " ");
                float RElbowOutx = OpenPoseData.datum.poseKeypoints.Get(0, 3, 0) / screenSize.x;
                float RElbowOuty = OpenPoseData.datum.poseKeypoints.Get(0, 3, 1) / screenSize.y;
                float RWristOutx = OpenPoseData.datum.poseKeypoints.Get(0, 4, 0) / screenSize.x;
                float RWristOuty = OpenPoseData.datum.poseKeypoints.Get(0, 4, 1) / screenSize.y;

                if (RElbowOutx < 1e-8)
                    RElbowOutx = lastRElbowx;
                else
                    lastRElbowx = RElbowOutx;

                if (RElbowOuty < 1e-8)
                    RElbowOuty = lastRElbowy;
                else
                    lastRElbowy = RElbowOuty;

                if (RWristOutx < 1e-8)
                    RWristOutx = lastRWristx;
                else
                    lastRWristx = RWristOutx;

                if (RWristOuty < 1e-8)
                    RWristOuty = lastRWristy;
                else
                    lastRWristy = RWristOuty;

                float RElbowdep = 0;
                float RWristdep = 0;
                if (RealSenseData != null&& RealSenseData.depthMap!=null)
                {
                    int RElbowOutix = (int)(RElbowOutx * realsenseSize.x);
                    int RElbowOutiy = (int)(RElbowOuty * realsenseSize.y);
                    int RWristOutix = (int)(RWristOutx * realsenseSize.x);
                    int RWristOutiy = (int)(RWristOuty * realsenseSize.y);

                    //RElbowdep = RealSenseData.depthMap.GetDistance(RElbowOutix, RElbowOutiy)/5;
                    //RWristdep = RealSenseData.depthMap.GetDistance(RWristOutix, RWristOutiy)/5;
                    RElbowdep = RealSenseData.GetAverageDistance(RElbowOutix, RElbowOutiy) / 5;
                    RWristdep = RealSenseData.GetAverageDistance(RWristOutix, RWristOutiy) / 5;
                    if (RElbowdep > 1) RElbowdep = 1;
                    if (RWristdep > 1) RWristdep = 1;
                }

                if (RWristdep < 1e-8)
                {
                    RWristdep = lastRWristdep;
                    RWristOut = new Vector3(RWristOutx, 1 - RWristOuty, lastRWristdep);
                }
                else
                {
                    lastRWristdep = RWristdep;
                    RWristOut = new Vector3(RWristOutx, 1 - RWristOuty, RWristdep);
                }
                if (RElbowdep < 1e-8)
                {
                    RElbowdep = lastRElbowdep;
                    RElbowOut = new Vector3(RElbowOutx, 1-RElbowOuty, lastRElbowdep);
                }
                else
                {
                    lastRElbowdep = RElbowdep;
                    RElbowOut = new Vector3(RElbowOutx, 1-RElbowOuty, RElbowdep);
                }
                RWristOut = new Vector3((RWristOut.x - 0.5f) * 5, (RWristOut.y) * 3 - 0.5f, RWristOut.z*2+1f);
                RElbowOut = new Vector3((RElbowOut.x - 0.5f) * 5, (RElbowOut.y) * 3 - 0.5f, RElbowOut.z*2+1f);
                // Debug.Log("RWristOut     : " + RWristOut.x + " " + RWristOut.y + " " + RWristOut.z + " ");
                // Debug.Log("RElbowOut     : " + RElbowOutx + " " + RElbowOuty + " " + RElbowdep + " ");
                        
            }
        }
    }
}