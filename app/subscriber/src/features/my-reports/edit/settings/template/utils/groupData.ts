import moment from 'moment';
import {
  getDistinct,
  IReportInstanceContentModel,
  IReportSectionChartTemplateModel,
  IReportSectionModel,
  TopicTypeName,
} from 'tno-core';

import { IGenerateChartDataOptions } from './generateChartData';
import { getChartData } from './getChartData';
import { getSentimentLabel } from './getSentimentLabel';
import { getSentimentValue } from './getSentimentValue';
import { IChartData } from './IChartData';
import { isStoryCount } from './isStoryCount';

export const groupData = (
  chart: IReportSectionChartTemplateModel,
  datasets: Record<string, IReportInstanceContentModel[]>,
  sections: IReportSectionModel[],
  options?: IGenerateChartDataOptions,
) => {
  const count = isStoryCount(chart);
  const groupBy = chart.sectionSettings.groupBy;
  let result: IChartData;

  // Group the data within each dataset.
  if (groupBy === '') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => (count ? 'Stories' : 'Average Sentiment'),
    });
  } else if (groupBy === 'mediaType') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.content?.mediaTypeId,
      getLabel: (c) => c?.content?.mediaType?.name,
    });
  } else if (groupBy === 'source') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.content?.otherSource,
      getLabel: (c) => c?.content?.source?.name,
    });
  } else if (groupBy === 'series') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.content?.seriesId,
      getLabel: (c) => c?.content?.series?.name,
    });
  } else if (groupBy === 'byline') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.content?.byline,
    });
  } else if (groupBy === 'contentType') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.content?.contentType,
    });
  } else if (groupBy === 'topicType') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groups: [TopicTypeName.Proactive, TopicTypeName.Issues],
      groupOn: (c) => undefined,
      isInGroup: (c, g) => c?.content?.topics.some((t) => t.topicType === g) ?? false,
    });
  } else if (groupBy === 'topicName') {
    const items = Object.entries(datasets)
      .map(([k, v]) => v)
      .flat();
    const topics = getDistinct(items.map((d) => d.content?.topics ?? []).flat(), (c) => c.name).map(
      (t) => t.name,
    );
    result = getChartData(chart, datasets, sections, {
      ...options,
      groups: topics,
      groupOn: (c) => undefined,
      isInGroup: (c, g) => c?.content?.topics.some((t) => t.name === g) ?? false,
    });
  } else if (groupBy === 'sentiment') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => getSentimentValue(c?.content),
    });
  } else if (groupBy === 'sentimentSimple') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => getSentimentLabel(c?.content),
    });
  } else if (groupBy === 'dayMonthYear') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => moment(c?.content?.publishedOn).tz('America/Vancouver').format('DD-MMM-YYYY'),
    });
  } else if (groupBy === 'monthDay') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => moment(c?.content?.publishedOn).tz('America/Vancouver').format('MMM-DD'),
    });
  } else if (groupBy === 'monthYear') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => moment(c?.content?.publishedOn).tz('America/Vancouver').format('MMM-YYYY'),
    });
  } else if (groupBy === 'year') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => moment(c?.content?.publishedOn).tz('America/Vancouver').format('YYYY'),
    });
  } else if (groupBy === 'reportSection') {
    result = getChartData(chart, datasets, sections, {
      ...options,
      groupOn: (c) => c?.sectionName,
    });
  } else {
    result = {
      labels: [],
      datasets: [],
    };
  }

  return result;
};