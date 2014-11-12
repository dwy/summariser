using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Summariser.Data;
using Summariser.Models;

namespace Summariser.Controllers
{
    public class DefaultController : ApiController
    {
        public object Get()
        {
            return new int[] {1,2,3,4,5};
        }

        public object Get(int id)
        {
            return new { id = id, value = "default"};
        }
    }
}