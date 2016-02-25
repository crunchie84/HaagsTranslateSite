namespace HaagsVertaler
{
  using System;
  using System.Web;
  using System.Web.Mvc;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;

  public class JsonNetResult : JsonResult
  {
    public override void ExecuteResult(ControllerContext context)
    {
      if (context == null)
      {
        throw new ArgumentNullException("context");
      }

      var response = context.HttpContext.Response;

      response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : "application/json";

      if (this.ContentEncoding != null)
      {
        response.ContentEncoding = this.ContentEncoding;
      }

      if (this.Data == null)
      {
        return;
      }

      var jsonSerializerSettings = new JsonSerializerSettings();
      jsonSerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
      jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      var formatting = HttpContext.Current != null && HttpContext.Current.IsDebuggingEnabled ? Formatting.Indented : Formatting.None;
      var serializedObject = JsonConvert.SerializeObject(Data, formatting, jsonSerializerSettings);
      response.Write(serializedObject);
    }
  }
}
