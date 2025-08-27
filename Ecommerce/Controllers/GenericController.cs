using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]

public class GenericController<TEntity> : ControllerBase where TEntity : class
{
    public readonly IBaseService<TEntity> _service;

    public GenericController(IBaseService<TEntity> service)
    {
        _service = service;
    }


    [HttpGet("enum-all")]
    public async Task<IActionResult> GetAll()
    {
        var entities = await _service.GetAll();
        return entities != null ? Ok(entities) : NotFound();
    }

    [HttpGet("byId/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await _service.GetById(id);
        return entity != null ? Ok(entity) : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntity newEntity)
    {
        await _service.Create(newEntity);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TEntity newEntity)
    {
        await _service.Update(newEntity);
        return Ok(newEntity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }


    [HttpGet("query-all")]
    public IActionResult GetAllQueryable()
    {
        var entities = _service.GetAllQueryable();
        return entities != null ? Ok(entities.ToList()) : NotFound();
    }
    
}