using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Example.Core.Web.Extensions
{
    public static class ModelStateExtensions
    {
        private static string ErrorMessage(ModelError error, ModelState modelState)
        {
            if (!string.IsNullOrEmpty(error.ErrorMessage))
            {
                return error.ErrorMessage;
            }
            if (modelState.Value == null)
            {
                return error.ErrorMessage;
            }
            var args = new object[] { modelState.Value.AttemptedValue };
            return string.Format("ValueNotValidForProperty=The value '{0}' is invalid", args);
        }

        public static object SerializeErrors(this ModelStateDictionary modelState)
        {
            return modelState.Where<KeyValuePair<string, ModelState>>(entry => entry.Value.Errors.Any<ModelError>())
                .ToDictionary<KeyValuePair<string, ModelState>, string, Dictionary<string, object>>(
                entry => entry.Key, entry => SerializeModelState(entry.Value));
        }
        public static string SerializeErrorToString(this ModelStateDictionary modelState)
        {
            var message = new List<string>();
            foreach (var state in modelState)
            {
                if (state.Value.Errors.Count > 0)
                    state.Value.Errors.ToList().ForEach(x => message.Add(x.ErrorMessage));
            }
            return string.Format("{0}", string.Join("\n", message.ToArray()));
        }

        private static Dictionary<string, object> SerializeModelState(ModelState modelState)
        {
            var dictionary = new Dictionary<string, object>();
            dictionary["errors"] = modelState.Errors.Select<ModelError, string>(x => ErrorMessage(x, modelState)).ToArray<string>();
            return dictionary;
        }

        public static object ToDataSourceResult(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.SerializeErrors();
            }
            return null;
        }
    }
}
