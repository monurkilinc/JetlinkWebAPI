using JetlinkWebAPI.Application.DTOs;
using JetlinkWebAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JetlinkWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TextConController : Controller
    {
        private readonly ITextConService _textConService;
        public TextConController(ITextConService textConService)
        {
            _textConService = textConService;
        }

        [HttpPost]
        public ActionResult<UserTextModel> ConvertText(UserTextModel userTextModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _textConService.ConvertWordToNumber(userTextModel);
            return Ok(result);
        }
    }
}
