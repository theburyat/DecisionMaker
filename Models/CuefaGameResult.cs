using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Models;

public class CuefaGameResult
{
    public CuefaResult Result { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public Cuefa EnemyChoice { get; set; }
}