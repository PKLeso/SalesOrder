using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrder.Data;

namespace SalesOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly SalesOrderDbContext context;
        public IConfiguration configuration;

        public BaseController(SalesOrderDbContext ctx, IConfiguration iConfig)
        {
            context = ctx;
            configuration = iConfig;
        }
    }
}
