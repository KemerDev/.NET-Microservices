using Microsoft.AspNetCore.Mvc;
using Game.Common;
using Game.Inventory.Service.Entities;
using Game.Inventory.Service.Clients;
using Game.Inventory.Service.Dtos;

namespace Game.Inventory.Service.Controllers
{
    [ApiController]
    [Route("inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IRepository<InventoryCs> inventoryRepository;
        private readonly ItemClient itemClient;

        public InventoryController(IRepository<InventoryCs> inventoryRepository, ItemClient itemClient)
        {
            this.inventoryRepository = inventoryRepository;
            this.itemClient = itemClient;
        }

        // create players inventory or containers or world global container
        [HttpPost]
        public async Task<ActionResult<InventoryDto>> CreateInventoryAsync([FromBody] CreateInventoryDto userId)
        {
            var temp = new InventoryCs
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId.UserId,
                inventoryItems = new List<ItemDto>()
            };

            var tempList = new List<InventoryCs>();
            tempList.Add(temp);

            await inventoryRepository.CreateItemsAsync(tempList);

            return Ok("test");
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<InventoryDto>> GetInventoryAsync(string userId)
        {
            if (userId == null)
            {
                return BadRequest();
            }

            var tempList = new List<string>();
            tempList.Add(userId);

            var inventory = await inventoryRepository.GetItemAsync(tempList);

            return Ok(inventory);
        }

        // [HttpGet("items")]
        // public Task<ActionResult<IEnumerable<ItemDto>>> GetInventoryItemsAsync(List<string> itemsId)
        // {
        //     return Ok("test");
        // }
    }
}