using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIMP_Lab4
{
    public class Segment
    {
        public int L { get; }
        public int R { get; }
        public Segment(int x1, int x2)
        {
            if (x1 < 0 || x2 < 0) throw new ArgumentException("l or zero cannot equals zero");
            if (x1 < x2)
            {
                L = x1;
                R = x2;
            }
            else
            {
                L = x2;
                R = x1;
            }
        }
    }
    public class ShadowCalc
    {
        private List<Segment> _segments;

        public ShadowCalc(IEnumerable<Segment> segments)
        {
            _segments = segments.ToList();
        }

        public long CalcTotalLength()
        {
            if (_segments.Count == 0) return 0;

            _segments.Sort((a,b) => a.L.CompareTo(b.L));

            long total = 0;
            int curL = _segments[0].L;
            int curR = _segments[0].R;

            foreach (var seg in _segments.Skip(1))
            {
                if (seg.L > curR)
                {
                    total += curR - curL;
                    curL = seg.L;
                    curR = seg.R;
                }
                else if (seg.R > curR)
                { 
                    curR = seg.R;
                }
            }
            total += curR - curL;
            return total;
        }
    }
}
