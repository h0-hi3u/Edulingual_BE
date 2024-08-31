using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Microsoft.AspNetCore.Http;

namespace Edulingual.Service.Interfaces;

public interface IExamService : IAutoRegisterable
{
    Task<ServiceActionResult> CreateExamFromExcel(string id, IFormFile file);
    Task<ServiceActionResult> GetExam(string id);
    Task<ServiceActionResult> DeleteExam(string id);
}
