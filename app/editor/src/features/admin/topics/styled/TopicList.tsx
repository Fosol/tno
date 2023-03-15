import styled from 'styled-components';

export const TopicList = styled.div`
  width: auto;
  min-height: 100%;
  display: flex;
  justify-content: center;

  .form-page {
    display: flex;
    gap: 1em;
  }

  .filter-bar {
    display: flex;
    align-items: center;
    input {
      margin-top: 3.5%;
    }
    button {
      background-color: white;
    }
    background-color: #f5f5f5;
  }

  .row-header {
    font-weight: bold;
    padding: 0.25em 0.5em 0.25em 0.5em;
    border-top: solid 2px black;
    border-bottom: solid 2px black;
  }

  .row {
    padding: 0.5em 0.5em 0 0.5em;

    &:nth-child(4n) {
      background-color: #f5f5f5;
    }

    textarea {
      height: 39.6px;
    }

    &.adding {
      background-color: ${(props) => props.theme.css.primaryLightColor};
      border-radius: 0.5em;
    }

    .actions {
      width: 78px;
    }

    button.move {
      height: 40%;

      padding: 0;
      margin-right: 0.5em;

      svg {
        height: 100%;
      }
    }
  }

  .add-row {
    background-color: ${(props) => props.theme.css.borderColor};
    height: 5px;
    align-content: center;
    justify-content: center;
    z-index: 2;

    &:hover {
      background-color: ${(props) => props.theme.css.lightVariantColor};
      cursor: pointer;

      svg {
        background-color: white;
        color: ${(props) => props.theme.css.activeColor};
        border-radius: 1em;
      }
    }
  }
`;