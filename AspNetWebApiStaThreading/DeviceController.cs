using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;

namespace AspNetWebApiStaThreading
{
    [RoutePrefix("api/devices")]
    public class DeviceController : ApiController
    {
        private readonly Logger _logger;
        private DeviceInfo _deviceInfo = new DeviceInfo();

        public DeviceController(Logger logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("")]
        [UseStaThread]
        public IHttpActionResult Get()
        {
            try
            {
                throw new ArgumentNullException("arg1");
            }
            finally
            {
                Console.WriteLine("Finally block executed");
            }
        }

        [HttpGet]
        [UseStaThread]
        public string Take1()
        {
            int sessionId = _deviceInfo.Take1();
            return string.Format("Take 1 - Thread id: {0}, Session id: {1}", Thread.CurrentThread.ManagedThreadId, sessionId);
        }

        [HttpGet]
        [UseStaThread]
        public string Take2()
        {
            int sessionId = _deviceInfo.Take2();
            return string.Format("Take 2 - Thread id: {0}, Session id: {1}", Thread.CurrentThread.ManagedThreadId, sessionId);
        }

        [HttpGet]
        public string Mta()
        {
            return "value";
        }

        [HttpGet]
        public HttpResponseMessage Message()
        {
            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public IHttpActionResult Action()
        {
            return Ok("Ok action result returned");
        }

        [HttpGet]
        public object Poco()
        {
            return null;
            //return new Company { Name = "abc", Place = "def" };
        }
    }

    public class Company
    {
        public string Name { get; set; }
        public string Place { get; set; }
    }
}
