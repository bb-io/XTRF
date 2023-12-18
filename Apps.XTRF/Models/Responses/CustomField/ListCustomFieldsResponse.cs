﻿using Blackbird.Applications.Sdk.Common;

namespace Apps.XTRF.Models.Responses.CustomField;

public record ListCustomFieldsResponse([Display("Custom fields")] IEnumerable<Entities.CustomField> CustomFields);