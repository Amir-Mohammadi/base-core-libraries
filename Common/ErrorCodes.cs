using System.Linq;
namespace base_core.Common
{
  public class ErrorCodes
  {
    #region Error code name resolver
    public static string Resolve(int errorCode)
    {
      var errorCodeType = typeof(ErrorCodes);
      var dictionary = errorCodeType.GetFields().Select(x => new { Name = x.Name, Value = x.GetValue(x.Name).ToString() });
      return dictionary.FirstOrDefault(x => x.Value == errorCode.ToString()).Name;
    }
    #endregion
    public const int INTERNAL_SERVER_ERROR = 1000;
    public const int ACCESS_DENIED = 1003;
    public const int RESOURCE_NOT_FOUND = 1016;
    public const int INVALID_TOKEN = 1022;
    public const int THIS_USER_IS_ALTERED = 1031;
    public const int INVALID_VERIFICATION_CODE = 1032;
    public const int XSS_DETECT = 1049;
    public const int API_PROTECTOR_DETECT = 1050;
    public const int FILE_NOT_FOUND = 1051;
    public const int CUSTOMER_MISMATCH = 1052;
    public const int ASSET_ALREADY_SHEARD = 1053;
    public const int ASSET_MISMATCH = 1054;
    public const int PHONE_FORMAT_ERROR = 1055;
    public const int EMAIL_FORMAT_ERROR = 1056;
  }
}