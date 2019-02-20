using Business.Dto.Medico;
using Business.Rule;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Interface.Controllers
{
    [Route("[controller]")]
    public class MedicoController : Controller
    {
        private readonly UnitOfWork _uow;
        private readonly MedicoBusiness _business;

        public MedicoController(UnitOfWork uow)
        {
            _uow = uow;
            _business = new MedicoBusiness(uow);
        }

        [Authorize("Bearer")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(JsonConvert.SerializeObject(_business.GetById(id)));
        }

        [Authorize("Bearer")]
        [HttpPost]
        public IActionResult Post([FromBody] MedicoInsertDto dto)
        {
            return Ok(JsonConvert.SerializeObject(_business.Insert(dto).Id));
        }

        [Authorize("Bearer")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MedicoUpdateDto dto)
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

        [HttpGet("suggestion")]
        public IActionResult Busca([FromQuery] string filter)
        {
            return Ok(JsonConvert.SerializeObject(_business.Busca(filter)));
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
