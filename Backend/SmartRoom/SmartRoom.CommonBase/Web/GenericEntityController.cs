using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Logic.Contracts;

namespace SmartRoom.CommonBase.Web
{
    public class GenericEntityController<E> : ControllerBase where E : EntityObject
    {
        readonly IGenericEntityManager<E> _entityManager;
        public GenericEntityController(IGenericEntityManager<E> entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<E>>> GetAll()
        {
            return Ok(await _entityManager.Get());
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<E>> Get(Guid id)
        {
            var res = await _entityManager.GetBy(id);
            return Ok(res);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([FromBody] E entity)
        {
            if (entity == null) return BadRequest();
            try
            {
                await _entityManager.Add(entity);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] E entity)
        {
            if (entity == null) return BadRequest();
            try
            {
                await _entityManager.Update(entity);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            await _entityManager.Delete(id);
            return Ok();
        }
    }
}
