using Microsoft.AspNetCore.Mvc;

namespace ProjectService.Controllers
{
    public interface IController<TDI>
    {
        Task<IActionResult> GetAll();

        Task<IActionResult> GetSingle(int id);

        Task<IActionResult> CreateEntity(TDI dto);

        Task<IActionResult> UpdateEntity(TDI dto, int id);

        Task<IActionResult> DeleteEntity(int id);

        Task<IActionResult> GetPagination(int page, string condition);

    }
}
