using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;
using SmartRoom.CommonBase.Core.Exceptions;
using SmartRoom.CommonBase.Logic.Contracts;
using SmartRoom.CommonBase.Web.Contracts;

namespace SmartRoom.CommonBase.Web
{
    public class GenericEntityController<E> : ControllerBase, IGenericEntityController<E> where E : EntityObject
    {
        protected readonly IGenericEntityManager<E> _entityManager;
        public GenericEntityController(IGenericEntityManager<E> entityManager)
        {
            _entityManager = entityManager;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<E>>> GetAll()
        {
            try
            {
                return Ok(await _entityManager.Get());
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<E>> Get(Guid id)
        {
            try
            {
                return Ok(await _entityManager.GetBy(id));
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([FromBody] E entity)
        {
            if (entity == null) return BadRequest(Messages.PARAMTER_NULL);
            try
            {
                await _entityManager.Add(entity);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }
            return Ok();
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put([FromBody] E entity)
        {
            if (entity == null) return BadRequest(Messages.PARAMTER_NULL);
            try
            {
                await _entityManager.Update(entity);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _entityManager.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(Messages.UNEXPECTED);
            }

            return Ok();
        }
    }
}
