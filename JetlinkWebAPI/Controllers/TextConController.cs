using JetlinkWebAPI.Application.DTOs;
using JetlinkWebAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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
            if (userTextModel == null || string.IsNullOrWhiteSpace(userTextModel.Text))
            {
                return BadRequest("Invalid input");
            }
            var result = _textConService.ConvertWordToNumber(userTextModel);
            return Ok(result);
        }
    }
}
