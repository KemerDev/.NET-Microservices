using Game.Item.Service.Dtos;
using Microsoft.AspNetCore.Mvc;
using Game.Item.Service.Entities;
using Game.Common;

namespace Game.Item.Service.Controllers
{
    [ApiController]
    [Route("Items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<ItemCs> itemsRepository;

        private static int requestCount = 0;

        public ItemsController(IRepository<ItemCs> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        // return all items | spawned items, inventory items etc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItemsAsync()
        {
            var items = (await itemsRepository.GetItemsAsync()).Select(item => item.AsDto());

            return Ok(items);
        }

        // return one or many items | spawned items, inventory items etc
        [HttpPost("GrubItems")]
        public async Task<IEnumerable<ItemDto>> GetItemAsync([FromBody] List<string> itemsId)
        {

            var filteredItems = (await itemsRepository.GetItemAsync(itemsId)).Select(item => item.AsDto());

            return filteredItems;
        }

        // create world loot, add item to players inventory etc
        [HttpPost("CreateLoot")]
        public async Task<ActionResult<ItemDto>> CreateItemsAsync([FromBody] List<CreateItemsDto> createItemsDto)
        {

            List<ItemCs> finalTemp = new();

            foreach (var item in createItemsDto)
            {
                var temp = new ItemCs
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    CreatedDate = DateTimeOffset.UtcNow
                };

                finalTemp.Add(temp);
            }

            await itemsRepository.CreateItemsAsync(finalTemp);

            return Ok("Loot");
        }

        // update the market price 
        [HttpPut("UpdateItem")]
        public async Task UpdateItemAsync([FromBody] List<UpdateItemDto> updateItemsDto)
        {
            List<ItemCs> finalTemp = new();

            var idList = updateItemsDto.Select(c => c.Id).ToList();

            var filteredItems = (await itemsRepository.GetItemAsync(idList)).Select(item => item.AsDto());

            foreach (var (itemFil, itemUp) in filteredItems.Zip(updateItemsDto))
            {
                var temp = new ItemCs
                {
                    Id = itemFil.Id,
                    Name = itemFil.Name,
                    Description = itemFil.Description,
                    Price = itemUp.Price,
                    CreatedDate = DateTimeOffset.UtcNow
                };

                finalTemp.Add(temp);
            }
            await itemsRepository.UpdateItemAsync(finalTemp);
        }

        // user might delete some items or sell some items or delete the loot gradually from the world as time passes etc
        [HttpDelete("DeleteItem")]
        public async Task DeleteItemAsync([FromBody] List<string> deleteItemsDto)
        {
            await itemsRepository.DeleteItemAsync(deleteItemsDto);
        }
    }
}