using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueByPassAPI.Model
{
    public class PostSpec
    {
        public string host {get;set; }

        public string path {get;set; }

        public object data {get;set; }

    }

    public class TestCount
    {
        public int callBackCount;

        public int callCount;

        public TestCount()
        {
            callCount = 0;
            callBackCount = 0;
        }

    }

}
