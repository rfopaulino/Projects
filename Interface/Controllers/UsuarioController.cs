using Business.Dto.Usuario;
using Business.Rule;
using Common;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Interface.Controllers
{
    [Route("[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly UsuarioBusiness _business;

        public UsuarioController(UnitOfWork uow)
        {
            _uow = uow;
            _business = new UsuarioBusiness(_uow);
        }

        [HttpPost("authentication")]
        public object Authentication([FromBody] UsuarioLoginDto dto, [FromServices] SigningConfigurations signingConfigurations, [FromServices] TokenConfigurations tokenConfigurations)
        {
            return _business.Authentication(dto, signingConfigurations, tokenConfigurations);
        }

        [HttpPost("validtoken")]
        public IActionResult ValidToken()
        {
            //SecurityToken validatedToken;
            //TokenValidationParameters validationParameters = new TokenValidationParameters();
            //validationParameters.IssuerSigningKey = DefaultX509Key_Public_2048;
            //new JwtSecurityTokenHandler().ValidateToken("BASE64_ENCODED_JWT_TOKEN_GOES_HERE", validationParameters, out validatedToken);

            return Ok(true);
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            return Ok(JsonConvert.SerializeObject(_business.GetById(id)));
        }

        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody] UsuarioInsertDto dto)
        {
            return Ok(JsonConvert.SerializeObject(_business.Insert(dto).UsuarioLogin));
        }

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] UsuarioUpdateDto dto)
        {
            _business.Update(id, dto);
            return Ok(true);
        }

        [Authorize("Bearer")]
        [HttpGet("grid")]
        public IActionResult Grid(string id)
        {
            return Ok(JsonConvert.SerializeObject(_business.Grid()));
        }

        [Authorize("Bearer")]
        [HttpPost("deletar")]
        public IActionResult ExcluirSelecionados([FromBody] List<string> ids)
        {
            _business.ExcluirSelecionados(ids);
            return Ok(true);
        }

        [Authorize("Bearer")]
        [HttpPost("inativar")]
        public IActionResult InativarSelecionados([FromBody] List<string> ids)
        {
            _business.InativarSelecionados(ids);
            return Ok(true);
        }
    }
}
