using Edulingual.Service.Attributes;
using Edulingual.Service.Constants;
using System.ComponentModel.DataAnnotations;

namespace Edulingual.Service.Response.User;

public class ViewUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}
