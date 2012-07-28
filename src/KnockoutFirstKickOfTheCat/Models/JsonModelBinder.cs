using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KnockoutFirstKickOfTheCat.Models
{
    public class JsonModelBinder : IModelBinder
    {
        private readonly static JavaScriptSerializer serializer = new JavaScriptSerializer();


        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
            if (string.IsNullOrEmpty(stringified))
                return null;
            return serializer.Deserialize(stringified, bindingContext.ModelType);
        }
    } 


}