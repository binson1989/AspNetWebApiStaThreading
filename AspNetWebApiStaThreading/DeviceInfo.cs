using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApiStaThreading
{
    public class DeviceInfo
    {
        private int _sessionId;

        private GeDevice _geDevice;
        public GeDevice GeDevice
        {
            get
            {
                return _geDevice ?? (_geDevice = new GeDevice());
            }
        }

        public int Take1()
        {
            _sessionId = 1;
            GeDevice.Take(1, 2);
            return _sessionId;
        }

        public int Take2()
        {
            GeDevice.Take(3, 4);
            return _sessionId;
        }
    }

    public class GeDevice
    {
        private int _id1, _id2;

        public void Take(int id1, int id2)
        {
            _id1 = id1;
            _id2 = id2;
        }
    }
}
