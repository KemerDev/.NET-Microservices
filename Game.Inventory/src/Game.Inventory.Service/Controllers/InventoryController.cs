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

        [HttpGet("{_id}")]
        public async Task<ActionResult<List<InventoryDto>>> GetInventoryAsync(List<string> _id)
        {
            if (_id == null)
            {
                return BadRequest();
            }

            var inventory = await inventoryRepository.GetItemAsync(_id);

            return Ok(inventory);
        }

        // create players inventory or containers or world global container
        [HttpPost]
        public async Task<ActionResult> CreateInventoryAsync([FromBody] CreateInventoryDto flags)
        {
            var tempItems = new List<ItemDto>();

            // if flags.empty is true then fill inventory with items
            if (flags.empty)
            {
                var randomItems = await itemClient.GetRandomItemsAsync();

                foreach (var item in randomItems)
                {
                    tempItems.Add(item);
                }
            }

            var temp = new InventoryCs
            {
                Id = Guid.NewGuid().ToString(),
                UserId = flags.UserId,
                inventoryItems = tempItems
            };

            var tempList = new List<InventoryCs>();
            tempList.Add(temp);

            await inventoryRepository.CreateItemsAsync(tempList);

            return Ok("Created");
        }
    }
}