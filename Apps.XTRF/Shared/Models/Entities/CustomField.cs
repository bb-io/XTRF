namespace Apps.XTRF.Shared.Models.Entities;

public record CustomField(string Type, string Name, string Key);

public record CustomField<T>(string Type, string Name, string Key, T? Value) : CustomField(Type, Name, Key);