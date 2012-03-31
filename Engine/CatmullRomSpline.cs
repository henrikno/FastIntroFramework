using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using OpenTK;

namespace OpenTKProject.Engine
{
    class CatmullRomSpline
    {
        private List<Vector3> points = new List<Vector3>();

        public void AddPoint(Vector3 p)
        {
            points.Add(p);
        }

        public Vector3 GetValue(float t)
        {
            float delta_t = (float) 1/points.Count();
            int p = (int)(t/delta_t);

            var a = new int[4];
            a[0] = p - 1;
            a[1] = p;
            a[2] = p + 1;
            a[3] = p + 2;

            List<int> b = a.Select(x => Math.Max(0, Math.Min(points.Count()-1, x))).ToList();

            float lt = (t - delta_t*(float) p)/delta_t;
            return Eq(lt, points[b[0]], points[b[1]], points[b[2]], points[b[3]]);
        }

        private Vector3 Eq(float t, Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4)
        {
            float t2 = t*t;
            float t3 = t2*t;
            
           float b1 = (float).5 * (  -t3 + 2*t2 - t);
           float b2 = (float).5 * (3 * t3 - 5 * t2 + 2);
           float b3 = (float).5 * (-3 * t3 + 4 * t2 + t);
           float b4 = (float).5 * (t3 - t2);

            return p1*b1 + p2*b2 + p3*b3 + p4*b4;

        }
    }
}
