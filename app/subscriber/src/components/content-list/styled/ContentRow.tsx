import styled from 'styled-components';
import { Col } from 'tno-core';

export const ContentRow = styled(Col)`
  margin-bottom: 0.5rem;
  padding-top: 0.25rem;
  padding-bottom: 0.25rem;
  border-bottom: 1px solid ${(props) => props.theme.css.bkStaticGray};
  &:hover {
    cursor: pointer;
    background-color: ${(props) => props.theme.css.highlightYellow};
    .teaser {
      background-color: ${(props) => props.theme.css.highlightYellowDarker};
    }
  }
  &.checked {
    background-color: ${(props) => props.theme.css.highlightYellow};
    .teaser {
      background-color: ${(props) => props.theme.css.highlightYellowDarker};
    }
  }

  .media-playback {
    video {
      height: 270px;
      width: 480px;
      margin-left: auto;
      margin-right: auto;
    }
    audio {
      margin-left: auto;
      margin-right: auto;
    }
    .copyright-text {
      margin-top: 0.5rem;
      font-size: 0.9rem;
      display: flex;
      margin-left: auto;
      margin-right: auto;
      color: ${(props) => props.theme.css.iconGrayColor};
      svg {
        margin-right: 0.25rem;
      }
    }
  }
  .positive {
    color: ${(props) => props.theme.css.toneGreen};
  }
  .negative {
    color: ${(props) => props.theme.css.toneRed};
  }
  .neutral {
    color: ${(props) => props.theme.css.toneYellow};
  }
  .tone-icon {
    height: 20px;
    width: 20px;
    margin-right: 0.5rem;
  }
  .checkbox {
    margin-right: 0.5rem;
  }
  .date {
    font-family: ${(props) => props.theme.css.fPrimary};
    display: flex;
    margin-right: 1rem;
    align-items: center;
    justify-content: center;
    font-size: 1.1rem;
  }
  .section,
  .page-number {
    font-family: ${(props) => props.theme.css.fPrimary};
  }

  .page-number {
    margin-left: 0.25rem;
  }

  .new-tab {
    margin-right: 0.5rem;
  }

  .play-icon,
  .eye-slash,
  .new-tab {
    height: 20px;
    width: 20px;
    color: ${(props) => props.theme.css.btnBkPrimary};
    &:hover {
      cursor: pointer;
      transform: scale(1.1);
    }
  }
  .teaser {
    &.with-grip {
      margin-left: 5.75rem;
    }
    margin-left: 4.25rem;
    background-color: ${(props) => props.theme.css.teaserBackground};
    font-family: ${(props) => props.theme.css.fSecondary};
    font-size: 0.9rem;
    border-radius: 0.25rem;
    padding: 0.2rem;
    margin-bottom: 0.25rem;
    cursor: default;
  }
  .headline {
    background: none;
    border: none;
    margin-right: auto;
    color: blue;
    padding: 0;
    cursor: pointer;
    font-family: ${(props) => props.theme.css.fSecondary};
  }

  .headline:hover,
  .headline:focus {
    color: darkblue;
    outline: none;
  }
  .checked {
    background-color: ${(props) => props.theme.css.highlightYellow};
    .teaser {
      background-color: ${(props) => props.theme.css.highlightYellowDarker};
    }
  }
  .grip {
    height: 20px;
    margin-right: 0.5rem;
    color: ${(props) => props.theme.css.btnBkPrimary};
    cursor: grab;
  }
`;