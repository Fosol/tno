import React from 'react';
import { FormikText, FormikTextArea, Show } from 'tno-core';

export interface IReportSectionTextProps extends React.AllHTMLAttributes<HTMLDivElement> {
  /** Array index position of section. */
  index: number;
  /** Icon to display in header */
  icon?: React.ReactNode;
  /** Enable toggling the form values */
  showForm?: boolean;
  /** Form is disabled. */
  disabled?: boolean;
}

export const ReportSectionText = React.forwardRef<HTMLDivElement, IReportSectionTextProps>(
  ({ index, showForm, disabled, ...rest }, ref) => {
    return (
      <Show visible={showForm}>
        <FormikText
          name={`sections.${index}.settings.label`}
          label="Section heading:"
          disabled={disabled}
        />
        <FormikTextArea
          name={`sections.${index}.description`}
          label="Summary text:"
          disabled={disabled}
        />
      </Show>
    );
  },
);