

using System.Text.Json.Serialization;

namespace BlazorWebProject.Model
{
    public class EmployeeModel
    {
        [JsonPropertyName("employeeId")]
        public string EmployeeId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("birthDate")]
        public string BirthDate { get; set; } = string.Empty;

        [JsonPropertyName("gender")]
        public string Gender { get; set; } = string.Empty;

        [JsonPropertyName("departmentId")]
        public string DepartmentId { get; set; } = string.Empty;

        [JsonPropertyName("departmentName")]
        public string DepartmentName { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string id { get; set; } = string.Empty;

        [JsonPropertyName("pk")]
        public string pk { get; set; } = string.Empty;
        public string RegisterId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
