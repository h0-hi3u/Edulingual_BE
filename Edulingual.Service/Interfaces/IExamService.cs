using Edulingual.Service.Models;
using Microsoft.AspNetCore.Http;

namespace Edulingual.Service.Interfaces;

public interface IExamService
{
    Task<ServiceActionResult> CreateExam(IFormFile file);
    Task<ServiceActionResult> GetExam(string id);
    Task<ServiceActionResult> DeleteExam(string id);
}
