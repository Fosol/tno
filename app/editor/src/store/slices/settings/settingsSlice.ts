import { createSlice, PayloadAction } from '@reduxjs/toolkit';

import { ISettingsState } from './interfaces';

export const initialSettingsState: ISettingsState = {
  loadingState: 0,
  isReady: false,
};

export const settingsSlice = createSlice({
  name: 'settings',
  initialState: initialSettingsState,
  reducers: {
    storeSettingsLoading(state: ISettingsState, action: PayloadAction<number>) {
      state.loadingState = action.payload;
    },
    storeSettingsValues(state: ISettingsState, action: PayloadAction<ISettingsState>) {
      state.isReady = action.payload.isReady;
      state.loadingState = action.payload.loadingState;
      state.featuredStoryActionId = action.payload.featuredStoryActionId;
      state.commentaryActionId = action.payload.commentaryActionId;
      state.topStoryActionId = action.payload.topStoryActionId;
      state.alertActionId = action.payload.alertActionId;
      state.editorUrl = action.payload.editorUrl;
      state.subscriberUrl = action.payload.subscriberUrl;
      state.defaultReportTemplateId = action.payload.defaultReportTemplateId;
      state.frontpageFilterId = action.payload.frontpageFilterId;
      state.excludeBylineIds = action.payload.excludeBylineIds;
      state.excludeSourceIds = action.payload.excludeSourceIds;
      state.morningReportId = action.payload.morningReportId;
      state.frontpageImageMediaTypeId = action.payload.frontpageImageMediaTypeId;
      state.frontPageImagesReportId = action.payload.frontPageImagesReportId;
      state.topStoryAlertId = action.payload.topStoryAlertId;
      state.basicAlertTemplateId = action.payload.basicAlertTemplateId;
    },
  },
});

export const { storeSettingsLoading, storeSettingsValues } = settingsSlice.actions;
