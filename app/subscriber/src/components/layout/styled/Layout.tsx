import styled from 'styled-components';

import { ILayoutProps } from '..';

export const Layout = styled.div<ILayoutProps>`
  &:not(.unauth) {
    main {
      overflow: clip auto;
      height: calc(100dvh - 4.75rem);
    }
    .header {
      grid-area: header;
    }

    .search-bar {
      grid-area: search-bar;
    }

    .nav-bar {
      grid-area: nav-bar;
    }

    .contents-container {
      grid-area: content;
    }

    @media (max-width: 900px) {
      .grid-container {
        grid-template-areas:
          'header header'
          'search-bar search-bar'
          'nav-bar content';
      }
    }
    @media (min-width: 900px) {
      .search-bar {
        display: none;
      }
      .grid-container {
        grid-template-areas:
          'header header'
          'nav-bar content';
      }
    }
    .grid-container {
      height: 100dvh;
      overflow: clip;
      display: grid;
      transition: 300ms;
      background-color: ${(props) => props.theme.css.bkMain};
      grid-auto-columns: max-content 8fr;
      grid-auto-rows: auto;
    }
  }
`;
