using Microsoft.AspNetCore.Mvc;
using SmartRoom.CommonBase.Core.Entities;

namespace SmartRoom.CommonBase.Web.Contracts
{
    public interface IGenericEntityController<E> where E : EntityObject
    {
        Task<IActionResult> Delete(Guid id);
        Task<ActionResult<E>> Get(Guid id);
        Task<ActionResult<IEnumerable<E>>> GetAll();
        Task<ActionResult> Post([FromBody] E entity);
        Task<IActionResult> Put([FromBody] E entity);
    }
}