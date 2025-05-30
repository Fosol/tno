using TNO.Entities;

namespace TNO.TemplateEngine.Models;

/// <summary>
/// ContentModel class, provides a model that represents an content.
/// </summary>
public class ContentModel
{
    #region Properties
    /// <summary>
    /// get/set - Primary key to content.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// get/set - The section in the report this content belongs in.
    /// </summary>
    public string SectionName { get; set; } = "";

    /// <summary>
    /// get/set - The section in the report this content belongs in.
    /// </summary>
    public string SectionLabel { get; set; } = "";

    /// <summary>
    /// get/set - The status of the content.
    /// </summary>
    public ContentStatus Status { get; set; } = ContentStatus.Draft;

    /// <summary>
    /// get/set - The type of content and form to use.
    /// </summary>
    public ContentType ContentType { get; set; }

    /// <summary>
    /// get/set - Foreign key to source.
    /// </summary>
    public int? SourceId { get; set; }

    /// <summary>
    /// get/set - The data source.
    /// </summary>
    public SourceModel? Source { get; set; }

    /// <summary>
    /// get/set - The id of the source.
    /// </summary>
    public string OtherSource { get; set; } = "";

    /// <summary>
    /// get/set - Foreign key to media type.
    /// </summary>
    public int MediaTypeId { get; set; }

    /// <summary>
    /// get/set - The media type.
    /// </summary>
    public MediaTypeModel? MediaType { get; set; }

    /// <summary>
    /// get/set - Foreign key to license.
    /// </summary>
    public int LicenseId { get; set; }

    /// <summary>
    /// get/set - Foreign key to series (show/program).
    /// </summary>
    public int? SeriesId { get; set; }

    /// <summary>
    /// get/set - The series (show/program).
    /// </summary>
    public SeriesModel? Series { get; set; }

    /// <summary>
    /// get/set - Provides a way to dynamically add new series.
    /// </summary>
    public string? OtherSeries { get; set; }

    /// <summary>
    /// get/set - Foreign key to contributor.
    /// </summary>
    public int? ContributorId { get; set; }

    /// <summary>
    /// get/set - The contributor.
    /// </summary>
    public ContributorModel? Contributor { get; set; }

    /// <summary>
    /// get/set - Foreign key to user who owns the content.
    /// </summary>
    public int? OwnerId { get; set; }

    /// <summary>
    /// get/set - The content type.
    /// </summary>
    public UserModel? Owner { get; set; }

    /// <summary>
    /// get/set - The headline.
    /// </summary>
    public string Headline { get; set; } = "";

    /// <summary>
    /// get/set - The author or writer's name.
    /// </summary>
    public string Byline { get; set; } = "";

    /// <summary>
    /// get/set - A unique identifier for the content from the source.
    /// </summary>
    public string Uid { get; set; } = "";

    /// <summary>
    /// get/set - The print content edition.
    /// </summary>
    public string Edition { get; set; } = "";

    /// <summary>
    /// get/set - The print content section.
    /// </summary>
    public string Section { get; set; } = "";

    /// <summary>
    /// get/set - The print content page.
    /// </summary>
    public string Page { get; set; } = "";

    /// <summary>
    /// get/set - The story summary or abstract.
    /// </summary>
    public string Summary { get; set; } = "";

    /// <summary>
    /// get/set - The story body.
    /// </summary>
    public string Body { get; set; } = "";

    /// <summary>
    /// get/set - The source URL.
    /// </summary>
    public string SourceUrl { get; set; } = "";

    /// <summary>
    /// get/set - When the content was posted to MMI.
    /// </summary>
    public DateTime? PostedOn { get; set; }

    /// <summary>
    /// get/set - When the content has been or will be published.
    /// </summary>
    public DateTime? PublishedOn { get; set; }

    /// <summary>
    /// get/set - Whether content is hidden from search results.
    /// </summary>
    public bool IsHidden { get; set; }

    /// <summary>
    /// get/set - Whether the content has been approved for publishing.
    /// </summary>
    public bool IsApproved { get; set; }

    /// <summary>
    /// get/set - Private content is not searchable.
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// get/set - The position this content will be presented in.
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// get/set - The first file reference's image content if available.
    /// </summary>
    public string? ImageContent { get; set; }

    /// <summary>
    /// get - Dictionary of versions associated with this content.
    /// This provides subscribers the ability to customize the content.
    /// The key is the user's ID.
    /// </summary>
    public Dictionary<int, Entities.Models.ContentVersion> Versions { get; set; } = new();

    /// <summary>
    /// get/set - An array of actions.
    /// </summary>
    public IEnumerable<ContentActionModel> Actions { get; set; } = Array.Empty<ContentActionModel>();

