using Business.Dto.Agendamento;
using Business.Rule;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Interface.Controllers
{
    [Route("[controller]")]
    public class AgendamentoController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly AgendamentoBusiness _business;

        public AgendamentoController(UnitOfWork uow)
        {
            _uow = uow;
            _business = new AgendamentoBusiness(uow);
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(JsonConvert.SerializeObject(_business.GetById(id)));
        }

        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody] AgendamentoInsertDto dto)
        {
            return Ok(_business.Insert(dto).Id);
        }

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] AgendamentoUpdateDto dto)
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
        public IActionResult ExcluirSelecionados([FromBody] List<int> ids)
        {
            _business.ExcluirSelecionados(ids);
            return Ok(true);
        }
    }
}
