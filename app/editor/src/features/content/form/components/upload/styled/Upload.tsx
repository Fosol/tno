import styled from 'styled-components';

export const Upload = styled.div`
  display: flex;
  flex-direction: row;
  width: fit-content;
  .indicator {
    align-self: center;
    padding-right: 0.25em;
  }

  .upload-image {
    height: 5rem;
    width: 5rem;
    align-self: center;
  }

  .text {
    align-self: center;
    button {
      margin-top: 0.5rem;
    }
  }
  .choose {
    font-weight: bold;
    font-size: 1rem;
  }
  .upload-box {
    margin-top: 1.5rem;
    border: 1px dotted #ccc;
    width: 500px;
    min-height: 24em;
  }
  .body {
    margin-top: 15%;
  }
  .file-action {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
  }

  .file-name {
    border-color: ${(props) => props.theme.css.primaryColor};
    div {
      max-width: 13.5em;
      display: inline-block;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
  }
  .delete {
    justify-self: flex-end;
  }

  .upload-image {
    color: ${(props) => props.theme.css.primaryLightColor};
    margin-bottom: 1rem;
  }
`;