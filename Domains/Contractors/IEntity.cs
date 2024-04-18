namespace InventoryManagement.Domains.Contractors
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
