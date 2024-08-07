DO $$
BEGIN

-- Update Frontpages Report with latest template on body and subject.
UPDATE public."report_template" SET
  "subject" = '@using TNO.TemplateEngine
DEV | @Settings.Subject.Text @(Settings.Subject.ShowTodaysDate ? $" - {ReportExtensions.GetTodaysDate():dd-MMM-yyyy}" :
"")',
    "body" = '@inherits RazorEngineCore.RazorEngineTemplateBase<TNO.TemplateEngine.Models.Reports.ReportEngineContentModel>
@using System
@using System.Linq
@using TNO.Entities
@using TNO.TemplateEngine
@{
    var pageBreak = Settings.Sections.UsePageBreaks ? "page-break-after: always;" : "";
    var utcOffset = ReportExtensions.GetUtcOffset(System.DateTime.Now, "Pacific Standard Time");
}
<h1 id="top" style="margin: 0; padding: 0;">@Settings.Subject.Text</h1>
<a name="top"></a>
<div style="color:red;">DO NOT FORWARD THIS EMAIL TO ANYONE</div>
<br />

@if (Content.Count() == 0)
{
    <p>There is no content in this report.</p>
}
@if (ViewOnWebOnly)
{
    <a href="@($"{SubscriberAppUrl}landing/todaysfrontpages")" target="_blank">Click to view this report online</a>
}
else
{
    foreach (var section in Sections)
    {
        var sectionContent = section.Value.Content.ToArray();
        if (section.Value.IsEnabled &&
        (sectionContent.Length > 0 ||
        !section.Value.Settings.HideEmpty ||
        (section.Value.SectionType == ReportSectionType.TableOfContents)
        && Content.Any()))
        {
            <div style="@pageBreak;display:flex; flex-flow:row wrap;">
                @* Full Stories *@
                @if (section.Value.Settings.ShowFullStory)
                {
                    for (var i = 0; i < sectionContent.Length; i++)
                    {
                        var content = sectionContent[i];
                        <div style="width:20rem;padding:0.5rem;">
                            <h3>@content.Source?.Name</h3>
                            <p>@content.PublishedOn?.AddHours(utcOffset).ToString("dddd, MMMM d, yyyy")</p>
                            @if (!string.IsNullOrEmpty(content.ImageContent))
                            {
                                var src = $"data:{content.ContentType};base64," + content.ImageContent;
                                <p><img src="@src" alt="@content.FileReferences.FirstOrDefault()?.FileName" /></p>
                            }
                        </div>
                    }
                }
            </div>
        }
    }
}

<p style="font-size: 9pt; margin-top: 0.5rem;">
    Terms of Use - This summary is a service provided by Government Communications and Public Engagement and is only
    intended for original addressee. All content is the copyrighted property of a third party creator of the material.
    Copying, retransmitting, archiving, redistributing, selling, licensing, or emailing the material to any third party
    or any employee of the Province who is not authorized to access the material is prohibited.
</p>'
WHERE "name" = 'Frontpages Report';

END $$;
