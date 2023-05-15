import React from 'react';
import { useAjaxWrapper, useLookup } from 'store/hooks';
import { IAdminState, useAdminStore } from 'store/slices';
import { IReportModel, useApiAdminReports } from 'tno-core';

interface IReportController {
  findAllReports: () => Promise<IReportModel[]>;
  getReport: (id: number) => Promise<IReportModel>;
  addReport: (model: IReportModel) => Promise<IReportModel>;
  updateReport: (model: IReportModel) => Promise<IReportModel>;
  deleteReport: (model: IReportModel) => Promise<IReportModel>;
  sendReport: (model: IReportModel, to: string) => Promise<IReportModel>;
}

export const useReports = (): [IAdminState, IReportController] => {
  const api = useApiAdminReports();
  const dispatch = useAjaxWrapper();
  const [state, store] = useAdminStore();
  const [, lookup] = useLookup();

  const controller = React.useMemo(
    () => ({
      findAllReports: async () => {
        const response = await dispatch<IReportModel[]>('find-all-reports', () =>
          api.findAllReports(),
        );
        store.storeReports(response.data);
        return response.data;
      },
      getReport: async (id: number) => {
        const response = await dispatch<IReportModel>('get-report', () => api.getReport(id));
        store.storeReports((reports) =>
          reports.map((ds) => {
            if (ds.id === response.data.id) return response.data;
            return ds;
          }),
        );
        return response.data;
      },
      addReport: async (model: IReportModel) => {
        const response = await dispatch<IReportModel>('add-report', () => api.addReport(model));
        store.storeReports((reports) => [...reports, response.data]);
        await lookup.getLookups();
        return response.data;
      },
      updateReport: async (model: IReportModel) => {
        const response = await dispatch<IReportModel>('update-report', () =>
          api.updateReport(model),
        );
        store.storeReports((reports) =>
          reports.map((ds) => {
            if (ds.id === response.data.id) return response.data;
            return ds;
          }),
        );
        await lookup.getLookups();
        return response.data;
      },
      deleteReport: async (model: IReportModel) => {
        const response = await dispatch<IReportModel>('delete-report', () =>
          api.deleteReport(model),
        );
        store.storeReports((reports) => reports.filter((ds) => ds.id !== response.data.id));
        await lookup.getLookups();
        return response.data;
      },
      sendReport: async (model: IReportModel, to: string) => {
        const response = await dispatch<IReportModel>('send-report', () =>
          api.sendReport(model, to),
        );
        return response.data;
      },
    }),
    [api, dispatch, lookup, store],
  );

  return [state, controller];
};