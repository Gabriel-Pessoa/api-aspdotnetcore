using Course.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace Course.Api.Filters
{
    // classe para validação, devolvendo uma bad request com uma lista de erros caso ocorra 
    public class ValidationModelStateCustom : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Se action estiver inválida
            if (!context.ModelState.IsValid) 
            {
                //Criou uma listas de erros baseado na minha classe ValidateFieldViewModelOutput
                var validateFieldViewModel = new ValidateFieldViewModelOutput(context.ModelState.SelectMany(sm => sm.Value.Errors).Select(s => s.ErrorMessage));
                
                // resultado recebe uma bandRequest e seu conteúdo é uma lista de erros.
                context.Result = new BadRequestObjectResult(validateFieldViewModel);
            }
        }
    }
}
