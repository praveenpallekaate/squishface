using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SquishFaceAPI.Model;
using SquishFaceAPI.Model.View;
using SquishFaceAPI.Service;

namespace SquishFaceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageManagement _messageManagement = null;

        public MessageController(IOptions<AppConfig> appConfigs, IMessageManagement messageManagement)
        {
            _messageManagement = messageManagement;
            _messageManagement.ResetGlobals();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _messageManagement.GetAllMessages();

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("by")]
        public IActionResult GetByCount(int count)
        {
            var result = _messageManagement.GetMessagesBy(new QueryDetail { Count = count });

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post(MessageDetail messageDetail)
        {
            if (messageDetail == null) return BadRequest();

            var result = _messageManagement.AddMessage(messageDetail);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Put(MessageDetail messageDetail)
        {
            if (messageDetail == null) return BadRequest();

            var result = _messageManagement.LikeMessage(messageDetail);

            return Ok(result);
        }
    }
}
