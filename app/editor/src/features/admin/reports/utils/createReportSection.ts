import { IReportSectionModel, ReportSectionTypeName } from 'tno-core';

export const createReportSection = (
  reportId: number,
  type: ReportSectionTypeName,
): IReportSectionModel => {
  return {
    id: 0,
    reportId: reportId,
    name: crypto.randomUUID(),
    description: '',
    sortOrder: 0,
    isEnabled: true,
    chartTemplates: [],
    settings: {
      label: '',
      sectionType: type,
      useAllContent: type === ReportSectionTypeName.MediaAnalytics,
      removeDuplicates: false,
      showHeadlines:
        type === ReportSectionTypeName.Content || type === ReportSectionTypeName.TableOfContents,
      showFullStory: type === ReportSectionTypeName.Content,
      showImage: type === ReportSectionTypeName.Gallery,
      hideEmpty: false,
      direction:
        type === ReportSectionTypeName.Gallery || type === ReportSectionTypeName.MediaAnalytics
          ? 'row'
          : 'column',
      groupBy: '',
      sortBy: '',
    },
  };
};