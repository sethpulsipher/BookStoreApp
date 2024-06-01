namespace BookStoreApp.API.Models
{
    /*
     * The Base class for all DTO's
     * They all require an Id
     */
    public abstract class BaseDto
    {
        public int Id { get; set; }
    }
}
