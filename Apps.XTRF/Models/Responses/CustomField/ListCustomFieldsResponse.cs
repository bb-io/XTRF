using Apps.XTRF.Models.Responses.Entities;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.CustomField;

public record ListCustomFieldsResponse([property: Display("Custom fields")] CustomFieldEntity[] CustomFields);