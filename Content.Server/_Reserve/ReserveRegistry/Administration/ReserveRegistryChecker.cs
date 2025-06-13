using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Content.Server.ReserveRegistry
{
    [UsedImplicitly]
    public sealed class ReserveRegistryChecker : IDisposable
    {
        private readonly HttpClient _httpClient = new();
        private ISawmill _sawmill = Logger.GetSawmill("ReserveRegistry");

        private readonly string _registryUrl;

        public ReserveRegistryChecker(string registryUrl, string authToken)
        {
            _registryUrl = registryUrl;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }

        public async Task<RegistryBanData?> CheckBanAsync(Guid uuid, CancellationToken cancel = default)
        {
            try
            {
                var url = $"{_registryUrl}/bans/check?uuid={WebUtility.UrlEncode(uuid.ToString())}";
                using var response = await _httpClient.GetAsync(url, cancel);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _sawmill.Debug("UUID {0} не найден в реестре.", uuid);
                    return null;
                }

                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    _sawmill.Error("Исчерпан лимит проверок, проверка {0} отклонена.", uuid);
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    _sawmill.Error("Неожиданный ответ от реестра: {0}", response.StatusCode);
                    return null;
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter(), new IPAddressJsonConverter() }
                };

                var data = await response.Content.ReadFromJsonAsync<RegistryBanData>(options, cancel);
                return data;
            }
            catch (Exception ex)
            {
                _sawmill.Error("Ошибка во время чека: {0}", ex);
                return null;
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public sealed class RegistryBanData
        {
            [JsonPropertyName("uuid")]
            public Guid Uuid { get; set; }

            [JsonPropertyName("hwid")]
            public byte[]? Hwid { get; set; }

            [JsonPropertyName("ckey")]
            public string Ckey { get; set; } = default!;

            [JsonPropertyName("ban_time")]
            public double BanTime { get; set; }

            [JsonPropertyName("added_by")]
            public int AddedBy { get; set; }

            [JsonPropertyName("address")]
            public IPAddress? Address { get; set; }

            [JsonPropertyName("reason")]
            public string? Reason { get; set; }
        }

        private sealed class IPAddressJsonConverter : JsonConverter<IPAddress>
        {
            public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var str = reader.GetString();
                return str != null ? IPAddress.Parse(str) : throw new JsonException("Invalid IP address");
            }

            public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
