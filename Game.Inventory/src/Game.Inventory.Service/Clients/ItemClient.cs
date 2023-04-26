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

        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var items = await httpClient.GetFromJsonAsync<IEnumerable<ItemDto>>("/Items");

            return items;
        }

        public async Task<IEnumerable<ItemDto>> GetItemAsync()
        {
            var filteredItems = await httpClient.GetFromJsonAsync<IEnumerable<ItemDto>>("/Items/GrabItems");

            return filteredItems;
        }

        public async Task<IEnumerable<ItemDto>> GetRandomItemsAsync()
        {
            var randomItems = await httpClient.GetFromJsonAsync<IEnumerable<ItemDto>>("/Items/RandomItems");
            return randomItems;
        }
    }
}