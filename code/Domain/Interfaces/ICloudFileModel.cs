namespace Domain.Interfaces;

public interface ICloudFileModel
{
    string FileName { get; set; }
    string Uri { get; set; }
    string FileType { get; set; }
    DateTime? CreateDate { get; set; }
    DateTime? ModificationDate { get; set; }
}
