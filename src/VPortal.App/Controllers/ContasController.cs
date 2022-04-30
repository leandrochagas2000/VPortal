using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VPortal.App.Extensions;
using VPortal.App.ViewModels;
using VPortal.Business.Interfaces;
using VPortal.Business.Models;

namespace VPortal.App.Controllers
{
    [Authorize]
    public class ContasController : BaseController
    {
        private readonly IContaRepository _contaRepository;
        private readonly IContaService _contaService;

        private readonly IMapper _mapper;

        public ContasController(IContaRepository contaRepository, 
                                IMapper mapper, 
                                IContaService contaService,
                                INotificador notificador) : base(notificador)
        {
            _contaRepository = contaRepository;
            _mapper = mapper;
            _contaService = contaService;
        }

        [AllowAnonymous]
        [Route("lista-de-contas")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContaViewModel>>(await _contaRepository.ObterTodos()));
        }

        [AllowAnonymous]
        [Route("dados-da-conta/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        [ClaimsAuthorize("Conta","Adicionar")]
        [Route("nova-conta")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Conta", "Adicionar")]
        [Route("nova-conta")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContaViewModel contaViewModel)
        {
            if (!ModelState.IsValid) return View(contaViewModel);

            var conta = _mapper.Map<Conta>(contaViewModel);
            await _contaService.Adicionar(conta);

            if (!OperacaoValida()) return View(contaViewModel);


            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Conta", "Editar")]
        [Route("editar-conta/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var contaViewModel = await ObterContaProdutosEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        [ClaimsAuthorize("Conta", "Editar")]
        [Route("editar-conta/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (Guid id, ContaViewModel contaViewModel)
        {
            if (id != contaViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(contaViewModel);

            var conta = _mapper.Map<Conta>(contaViewModel);
            await _contaService.Atualizar(conta);


            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Conta", "Excluir")]
        [Route("excluir-conta/{id:guid}")]
        public async Task<IActionResult> Delete (Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if (contaViewModel == null)
            {
                return NotFound();
            }

            return View(contaViewModel);
        }

        [ClaimsAuthorize("Conta", "Excluir")]
        [Route("excluir-conta/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contaViewModel = await ObterContaEndereco(id);

            if(contaViewModel == null) return NotFound();

            await _contaService.Remover(id);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("obter-endereco-conta/{id:guid}")]
        public async Task<IActionResult> ObterEndereco(Guid id)
        {
            var conta = await ObterContaEndereco(id);

            if (conta == null)
            {
                return NotFound();
            }

            return PartialView("_DetalhesEndereco", conta);
        }

        [ClaimsAuthorize("Conta", "Editar")]
        [Route("atualizar-endereco-conta/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var conta = await ObterContaEndereco(id);

            if (conta == null)
            {
                return NotFound();
            }

            return PartialView("_AtualizarEndereco", new ContaViewModel { Endereco = conta.Endereco });
        }

        [ClaimsAuthorize("Conta", "Excluir")]
        [Route("atualizar-endereco-conta/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(ContaViewModel contaViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid) return PartialView("_AtualizarEndereco", contaViewModel);

            await _contaService.AtualizarEndereco(_mapper.Map<Endereco>(contaViewModel.Endereco));

            var url = Url.Action("ObterEndereco", "Contas", new { id = contaViewModel.Endereco.ContaId });
            return Json(new { success = true, url });

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
