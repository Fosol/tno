using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using TNO.Core.Data;

namespace TNO.Entities;

/// <summary>
/// AVOverviewTemplate class, provides a DB model to manage different types of overviews.
/// </summary>
[Cache("av_overview_template_section")]
[Table("av_overview_template_section")]
public class AVOverviewTemplateSection : BaseType<int>
{
    #region Properties
    /// <summary>
    /// get/set - The foreign key to the overview template.
    /// </summary>
    [Column("av_overview_template_id")]
    public int AVOverviewTemplateId { get; set; }

    /// <summary>
    /// get/set - Foreign key to the source.
    /// </summary>
    [Column("source_id")]
    public int? SourceId { get; set; }

    /// <summary>
    /// get/set - The source code to identify the publisher.
    /// </summary>
    [Column("other_source")]
    public string OtherSource { get; set; } = "";

    /// <summary>
    /// get/set - The foreign key for series
    /// </summary>
    [Column("series_id")]
    public int? SeriesId { get; set; }

    /// <summary>
    /// get/set - The anchors for the template.
    /// </summary>
    [Column("anchors")]
    public string Anchors { get; set; } = "";

    /// <summary>
    /// get/set - The start time for the template section
    /// </summary>
    [Column("start_time")]
    public string? StartTime { get; set; }


    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of a av overview template object.
    /// </summary>
    protected AVOverviewTemplateSection() : base() { }

    /// <summary>
    /// Creates a new instance of a av overview template object, initializes with specified parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="type"></param>
    /// <param name="owner"></param>
    /// <param name="template"></param>
    public AVOverviewTemplateSection(string name) : base(name)
    {

    }
    #endregion
}