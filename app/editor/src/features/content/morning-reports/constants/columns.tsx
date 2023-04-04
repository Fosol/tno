import { Column, UseSortByColumnOptions } from 'react-table';
import { CellCheckbox, CellDate, CellEllipsis, ContentStatusName, IContentModel } from 'tno-core';

import { getStatusText } from '../../list-view/utils';

export const columns: (Column<IContentModel> & UseSortByColumnOptions<IContentModel>)[] = [
  {
    id: 'id',
    Header: 'Headline',
    accessor: 'headline',
    width: 5,
    Cell: ({ value }) => (
      <CellEllipsis data-tooltip-id="main-tooltip" data-tooltip-content={value}>
        {value}
      </CellEllipsis>
    ),
  },
  {
    id: 'otherSource',
    Header: 'Source',
    width: 1,
    accessor: 'otherSource',
    Cell: ({ value }) => <CellEllipsis>{value}</CellEllipsis>,
  },
  {
    id: 'productId',
    Header: 'Product',
    width: 1,
    accessor: (row) => row.product?.name,
    Cell: ({ value }: { value: string }) => <CellEllipsis>{value}</CellEllipsis>,
  },
  {
    id: 'page',
    Header: 'Section:Page',
    width: 1,
    accessor: (row) => (row.section ? `${row.section}:${row.page}` : row.page),
    Cell: ({ value }: { value: string }) => (
      <CellEllipsis data-tooltip-id="main-tooltip" data-tooltip-content={value}>
        {value}
      </CellEllipsis>
    ),
  },
  {
    id: 'status',
    Header: 'Status',
    width: 1,
    accessor: (row) => <div className="center">{getStatusText(row.status)}</div>,
  },
  {
    id: 'publishedOn',
    Header: 'Pub Date',
    width: 2,
    accessor: (row) => row.publishedOn ?? row.createdOn,
    Cell: ({ value }: any) => (
      <div className="center">
        <CellDate value={value} />
      </div>
    ),
  },
  {
    id: 'use',
    Header: 'Use',
    disableSortBy: true,
    width: 1,
    accessor: (row) =>
      row.status === ContentStatusName.Publish || row.status === ContentStatusName.Published,
    Cell: ({ value }: { value: boolean }) => {
      return (
        <div className="center">
          <CellCheckbox checked={value} />
        </div>
      );
    },
  },
];