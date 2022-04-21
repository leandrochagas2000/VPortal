using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VPortal.App.ViewModels;
using VPortal.Business.Interfaces;
using VPortal.Business.Models;

namespace VPortal.App.Controllers
{
    public class ContasController : BaseController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IMapper _mapper;

        public ContasController(IContaRepository contaRepository, IMapper mapper)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContaViewModel>>(await _contaRepository.ObterTodos()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaViewModel contaViewModel)
        {
            if (!ModelState.IsValid) return View(contaViewModel);

            var conta = _mapper.Map<Conta>(contaViewModel);
            await _contaRepository.Adicionar(conta);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var contaViewModel = await ObterContaProdutosEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Guid id, ContaViewModel contaViewModel)
        {
            if (id != contaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(contaViewModel);

            var conta = _mapper.Map<Conta>(contaViewModel);
            await _contaRepository.Atualizar(conta);


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete (Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if(contaViewModel == null) return NotFound();

            await _contaRepository.Remover(id);
            return RedirectToAction("Index");
        }

        private async Task<ContaViewModel> ObterContaEndereco(Guid id)
        {
            return _mapper.Map<ContaViewModel>(await _contaRepository.ObterContaEndereco(id));
        }

        private async Task<ContaViewModel> ObterContaProdutosEndereco(Guid id)
        {
            return _mapper.Map<ContaViewModel>(await _contaRepository.ObterContaProdutosEndereco(id));
        }

    }
}
