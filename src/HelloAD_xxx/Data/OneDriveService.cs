namespace HelloAD.Data;

public class OneDriveService
{
	private readonly HttpClient _httpClient;

	public OneDriveService(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}

	public async Task<IEnumerable<string>> GetOneDriveFilesAsync()
	{
		var response = await _httpClient.GetAsync("https://graph.microsoft.com/v1.0/me/drive/root/children");
		response.EnsureSuccessStatusCode();
		var content = await response.Content.ReadAsStringAsync();
		var files = System.Text.Json.JsonSerializer.Deserialize<OneDriveFiles>(content);
		return files.Value.Select(f => f.Name);
	}
}

public class OneDriveFiles
{
	public List<OneDriveFile> Value { get; set; }
}

public class OneDriveFile
{
	public string Name { get; set; }
}

