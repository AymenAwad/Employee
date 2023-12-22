
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Shared.Globalization
{
    public class Globals
    {
        private readonly LocService _SharedLocalizer;
        public Globals(LocService service)
        {
            _SharedLocalizer = service;
        }//

        public enum TimeOut
        {
            DriverRequest = 14
        }
        public enum StringLength
        {
            LabelLenght = 20,
            phoneLength = 15,
            GUID = 50,
            CommentLength = 1000,
            ShortName = 120,
            LongName = 500,
            AddressLength = 1000,
            ImageLength = 200,
            VideoLength = 200,
            HashLenght = 512,
            EmailLength = 100,
            FileLenght = 200,
            ImageFileLength = 2000000,
            DescriptionLength = 2000,
            UploadFileLength = 30000000,
            NameLength = 120,
            ItemDescLength = 1024
        }
        public static Dictionary<string, string> RegEx = new Dictionary<string, string>
        {
            //{ "Name", @"^[A-Za-z ءأ-ي]*$" },
            { "Name", @"^\S([أ-يءA-Za-z]{1,}( [أ-يءA-Za-z]{2,})*) ?$" },
            { "Description", @"^\S([أ-يءA-Za-z0-9-_]{1,}( [أ-يءA-Za-z0-9-_]{1,})*) ?$" },
            { "Phone", @"^[0-9\u0660-\u0669]{9,14}$" },
            { "PhoneLandLine", @"^[0-9\u0660-\u0669]{5,14}$" },
            { "Id", @"^[1-9]+[0-9]*$" },
            { "Number",@"^\d{1,10}(\.\d{1,2})?$" },
            { "IntegerNumber",@"^[0-9\u0660-\u0669]*$" },
            { "Email", @"^[a-zA-Z0-9]{2,30}((\.|-)([a-zA-Z0-9]{2,30})){0,4}?@[a-zA-Z]{2,30}((\.|-)([a-zA-Z]{2,30})){1,4}?$" },


        };

        public static Dictionary<string, string> RegExMsg = new Dictionary<string, string>
        {
            { "Name", "Characters Only" },
            { "Phone", "Phone Must Be Numbers at Least 10 Numbers" },
            { "Number", @"Numbers Only" },
            { "Email", @"Email not valid, must be like xxx@xxxx.xxx" },

        };
        /// <summary>
        /// Message For Minmum Length and Max Length Errors
        /// </summary>
        /// <param name="Property"></param>
        /// <param name="Length">Number Of Length</param>
        /// <param name="LengthType">Maximum Or Minmum </param>
        /// <param name="PropertyType">Charcter Or Numbers</param>
        /// <returns></returns>
        public string MinmumandMaximumLengthErrorMsg(string Property, int Length, string LengthType = "Maximum", string PropertyType = "Character")
        {
            string keyLength = _SharedLocalizer.GetLocalizedString($"{LengthType} Length");
            string keyIs = _SharedLocalizer.GetLocalizedString("Is");
            string keyProp = _SharedLocalizer.GetLocalizedString(Property);
            string keyLengthType = _SharedLocalizer.GetLocalizedString(LengthType);
            string keyPropType = _SharedLocalizer.GetLocalizedString(PropertyType);
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{keyLength} {keyProp} {keyIs} {Length} {keyPropType}";
            else
                return $"{Property} {LengthType} Length Is {Length} {PropertyType}.";

        }

        ///// <summary>
        ///// Message For Ids For Not Empty Or 0
        ///// </summary>
        ///// <param name="EntityName"></param>
        ///// <returns></returns>
        //public static string IdsErrorMsg(string EntityName)
        //{
        //    return $"{EntityName} Id Must Be Not Empty and Greater Than 0.";
        //}
        /// <summary>
        /// Message For Ids For Not Empty Or 0
        /// </summary>
        /// <param name="EntityName"></param>
        /// <returns></returns>
        public string IdErrorMsg(string EntityName)
        {
            string key = _SharedLocalizer.GetLocalizedString("Must Be Not Empty");
            string keyId = _SharedLocalizer.GetLocalizedString("Id");
            string keyEntity = _SharedLocalizer.GetLocalizedString(EntityName);
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{key} {keyId} {keyEntity}";
            else
                return $"{keyEntity} {keyId} {key}";
        }

        /// <summary>
        /// Message For Required Property
        /// </summary>
        /// <param name="ProprtyName"></param>
        /// <returns></returns>
        public string RequiredErrorMsg(string ProprtyName)
        {
            string key = _SharedLocalizer.GetLocalizedString("Must Be Not Empty");
            string keyProp = _SharedLocalizer.GetLocalizedString(ProprtyName);
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{key} {keyProp}";
            else
                return $"{keyProp} {key}";
        }

        /// <summary>
        /// Return error msg for intgers or fractions numbers if it greater than your value
        /// </summary>
        /// <param name="ProprtyName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string GreaterThanErrorMsg(string ProprtyName, object Value)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString("must be")} {_SharedLocalizer.GetLocalizedString(ProprtyName)}" +
                    $"{_SharedLocalizer.GetLocalizedString("greater than ")} {Value}";
            else
                return $"{ProprtyName} must be greater than {Value}";
        }

        /// <summary>
        /// Return error msg for intgers or fractions numbers if it less than your value
        /// </summary>
        /// <param name="ProprtyName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string LessThanErrorMsg(string ProprtyName, object Value)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString("must be")} {_SharedLocalizer.GetLocalizedString(ProprtyName)}" +
                    $"{_SharedLocalizer.GetLocalizedString("less than ")} {Value}";
            else
                return $"{ProprtyName} must be less than {Value}";
        }

        /// <summary>
        /// function display msg for Delete failure exception
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="Child"></param>
        /// <returns></returns>
        public string DeleteFailureExceptionMsg(string entityName, string Child)
        {
            string KeyFailed = $"{_SharedLocalizer.GetLocalizedString("failed")} {_SharedLocalizer.GetLocalizedString("Deletion of")} {_SharedLocalizer.GetLocalizedString(entityName)}";
            string keyThere = _SharedLocalizer.GetLocalizedString("There Is");
            string keyAssoc = _SharedLocalizer.GetLocalizedString("Associated With It");
            string keyChild = _SharedLocalizer.GetLocalizedString(Child);
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{KeyFailed} {keyThere} {keyChild} {keyAssoc}";
            else
                return $"Deletion of {entityName} failed, There Is {Child} Associated With It";
        }

        /// <summary>
        /// function display msg for Not Found exception
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="EntityId"></param>
        /// <returns></returns>
        public string NotFoundExceptionMsg(string entityName, object EntityId)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString(entityName)} {_SharedLocalizer.GetLocalizedString("Which Code Is")}" +
                    $" {EntityId} {_SharedLocalizer.GetLocalizedString("Was Not Found")}";
            else
                return $"The {entityName} Which Code Is {EntityId} Was Not Found";
        }

        public string NotEnouphQuantity(object ItemId, object PackunitId, object InvoiceId)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{ItemId} {_SharedLocalizer.GetLocalizedString("with")} {PackunitId}" +
                    $" {_SharedLocalizer.GetLocalizedString("has not enouph quantity for this invoice")} {InvoiceId}";
            else
                return $"The {ItemId} with {PackunitId} has not enouph quantity for this invoice {InvoiceId}";
        }

        /// <summary>
        /// For rollback and commit actions
        /// </summary>
        /// <returns></returns>
        public string InvoiceAlreadyHaveAction(object InvoiceId, object Action)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString("Invoice")} {InvoiceId}" +
                    $" {_SharedLocalizer.GetLocalizedString("was already")} {Action}";
            else
                return $"Invoice {InvoiceId} was already {Action}";
        }

        public string InvoiceTimeOutRange()
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString("You can not modify or make any procedure for this invoice")}";
            else
                return $"You can not modify or make any procedure for this invoice";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">Action that happen when exception occured like as Add or Update or Delete....</param>
        /// <param name="EntityName">Name of entity which is used</param>
        /// <param name="key">Key which is unique index in database like as Name or Id</param>
        /// <returns></returns>
        public string KeyIsAlreadyExistsExceptionMsg(string action, string EntityName, string keyNameProp)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString("failed")} {_SharedLocalizer.GetLocalizedString(action)} {_SharedLocalizer.GetLocalizedString(EntityName)}" +
                    $" {_SharedLocalizer.GetLocalizedString(keyNameProp)} {_SharedLocalizer.GetLocalizedString("Is Already Existed")}";
            else
                return $"{action} {EntityName} failed. {keyNameProp} Is Already Existed";
        }

        public string NotFoundInEnumMsg(string propertyName, string Enum)
        {
            string key1 = _SharedLocalizer.GetLocalizedString("Not Found");
            string key2 = _SharedLocalizer.GetLocalizedString("In");
            string keyEnum = _SharedLocalizer.GetLocalizedString(Enum);
            string keyProp = _SharedLocalizer.GetLocalizedString(propertyName);
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{keyProp} {key1} {key2} {keyEnum}";
            else
                return $"{propertyName} Not Found In {Enum}";
        }

        /// <summary>
        /// function that take key for regex and return error msg
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetRegexErrorMessage(string key)
        {
            switch (key.ToLower())
            {
                case "name":
                    return $"{_SharedLocalizer.GetLocalizedString("Allow Characters Only")}";
                case "phone":
                    return $"{_SharedLocalizer.GetLocalizedString("Must Be Numbers and between 9 and 14")}";
                case "number":
                    return $"{_SharedLocalizer.GetLocalizedString("Allow Numbers Only")}";
                case "description":
                    return $"{_SharedLocalizer.GetLocalizedString("Not accept spaces on first and last of text")}";
                case "email":
                    return $"{_SharedLocalizer.GetLocalizedString("Email not valid, must be like") + " xxx@xxxx.xxx"}";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Return with Msg for response
        /// </summary>
        /// <param name="EntityName">Like Branch, Company.....</param>
        /// <param name="Action">Action that occuer on api like (Added, Updated, Deleted, Changed)</param>
        /// <returns></returns>
        public string CommandResponseMsg(string EntityName, string Action)
        {
            if (CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar")
                return $"{_SharedLocalizer.GetLocalizedString($"has been {Action.ToLower()}")} {_SharedLocalizer.GetLocalizedString($"{EntityName}")} " +
                    $"{_SharedLocalizer.GetLocalizedString("successfully")}";
            else
                return $"{EntityName} has been {Action} successfully";
        }

        /// <summary>
        /// send Key Message and Get any Localized Messasge
        /// /// </summary>
        /// <param name="keyMsg"></param>
        /// <returns>Localized Msg </returns>
        public string GetLocalizedKeyMsg(string keyMsg)
        {
            return $"{_SharedLocalizer.GetLocalizedString(keyMsg)}";

        }

        public static string ShortGUID()
        {
            return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
        }
        public static string GetRequestDomainName(string PathBase)
        {
            var PathBaseList = PathBase.Split(".").ToList();
            if (PathBaseList.Count <= 0)
                return string.Empty;
            PathBaseList.RemoveAt(0);

            string baseName = string.Join(".", PathBaseList);
            return baseName;
        }

        public string LocalizeString(string msg)
        {
            return _SharedLocalizer.GetLocalizedString(msg);
        }
    }
}
