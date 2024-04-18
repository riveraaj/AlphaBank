using Interfaces.ContinueLoans.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers {
    public class R_LateClientsController(ICollectionHistoryService collectionHistoryService) : Controller {

        private readonly ICollectionHistoryService _collectionHistoryService = collectionHistoryService;

        public async Task<IActionResult> LateClientsList()
            => View(await _collectionHistoryService.GetAllForDefaultersReport());
    }
}