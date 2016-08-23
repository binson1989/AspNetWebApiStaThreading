using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Diagnostics;
using Autofac.Extras.NLog;

namespace AspNetWebApiStaThreading
{
    [RoutePrefix("api")]
    public class DeviceController : ApiController
    {
        private readonly ILogger _logger;
        private IGoodService _goodService;
        private DeviceInfo _deviceInfo = new DeviceInfo();

        public DeviceController(IGoodService goodService, ILogger logger)
        {
            _goodService = goodService;
            _goodService.DisplayName();
            _logger = logger;
            Debug.WriteLine("XXX: New controller created !!!");
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

        [HttpGet, Route("take1")]
        [UseStaThread]
        public string Take1()
        {
            int sessionId = _deviceInfo.Take1();
            _logger.Info("Take 1 - Thread id: {0}, Session id: {1}", Thread.CurrentThread.ManagedThreadId, sessionId);
            return string.Format("Take 1 - Thread id: {0}, Session id: {1}", Thread.CurrentThread.ManagedThreadId, sessionId);
        }

        [HttpGet, Route("take2")]
        [UseStaThread]
        public string Take2()
        {
            int sessionId = _deviceInfo.Take2();
            return string.Format("Take 2 - Thread id: {0}, Session id: {1}", Thread.CurrentThread.ManagedThreadId, sessionId);
        }

        [HttpGet, Route("mta")]
        public string Mta()
        {
            return "value";
        }

        [HttpGet, Route("message")]
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

        [HttpPost, Route("validate"), UseStaThread]
        public IHttpActionResult Validate([FromBody]Company companies)
        {
            return Ok(companies);
        }

        [HttpGet, Route("throw"), UseStaThread]
        public IHttpActionResult Throw([FromBody]IList<Company> companies)
        {
            throw new NotSupportedException("XXX: This api is not supported!!!");
        }
    }

    //[Validator(typeof(CompanyValidator))]
    public class Company
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    }
}
