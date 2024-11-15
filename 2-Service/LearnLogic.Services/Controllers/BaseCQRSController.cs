using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace LearnLogic.Services.Controllers
{
    public abstract class BaseCQRSController : ControllerBase
    {
        private readonly ICollection<string> _errors = new List<string>();

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            var hasError = validationResult.Errors.Any();
            if (hasError)
            {
                foreach (var error in validationResult.Errors)
                {
                    AddError(error.ErrorMessage);
                }
            }
            return CustomPostResponse();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (IsOperationValid())
            {
                return Ok(result);
            }

            return BadRequest(GetValidationProblemDetails());
        }

        protected ActionResult CustomPostResponse(object result = null)
        {
            if (IsOperationValid())
            {
                if (result == null)
                {
                    return Ok();
                }
                else
                    return Ok(result);
            }

            return BadRequest(GetValidationProblemDetails());
        }

        protected ValidationProblemDetails GetValidationProblemDetails()
        {
            var errors = new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Messages", _errors.ToArray() }
            });

            return errors;
        }

        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
