using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using NLog;
using System.ComponentModel.DataAnnotations;
using FluentValidation.Attributes;
using System.Diagnostics;

namespace AspNetWebApiStaThreading
{
    [RoutePrefix("api")]
    public class DeviceController : ApiController
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IGoodService _goodService;
        private DeviceInfo _deviceInfo = new DeviceInfo();

        public DeviceController(IGoodService goodService)
        {
            //_logger = logger;
            _goodService = goodService;
            _goodService.DisplayName();
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

        [HttpPost, Route("validate")]
        public IHttpActionResult Validate([FromBody]IList<Company> companies)
        {
            return Ok(companies);
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
