using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Entity<T>
    {
        [JsonIgnore]
        public T? Id { get; set; }
    }
}
