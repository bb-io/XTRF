using Apps.XTRF.Shared.Invocables;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.XTRF.Shared.Actions.Base;

public class BaseFileActions : XtrfInvocable
{
    private readonly IFileManagementClient _fileManagementClient;
    
    protected BaseFileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    protected async Task<byte[]> DownloadFile(FileReference fileReference)
    {
        var fileStream = await _fileManagementClient.DownloadAsync(fileReference);
        return await fileStream.GetByteData();
    }

    protected async Task<FileReference> UploadFile(byte[] fileBytes, string contentType, string filename)
    {
        using var stream = new MemoryStream(fileBytes);
        return await _fileManagementClient.UploadAsync(stream, contentType, filename);
    }
}