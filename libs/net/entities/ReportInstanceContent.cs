using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TNO.Entities;

/// <summary>
/// ReportInstanceContent class, provides a DB model to associate content with a instance of a report.
/// </summary>
[Table("report_instance_content")]
public class ReportInstanceContent : AuditColumns
{
    #region Properties
    /// <summary>
    /// get/set - Primary key and foreign key to the report instance.
    /// </summary>
    [Key]
    [Column("report_instance_id")]
    public long InstanceId { get; set; }

    /// <summary>
    /// get/set - The report instance.
    /// </summary>
    public ReportInstance? Instance { get; set; }

    /// <summary>
    /// get/set - Primary key and foreign key to the content.
    /// </summary>
    [Key]
    [Column("content_id")]
    public long ContentId { get; set; }

    /// <summary>
    /// get/set - The content for this report.
    /// </summary>
    public Content? Content { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of a ReportInstanceContent object.
    /// </summary>
    protected ReportInstanceContent() { }

    /// <summary>
    /// Creates a new instance of a ReportInstanceContent object, initializes with specified parameters.
    /// </summary>
    /// <param name="instance"></param>
    /// <param name="content"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ReportInstanceContent(ReportInstance instance, Content content)
    {
        this.Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        this.InstanceId = instance.Id;
        this.Content = content ?? throw new ArgumentNullException(nameof(content));
        this.ContentId = content.Id;
    }

    /// <summary>
    /// Creates a new instance of a ReportInstanceContent object, initializes with specified parameters.
    /// </summary>
    /// <param name="instanceId"></param>
    /// <param name="contentId"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public ReportInstanceContent(long instanceId, long contentId)
    {
        this.InstanceId = instanceId;
        this.ContentId = contentId;
    }
    #endregion
}