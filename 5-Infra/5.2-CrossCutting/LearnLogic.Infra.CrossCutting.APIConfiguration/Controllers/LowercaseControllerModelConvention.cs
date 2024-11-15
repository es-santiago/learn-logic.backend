using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace LearnLogic.Infra.CrossCutting.APIConfiguration.Controllers
{
    public class LowercaseControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            controller.ControllerName = controller.ControllerName.ToLower();

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel
                {
                    Template = AttributeRouteModel.CombineTemplates(
                        selector.AttributeRouteModel?.Template,
                        controller.ControllerName)
                };
            }
        }
    }
}
