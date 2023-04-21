import React from 'react';

import { Text } from '../form';
import { ITableProps, SortFlag, TablePager, useTable } from '.';
import * as styled from './styled';

export const FlexboxTable = <T extends object>({
  rowId,
  columns,
  data,
  ...rest
}: ITableProps<T>) => {
  const table = useTable({
    rowId,
    columns,
    data,
    options: {
      isMulti: rest.isMulti,
      onRowClick: rest.onRowClick,
      onCellClick: rest.onCellClick,
      onColumnClick: rest.onColumnClick,
      onSelectedChanged: rest.onSelectedChanged,
      stopPropagation: rest.stopPropagation,
      showHeader: rest.showHeader,
      showFooter: rest.showFooter,
      selectedRowIds: rest.selectedRowIds,
      activeRowId: rest.activeRowId,
    },
    paging: {
      manualPaging: rest.manualPaging,
      pageIndex: rest.pageIndex,
      pageSize: rest.pageSize,
      pageCount: rest.pageCount,
      pageButtons: rest.pageButtons,
      showPaging: rest.showPaging,
      scrollSize: rest.scrollSize,
      onPageChange: rest.onPageChange,
    },
    sorting: {
      showSort: rest.showSort,
      sortOrder: rest.sortOrder,
      onSortChange: rest.onSortChange,
    },
    filter: {
      showFilter: rest.showFilter,
      search: rest.search,
      onFilterChange: rest.onFilterChange,
    },
    grouping: {
      groupBy: rest.groupBy,
      groupHeading: rest.groupHeading,
    },
  });
  const [search, setSearch] = React.useState(table.search);

  const style = {
    className: `table${rest.className ? ` ${rest.className}` : ''}`,
    columns: table.columns,
    scrollSize: table.scrollSize,
  };

  return (
    <styled.FlexboxTable {...style}>
      {table.showFilter && (
        <div className="filter">
          <div>
            <Text
              name="filter"
              placeholder="Search by keyword"
              value={search}
              onChange={(e) => {
                setSearch(e.target.value);
                table.applyFilter(e.target.value);
              }}
            />
          </div>
        </div>
      )}
      {table.options.showHeader && (
        <header className="header">
          {table.header.columns
            .filter((col) => col.isVisible)
            .map((col, index) => (
              <div
                key={col.index}
                className={`column col-${index}`}
                onClick={(e) => {
                  if (table.options.stopPropagation) e.stopPropagation();
                  // table.options.onColumnClick?.(col, e);
                }}
              >
                {col.label && <span className="label">{col.label}</span>}
                {table.showSort && (
                  <div
                    className="sort"
                    onClick={() => {
                      table.setSortOrder([
                        ...table.sortOrder.filter((sort) => sort.id !== col.name.toString()),
                        {
                          id: col.name.toString(),
                          sort: col.sort,
                          isSorted: !col.isSorted ? true : col.isSortedDesc ? false : true,
                          isSortedDesc: col.isSorted ? !col.isSortedDesc : col.isSortedDesc,
                        },
                      ]);
                    }}
                  >
                    <SortFlag column={col} />
                  </div>
                )}
              </div>
            ))}
        </header>
      )}
      <div className="rows">
        {table.groupBy && (
          <div className="groups">
            {table.groups.map((group) => {
              return (
                <React.Fragment key={group.key}>
                  <div className="group">{group.key}</div>
                  <div className="group-rows">
                    {group.rows.map((row) => {
                      return (
                        <div
                          className={`row${row.isSelected ? ' selected' : ''}${
                            row.isActive ? ' active' : ''
                          }`}
                          key={`${row.original[row.rowId]}`}
                          onClick={(e) => {
                            if (table.options.stopPropagation) e.stopPropagation();
                            table.options.onRowClick?.(row, e);
                          }}
                        >
                          {row.cells
                            .filter((col) => col.isVisible)
                            .map((col, index) => (
                              <div
                                className={`column col-${index}`}
                                key={`${index}`}
                                onClick={(e) => {
                                  if (table.options.stopPropagation) e.stopPropagation();
                                  table.options.onCellClick?.(col, e);
                                }}
                              >
                                {col.cell(col)}
                              </div>
                            ))}
                        </div>
                      );
                    })}
                  </div>
                </React.Fragment>
              );
            })}
          </div>
        )}
        {!table.groupBy &&
          table.page.map((row) => {
            return (
              <div
                className={`row${row.isSelected ? ' selected' : ''}${
                  row.isActive ? ' active' : ''
                }`}
                key={`${row.original[row.rowId]}`}
                onClick={(e) => {
                  if (table.options.stopPropagation) e.stopPropagation();
                  table.options.onRowClick?.(row, e);
                }}
              >
                {row.cells
                  .filter((col) => col.isVisible)
                  .map((col, index) => (
                    <div
                      className={`column col-${index}`}
                      key={`${index}`}
                      onClick={(e) => {
                        if (table.options.stopPropagation) e.stopPropagation();
                        table.options.onCellClick?.(col, e);
                      }}
                    >
                      {col.cell(col)}
                    </div>
                  ))}
              </div>
            );
          })}
      </div>
      {table.options.showFooter && <footer className="footer"></footer>}
      {!table.groupBy && <TablePager table={table} />}
    </styled.FlexboxTable>
  );
};