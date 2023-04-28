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

        public async Task<IEnumerable<InventoryItemDto>> GetItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDto>>("/Items");

            return items;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetItemAsync()
        {
            var filteredItems = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDto>>("/Items/GrabItems");

            return filteredItems;
        }

        public async Task<IEnumerable<InventoryItemDto>> GetRandomItemsAsync()
        {
            var randomItems = await httpClient.GetFromJsonAsync<IEnumerable<InventoryItemDto>>("/Items/RandomItems");
            return randomItems;
        }
    }
}