    /// <summary>
    /// get/set - An array of categories.
    /// </summary>
    public IEnumerable<ContentTopicModel> Topics { get; set; } = Array.Empty<ContentTopicModel>();

    /// <summary>
    /// get/set - An array of tags.
    /// </summary>
    public IEnumerable<ContentTagModel> Tags { get; set; } = Array.Empty<ContentTagModel>();

    /// <summary>
    /// get/set - An array of time tracking entries.
    /// </summary>
    public IEnumerable<TimeTrackingModel> TimeTrackings { get; set; } = Array.Empty<TimeTrackingModel>();

    /// <summary>
    /// get/set - An array of labels.
    /// </summary>
    public IEnumerable<ContentLabelModel> Labels { get; set; } = Array.Empty<ContentLabelModel>();

    /// <summary>
    /// get/set - An array of tone pools.
    /// </summary>
    public IEnumerable<ContentTonePoolModel> TonePools { get; set; } = Array.Empty<ContentTonePoolModel>();

    /// <summary>
    /// get/set - An array of file references.
    /// </summary>
    public IEnumerable<FileReferenceModel> FileReferences { get; set; } = Array.Empty<FileReferenceModel>();

    /// <summary>
    /// get/set - Whether content is CBRA unqualified.
    /// </summary>
    public bool IsCBRAUnqualified { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of an ContentModel.
    /// </summary>
    public ContentModel() { }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName">The section in the report this content belongs in</param>
    /// <param name="sectionLabel"></param>
    public ContentModel(Entities.Content entity, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = entity?.Id ?? throw new ArgumentNullException(nameof(entity));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.Status = entity.Status;
        this.ContentType = entity.ContentType;
        this.SourceId = entity.SourceId;
        this.Source = entity.Source != null ? new SourceModel(entity.Source) : null;
        this.OtherSource = entity.OtherSource;
        this.MediaTypeId = entity.MediaTypeId;
        this.MediaType = entity.MediaType != null ? new MediaTypeModel(entity.MediaType) : null;
        this.LicenseId = entity.LicenseId;
        this.SeriesId = entity.SeriesId;
        this.Series = entity.Series != null ? new SeriesModel(entity.Series) : null;
        this.ContributorId = entity.ContributorId;
        this.Contributor = entity.Contributor != null ? new ContributorModel(entity.Contributor) : null;
        this.OwnerId = entity.OwnerId;
        this.Owner = entity.Owner != null ? new UserModel(entity.Owner) : null;
        this.Headline = entity.Headline;
        this.Byline = entity.Byline;
        this.Uid = entity.Uid;
        this.Edition = entity.Edition;
        this.Section = entity.Section;
        this.Page = entity.Page;
        this.Summary = entity.Summary;
        this.Body = entity.Body;
        this.SourceUrl = entity.SourceUrl;
        this.PostedOn = entity.PostedOn;
        this.PublishedOn = entity.PublishedOn;
        this.IsHidden = entity.IsHidden;
        this.IsApproved = entity.IsApproved;
        this.IsPrivate = entity.IsPrivate;
        this.SortOrder = sortOrder;

        this.Versions = entity.Versions;

        this.Actions = entity.ActionsManyToMany.Select(e => new ContentActionModel(e));
        this.Topics = entity.TopicsManyToMany.Select(e => new ContentTopicModel(e));
        this.Tags = entity.TagsManyToMany.Select(e => new ContentTagModel(e));
        this.TimeTrackings = entity.TimeTrackings.Select(e => new TimeTrackingModel(e));
        this.Labels = entity.Labels.Select(e => new ContentLabelModel(e));
        this.TonePools = entity.TonePoolsManyToMany.Select(e => new ContentTonePoolModel(e));
        this.FileReferences = entity.FileReferences.Select(e => new FileReferenceModel(e));
        this.IsCBRAUnqualified = entity.IsCBRAUnqualified;
    }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName"></param>
    /// <param name="sectionLabel"></param>
    public ContentModel(TNO.API.Areas.Editor.Models.Content.ContentModel model, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = model?.Id ?? throw new ArgumentNullException(nameof(model));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.Status = model.Status;
        this.ContentType = model.ContentType;
        this.SourceId = model.SourceId;
        this.Source = model.Source != null ? new SourceModel(model.Source) : null;
        this.OtherSource = model.OtherSource;
        this.MediaTypeId = model.MediaTypeId;
        this.MediaType = model.MediaType != null ? new MediaTypeModel(model.MediaType) : null;
        this.LicenseId = model.LicenseId;
        this.SeriesId = model.SeriesId;
        this.Series = model.Series != null ? new SeriesModel(model.Series) : null;
        this.ContributorId = model.ContributorId;
        this.Contributor = model.Contributor != null ? new ContributorModel(model.Contributor) : null;
        this.OwnerId = model.OwnerId;
        this.Owner = model.Owner != null ? new UserModel(model.Owner) : null;
        this.Headline = model.Headline;
        this.Byline = model.Byline;
        this.Uid = model.Uid;
        this.Edition = model.Edition;
        this.Section = model.Section;
        this.Page = model.Page;
        this.Summary = model.Summary;
        this.Body = model.Body;
        this.SourceUrl = model.SourceUrl;
        this.PostedOn = model.PostedOn;
        this.PublishedOn = model.PublishedOn;
        this.IsHidden = model.IsHidden;
        this.IsApproved = model.IsApproved;
        this.IsPrivate = model.IsPrivate;
        this.SortOrder = sortOrder;

        this.Versions = model.Versions;

        this.Actions = model.Actions.Select(e => new ContentActionModel(e));
        this.Topics = model.Topics.Select(e => new ContentTopicModel(e));
        this.Tags = model.Tags.Select(e => new ContentTagModel(e));
        this.TimeTrackings = model.TimeTrackings.Select(e => new TimeTrackingModel(e));
        this.Labels = model.Labels.Select(e => new ContentLabelModel(e));
        this.TonePools = model.TonePools.Select(e => new ContentTonePoolModel(e));
        this.FileReferences = model.FileReferences.Select(e => new FileReferenceModel(e));
        this.IsCBRAUnqualified = model.IsCBRAUnqualified;
    }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName"></param>
    /// <param name="sectionLabel"></param>
    public ContentModel(TNO.API.Areas.Services.Models.AVOverview.ContentModel model, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = model?.Id ?? throw new ArgumentNullException(nameof(model));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.ContentType = model.ContentType;
        this.Body = model.Body;
        this.IsApproved = model.IsApproved;
        this.SortOrder = sortOrder;
        this.FileReferences = model.FileReferences.Select(e => new FileReferenceModel(e));
    }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName"></param>
    /// <param name="sectionLabel"></param>
    public ContentModel(TNO.API.Areas.Services.Models.Content.ContentModel model, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = model?.Id ?? throw new ArgumentNullException(nameof(model));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.Status = model.Status;
        this.ContentType = model.ContentType;
        this.SourceId = model.SourceId;
        this.Source = model.Source != null ? new SourceModel(model.Source) : null;
        this.OtherSource = model.OtherSource;
        this.MediaTypeId = model.MediaTypeId;
        this.MediaType = model.MediaType != null ? new MediaTypeModel(model.MediaType) : null;
        this.LicenseId = model.LicenseId;
        this.SeriesId = model.SeriesId;
        this.Series = model.Series != null ? new SeriesModel(model.Series) : null;
        this.ContributorId = model.ContributorId;
        this.Contributor = model.Contributor != null ? new ContributorModel(model.Contributor) : null;
        this.OwnerId = model.OwnerId;
        this.Owner = model.Owner != null ? new UserModel(model.Owner) : null;
        this.Headline = model.Headline;
        this.Byline = model.Byline;
        this.Uid = model.Uid;
        this.Edition = model.Edition;
        this.Section = model.Section;
        this.Page = model.Page;
        this.Summary = model.Summary;
        this.Body = model.Body;
        this.SourceUrl = model.SourceUrl;
        this.PostedOn = model.PostedOn;
        this.PublishedOn = model.PublishedOn;
        this.IsHidden = model.IsHidden;
        this.IsApproved = model.IsApproved;
        this.IsPrivate = model.IsPrivate;
        this.SortOrder = sortOrder;

        this.Versions = model.Versions;

        this.Actions = model.Actions.Select(e => new ContentActionModel(e));
        this.Topics = model.Topics.Select(e => new ContentTopicModel(e));
        this.Tags = model.Tags.Select(e => new ContentTagModel(e));
        this.TimeTrackings = model.TimeTrackings.Select(e => new TimeTrackingModel(e));
        this.Labels = model.Labels.Select(e => new ContentLabelModel(e));
        this.TonePools = model.TonePools.Select(e => new ContentTonePoolModel(e));
        this.FileReferences = model.FileReferences.Select(e => new FileReferenceModel(e));
        this.IsCBRAUnqualified = model.IsCBRAUnqualified;
    }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName"></param>
    /// <param name="sectionLabel"></param>
    public ContentModel(TNO.API.Areas.Services.Models.Report.ContentModel model, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = model?.Id ?? throw new ArgumentNullException(nameof(model));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.Status = model.Status;
        this.ContentType = model.ContentType;
        this.SourceId = model.SourceId;
        this.Source = model.Source != null ? new SourceModel(model.Source) : null;
        this.OtherSource = model.OtherSource;
        this.MediaTypeId = model.MediaTypeId;
        this.MediaType = model.MediaType != null ? new MediaTypeModel(model.MediaType) : null;
        this.LicenseId = model.LicenseId;
        this.SeriesId = model.SeriesId;
        this.Series = model.Series != null ? new SeriesModel(model.Series) : null;
        this.ContributorId = model.ContributorId;
        this.Contributor = model.Contributor != null ? new ContributorModel(model.Contributor) : null;
        this.OwnerId = model.OwnerId;
        this.Owner = model.Owner != null ? new UserModel(model.Owner) : null;
        this.Headline = model.Headline;
        this.Byline = model.Byline;
        this.Uid = model.Uid;
        this.Edition = model.Edition;
        this.Section = model.Section;
        this.Page = model.Page;
        this.Summary = model.Summary;
        this.Body = model.Body;
        this.SourceUrl = model.SourceUrl;
        this.PostedOn = model.PostedOn;
        this.PublishedOn = model.PublishedOn;
        this.IsHidden = model.IsHidden;
        this.IsApproved = model.IsApproved;
        this.IsPrivate = model.IsPrivate;
        this.SortOrder = sortOrder;

        this.Versions = model.Versions;

        this.Actions = model.Actions.Select(e => new ContentActionModel(e));
        this.Topics = model.Topics.Select(e => new ContentTopicModel(e));
        this.Tags = model.Tags.Select(e => new ContentTagModel(e));
        this.TimeTrackings = model.TimeTrackings.Select(e => new TimeTrackingModel(e));
        this.Labels = model.Labels.Select(e => new ContentLabelModel(e));
        this.TonePools = model.TonePools.Select(e => new ContentTonePoolModel(e));
        this.FileReferences = model.FileReferences.Select(e => new FileReferenceModel(e));
        this.IsCBRAUnqualified = model.IsCBRAUnqualified;
    }

    /// <summary>
    /// Creates a new instance of an ContentModel, initializes with specified parameter.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="sortOrder"></param>
    /// <param name="sectionName"></param>
    /// <param name="sectionLabel"></param>
    public ContentModel(TNO.API.Areas.Services.Models.ReportInstance.ContentModel model, int sortOrder = 0, string sectionName = "", string sectionLabel = "")
    {
        this.Id = model?.Id ?? throw new ArgumentNullException(nameof(model));
        this.SectionName = sectionName;
        this.SectionLabel = sectionLabel;
        this.Status = model.Status;
        this.ContentType = model.ContentType;
        this.SourceId = model.SourceId;
        this.Source = model.Source != null ? new SourceModel(model.Source) : null;
        this.OtherSource = model.OtherSource;
        this.MediaTypeId = model.MediaTypeId;
        this.MediaType = model.MediaType != null ? new MediaTypeModel(model.MediaType) : null;
        this.LicenseId = model.LicenseId;
        this.SeriesId = model.SeriesId;
        this.Series = model.Series != null ? new SeriesModel(model.Series) : null;
        this.ContributorId = model.ContributorId;
        this.Contributor = model.Contributor != null ? new ContributorModel(model.Contributor) : null;
        this.OwnerId = model.OwnerId;
        this.Owner = model.Owner != null ? new UserModel(model.Owner) : null;
        this.Headline = model.Headline;
        this.Byline = model.Byline;
        this.Uid = model.Uid;
        this.Edition = model.Edition;
        this.Section = model.Section;
        this.Page = model.Page;
        this.Summary = model.Summary;
        this.Body = model.Body;
        this.SourceUrl = model.SourceUrl;
        this.PostedOn = model.PostedOn;
        this.PublishedOn = model.PublishedOn;
        this.IsHidden = model.IsHidden;
        this.IsApproved = model.IsApproved;
        this.IsPrivate = model.IsPrivate;
        this.SortOrder = sortOrder;

        this.Versions = model.Versions;

        this.Actions = model.Actions.Select(e => new ContentActionModel(e));
        this.Topics = model.Topics.Select(e => new ContentTopicModel(e));
        this.Tags = model.Tags.Select(e => new ContentTagModel(e));
        this.TimeTrackings = model.TimeTrackings.Select(e => new TimeTrackingModel(e));
        this.Labels = model.Labels.Select(e => new ContentLabelModel(e));
        this.TonePools = model.TonePools.Select(e => new ContentTonePoolModel(e));
        this.FileReferences = model.FileReferences.Select(e => new FileReferenceModel(e));
        this.IsCBRAUnqualified = model.IsCBRAUnqualified;
    }
    #endregion
}
