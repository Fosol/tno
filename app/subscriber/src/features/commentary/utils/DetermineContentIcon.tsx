import { BiRadio } from 'react-icons/bi';
import { FaNewspaper, FaTv } from 'react-icons/fa';
import { ContentTypeName } from 'tno-core';

import * as styled from './styled';

export interface IDetermineContentIconProps {
  contentType: ContentTypeName;
}

/** Determine Icon to display based on a commentary item's content type. */
export const DetermineContentIcon: React.FC<IDetermineContentIconProps> = ({ contentType }) => {
  const determineIcon = (contentType: ContentTypeName) => {
    switch (contentType) {
      case ContentTypeName.PrintContent:
        return <FaNewspaper />;
      case ContentTypeName.Story:
        return <FaTv />;
      case ContentTypeName.Snippet:
        return <BiRadio />;
      default:
        return <></>;
    }
  };
  return <styled.DetermineContentIcon>{determineIcon(contentType)}</styled.DetermineContentIcon>;
};