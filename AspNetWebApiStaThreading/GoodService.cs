using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApiStaThreading
{
    public class GoodService : IGoodService
    {
        public GoodService()
        {
            Debug.WriteLine("XXX: Instance created!!!");
        }

        public void DisplayName()
        {
            
        }
    }

    public interface IGoodService
    {
        void DisplayName();
    }
}
