using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using SkamBook.Application.ViewModels;

namespace SkamBook.API.Filters;
public class ValidationFilters : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                .SelectMany(v => v.Errors)
                .Select(v => v.ErrorMessage)
                .ToList();

            var reponse = new ResponseViewModel(false, errors);

            context.Result = new JsonResult(reponse)
            {
                StatusCode = 400
            };
        }
    }
}
