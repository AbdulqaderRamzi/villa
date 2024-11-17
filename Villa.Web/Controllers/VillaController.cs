using Microsoft.AspNetCore.Mvc;
using Villa.Web.Models.Dtos.Villa;
using Villa.Web.Services.IServices;

namespace Villa.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaApiClient _villaApiClient;

        public VillaController(IVillaApiClient villaApiClient)
        {
            _villaApiClient = villaApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var villas = await _villaApiClient.GetAllVillasAsync();
            return View(villas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VillaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var createdVilla = await _villaApiClient.CreateVillaAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id is 0)
            {
                return BadRequest();
            }
            var villa = await _villaApiClient.GetVillaAsync(id);
            return View(villa);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int idVillaUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                return View(dto);
            }

            await _villaApiClient.UpdateVillaAsync(id, dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (id is 0)
            {
                return BadRequest();
            }
            await _villaApiClient.DeleteVillaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
