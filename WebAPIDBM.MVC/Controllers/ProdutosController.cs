using Microsoft.AspNetCore.Mvc;
using WebAPIDBM.Domain.Entities;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    public async Task<IActionResult> Index()
    {
        var produtos = await _produtoService.GetProdutosAsync();
        return View(produtos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (ModelState.IsValid)
        {
            await _produtoService.CreateProdutoAsync(produto);
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var produto = await _produtoService.GetProdutoByIdAsync(id);
        return View(produto);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Produto produto)
    {
        if (ModelState.IsValid)
        {
            await _produtoService.UpdateProdutoAsync(produto);
            return RedirectToAction(nameof(Index));
        }
        return View(produto);
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _produtoService.DeleteProdutoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
