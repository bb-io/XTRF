﻿namespace Apps.XTRF.Shared.Models.Entities;

public record CustomField(string Type, string Name, string Key);

public record CustomField<T>(string Type, string Name, string Key, T? Value) : CustomField(Type, Name, Key);

public record CustomBooleanField(string Type, string Key, string Name, bool Value);

public record CustomDecimalField(string Type, string Key, string Name, decimal Value);

public record CustomDateTimeField(string Type, string Key, string Name, DateTime Value);