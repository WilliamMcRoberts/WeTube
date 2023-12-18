using Microsoft.AspNetCore.Components.Forms;

namespace WeTube.Models;

public class InputFileModel
{
    public IBrowserFile? File { get; set; }
    public string VideoName { get; set; } = string.Empty;
    public string VideoPath { get; set; } = string.Empty;
}
