namespace OvdVsBotWeb.DataAccess
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }

}
