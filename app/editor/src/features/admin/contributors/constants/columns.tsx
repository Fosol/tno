import { CellCheckbox, CellEllipsis, IContributorModel, ITableHookColumn } from 'tno-core';

export const columns: ITableHookColumn<IContributorModel>[] = [
  {
    label: 'Name',
    name: 'name',
    width: 2,
    cell: (cell) => <CellEllipsis>{cell.original.name}</CellEllipsis>,
  },
  {
    label: 'Source',
    name: 'sourceId',
    width: 2,
    cell: (cell) => <CellEllipsis>{cell.original.source?.name}</CellEllipsis>,
  },
  {
    label: 'Description',
    name: 'description',
    width: 7,
    cell: (cell) => <CellEllipsis>{cell.original.description}</CellEllipsis>,
  },
  {
    label: 'Enabled',
    name: 'isEnabled',
    width: 1,
    cell: (cell) => <CellCheckbox checked={cell.original.isEnabled} />,
  },
];