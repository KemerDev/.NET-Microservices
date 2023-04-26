using Game.Inventory.Service.Dtos;

namespace Game.Inventory.Service.Clients
{
    public class ItemClient
    {
        private readonly HttpClient httpClient;

        public ItemClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<ItemDto>> GetItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<ItemDto>>("/Items");

            return items;
        }
    }
}