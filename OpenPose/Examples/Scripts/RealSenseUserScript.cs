using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using UnityEngine;
namespace Intel.RealSense
{
    class RealSenseUserScript : MonoBehaviour
    {
        private Sensor depthSensor = null;
        private VideoStreamProfile sp = null;
        private Device dev = null;
        public DepthFrame depthMap = null;

        void Start()
        {
            using (var ctx = new Context())
            {
                var devices = ctx.QueryDevices();

                if (devices.Count == 0) return;
                dev = devices[0];

                depthSensor = dev.QuerySensors()[0];

                sp = depthSensor.StreamProfiles
                                    .Where(p => p.Stream == Stream.Depth)
                                    .OrderByDescending(p => p.Framerate)
                                    .Select(p => p.As<VideoStreamProfile>())
                                    .First(p => p.Width == 640 && p.Height == 480);
                depthSensor.Open(sp);
                Debug.Log("open");


                int one_meter = (int)(1f / depthSensor.DepthScale);
                ushort[] depth = new ushort[640 * 480];
                char[] buffer = new char[(640 / 10 + 1) * (480 / 20)];
                int[] coverage = new int[64];

                depthSensor.Start(f =>
                {
                    depthMap = f.As<DepthFrame>();
                    //Debug.Log(depthMap.GetDistance(200, 300));
                });
            }
        }
        public float GetAverageDistance(int x, int y)
        {
            int valid_num = 0;
            float sum_depth = 0;

            if (x > 0)
            {
                if (y > 0)
                {
                    var num = depthMap.GetDistance(x - 1, y - 1);
                    valid_num += num == 0 ? 0 : 1;
                    sum_depth += depthMap.GetDistance(x - 1, y - 1);
                }
                if (y < 480 - 1)
                {
                    valid_num += depthMap.GetDistance(x - 1, y + 1) == 0 ? 0 : 1;
                    sum_depth += depthMap.GetDistance(x - 1, y + 1);
                }
                valid_num += depthMap.GetDistance(x - 1, y) == 0 ? 0 : 1;
                sum_depth += depthMap.GetDistance(x - 1, y);
            }

            if (x < 640 - 1)
            {
                if (y > 0)
                {
                    valid_num += depthMap.GetDistance(x + 1, y - 1) == 0 ? 0 : 1;
                    sum_depth += depthMap.GetDistance(x + 1, y - 1);
                }
                if (y < 480 - 1)
                {
                    valid_num += depthMap.GetDistance(x + 1, y + 1) == 0 ? 0 : 1;
                    sum_depth += depthMap.GetDistance(x + 1, y + 1);
                }
                valid_num += depthMap.GetDistance(x + 1, y) == 0 ? 0 : 1;
                sum_depth += depthMap.GetDistance(x + 1, y);
            }

            if (y > 0)
            {
                valid_num += depthMap.GetDistance(x, y - 1) == 0 ? 0 : 1;
                sum_depth += depthMap.GetDistance(x, y - 1);
            }

            if (y < 480 - 1)
            {
                valid_num += depthMap.GetDistance(x, y + 1) == 0 ? 0 : 1;
                sum_depth += depthMap.GetDistance(x, y + 1);
            }

            valid_num += depthMap.GetDistance(x, y) == 0 ? 0 : 1;
            sum_depth += depthMap.GetDistance(x, y);

            //Debug.Log("valid number: " + valid_num);
            //Debug.Log("local sum" + sum_depth);

            if (valid_num == 0) return 0;
            else return sum_depth / (float)valid_num;
        }
        void Update()
        {
        }
    }
}