using Microsoft.AspNetCore.Components.Forms;

namespace WeTube.Models
{
    public class FileUploadModel
    {
        public IReadOnlyList<IBrowserFile> Files { get; set; }
    }
}
