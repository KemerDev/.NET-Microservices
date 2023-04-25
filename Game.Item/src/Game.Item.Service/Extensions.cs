using Game.Item.Service.Dtos;
using Game.Item.Service.Entities;

namespace Game.Item.Service
{
    public static class Extensions
    {
        public static ItemDto AsDto(this ItemCs item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}