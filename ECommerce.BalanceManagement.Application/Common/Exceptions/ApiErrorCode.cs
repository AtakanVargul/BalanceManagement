
namespace ECommerce.BalanceManagement.Application.Common.Exceptions;

/// <summary>
/// Generic Error Codes
/// </summary>
public class ApiErrorCode
{
    public const string InternalError = "100";
    public const string ServiceForbidden = "101";
    public const string InvalidParameters = "102";
    public const string NotFound = "103";
    public const string ValidationError = "104";
    public const string InvalidCredentials = "105";
    public const string DuplicateRecord = "106";
    public const string InvalidAuthorizationHeader = "107";
    public const string AuthorizationSignatureMismatch = "108";
    public const string AuthorizationTimestampExpired = "109";
    public const string AuditableMissingInfo = "110";
    public const string InsufficientBalanceLimit = "111";
    public const string InsufficientStock = "112";
}