import {
  IContentListAdvancedFilter,
  IContentListFilter,
} from 'features/content/list-view/interfaces';
import React from 'react';
import { useParams } from 'react-router-dom';
import { convertTo, fromQueryString } from 'tno-core';

/** hook that creates advanced subscriber filter based off the url */
export const useParamsToFilter = () => {
  const params = useParams();
  const urlParams = new URLSearchParams(params.query);
  const search = React.useMemo(
    () =>
      fromQueryString(params.query, {
        arrays: [
          'sourceIds',
          'sentiment',
          'mediaTypeIds',
          'actions',
          'contentTypes',
          'contributorIds',
          'seriesIds',
          'tags',
        ],
        numbers: ['sourceIds', 'sentiment', 'mediaTypeIds', 'contributorIds', 'seriesIds'],
      }),
    [params.query],
  );
  const advancedSubscriberFilter: IContentListFilter & Partial<IContentListAdvancedFilter> =
    React.useMemo(() => {
      return {
        useUnpublished: urlParams.get('useUnpublished') === 'true' ?? false,
        keyword: urlParams.get('keyword') ?? '',
        searchTerm: urlParams.get('searchTerm') ?? '',
        inByline: urlParams.get('inByline') === 'true' ?? false,
        inHeadline: urlParams.get('inHeadline') === 'true' ?? false,
        inStory: urlParams.get('inStory') === 'true' ?? false,
        contentTypes: search.contentTypes?.map((v: any) => convertTo(v, 'string', undefined)),
        contributorIds: search.contributorIds?.map((v: any) => convertTo(v, 'number', undefined)),
        actions: search.actions?.map((v: any) => convertTo(v, 'string', undefined)),
        hasFile: urlParams.get('hasFile') === 'true' ?? false,
        headline: urlParams.get('headline') ?? '',
        pageIndex: convertTo(urlParams.get('pageIndex'), 'number', 0),
        pageSize: convertTo(urlParams.get('pageSize'), 'number', 100),
        sourceIds: search.sourceIds?.map((v: any) => convertTo(v, 'number', undefined)),
        mediaTypeIds: search.mediaTypeIds?.map((v: any) => convertTo(v, 'number', undefined)),
        sentiment: search.sentiment?.map((v: any) => convertTo(v, 'number', undefined)),
        startDate: urlParams.get('publishedStartOn') ?? '',
        endDate: urlParams.get('publishedEndOn') ?? '',
        storyText: urlParams.get('storyText') ?? '',
        boldKeywords: urlParams.get('boldKeywords') === 'true' ?? '',
        topStory: urlParams.get('actions') === 'Top Story' ?? false,
        section: urlParams.get('section') ?? '',
        seriesIds: search.seriesIds?.map((v: any) => convertTo(v, 'number', undefined)),
        page: urlParams.get('paperPage') ?? '',
        edition: urlParams.get('edition') ?? '',
        sort: [],
        tags: search.tags?.map((v: any) => convertTo(v, 'string', undefined)),
      };
      // only want this to update when the query changes
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [params.query]);

  return { advancedSubscriberFilter };
};