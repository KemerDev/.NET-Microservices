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
        public async Task<ActionResult> InitWorldInventoryAsync([FromBody] CreateInventoryDto flags)
        {
            var tempItems = new List<ItemCs>();

            // if flags.empty is true then fill inventory with items
            if (flags.worldInit)
            {
                var containerSum = 100;
                var randomItems = await itemClient.GetRandomItemsAsync();

                var InitMasterInventory = new InventoryCs
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = flags.UserId,
                    inventoryList = new List<ContainerCs>(),
                };

                var ScarceCont = new ContainerCs
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "ScarseLootContainer",
                    itemList = new List<ItemCs>()
                };

                var contCount = -1;
                var tempCount = -1;

                foreach (var item in randomItems)
                {
                    tempCount++;

                    if (tempCount == 5 && contCount <= 50)
                    {
                        contCount++;

                        var Containers = new ContainerCs
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "CrateContainer",
                            itemList = tempItems
                        };

                        InitMasterInventory.inventoryList.Add(Containers);

                        tempCount = -1;

                        tempItems.Clear();
                    }

                    if (tempCount == 5 && (contCount > 50 || contCount <= 70))
                    {
                        contCount++;

                        var Containers = new ContainerCs
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "CacheContainer",
                            itemList = tempItems
                        };

                        InitMasterInventory.inventoryList.Add(Containers);

                        tempCount = -1;

                        tempItems.Clear();
                    }

                    if (tempCount == 5 && (contCount > 70 || contCount <= containerSum))
                    {
                        contCount++;

                        var Containers = new ContainerCs
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = "OtherContainer",
                            itemList = tempItems
                        };

                        InitMasterInventory.inventoryList.Add(Containers);

                        tempCount = -1;

                        tempItems.Clear();
                    }

                    var tempItem = new ItemCs
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        CreatedDate = DateTimeOffset.UtcNow
                    };

                    tempItems.Add(tempItem);
                }

                ScarceCont.itemList = tempItems;

                InitMasterInventory.inventoryList.Add(ScarceCont);

                await inventoryRepository.CreateItemsAsync(new List<InventoryCs> { InitMasterInventory });
            }
            return Ok("Created");
        }
    }
}