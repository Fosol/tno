import styled from 'styled-components';

export const FilterMedia = styled.div`
  .tool-bar {
    .reset {
      margin-top: 0.5em;
      margin-left: 0.5em;
      &:hover {
        cursor: pointer;
        transform: scale(1.1);
      }
    }
    .folder-sub-menu {
      margin-left: auto;
    }
    .or {
      margin-right: 0.5em;
    }
    .date-navigator {
      margin-left: auto;
      margin-right: auto;
      input {
        margin-top: 0.25em;
      }
    }
  }
  .table {
    width: 100%;
    .group {
      background-color: #f9f9f9 !important;
      font-family: ${(props) => props.theme.css?.bcSans};
    }
    .header {
      background-color: #f5f6fa;
      font-family: 'Roboto', sans-serif;
      font-size: 0.8em;
      /* box shadow only on bottom */
      box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.1);
      border: none;
      color: #7c7e8a;

      .column {
        background-color: #f5f6fa;
      }
    }
    .rows {
      cursor: pointer;
    }
  }
  .headline {
    /* link color */
    color: #3847aa;
  }

  .date-navigator {
    .calendar {
      color: #3847aa;
    }
    svg {
      align-self: center;
      height: 1.5em;
      width: 1.5em;
      &:hover {
        cursor: pointer;
      }
    }
    margin-bottom: 1em;
  }
`;