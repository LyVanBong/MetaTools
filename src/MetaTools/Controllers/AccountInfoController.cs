namespace MetaTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountInfoController : ControllerBase
    {
        private readonly IAccountInfoRepository _accountInfoRepository;

        public AccountInfoController(IAccountInfoRepository accountInfoRepository)
        {
            _accountInfoRepository = accountInfoRepository;
        }

        [HttpGet("gets")]
        public IActionResult GetAll()
        {
            return Ok(_accountInfoRepository.GetAllAccountsAsync());
        }
    }
}