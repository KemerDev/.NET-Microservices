namespace Game.Inventory.Service.Entities
{
    public class ContainerCs
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public List<ItemCs> itemList { get; set; }
    }
}