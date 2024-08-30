using Edulingual.Common.Interfaces;
using Edulingual.Service.Models;
using Edulingual.Service.Request.Feedback;

namespace Edulingual.Service.Interfaces;

public interface IFeedbachSerivce : IAutoRegisterable
{
    Task<ServiceActionResult> CreateFeedback(CreateFeedbackRequest createFeedbackRequest);
    Task<ServiceActionResult> GetFeedbackOfCourse(string id, int pageIndex, int pageSize);
    Task<ServiceActionResult> GetMyFeedbackInCourse(string id);
    Task<ServiceActionResult> DeleteFeedback(string id);
    Task<ServiceActionResult> UpdateFeedback(UpdateFeedbackRequest updateFeedbackRequest);
}
