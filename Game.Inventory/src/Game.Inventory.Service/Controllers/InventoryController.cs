using Microsoft.AspNetCore.Mvc;
using Game.Common;
using Game.Inventory.Service.Entities;

namespace Game.Inventory.Service.Controllers
{
    [ApiController]
    [Route("player/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IRepository<ItemCs> ItemsRepository;

        public InventoryController(IRepository<ItemCs> ItemsRepository)
        {
            this.ItemsRepository = ItemsRepository;
        }
    }
}