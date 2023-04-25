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

        public ItemsController(IRepository<ItemCs> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = (await itemsRepository.GetItemsAsync()).Select(item => item.AsDto());

            return items;
        }

        [HttpPost("GrubItems")]
        public async Task<IEnumerable<ItemDto>> GetItemAsync([FromBody] List<string> itemsId)
        {

            var filteredItems = (await itemsRepository.GetItemAsync(itemsId)).Select(item => item.AsDto());

            return filteredItems;
        }

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
    }
}