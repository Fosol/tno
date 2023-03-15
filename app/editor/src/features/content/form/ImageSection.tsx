import { useFormikContext } from 'formik';
import { useLookupOptions } from 'hooks';
import moment from 'moment';
import * as React from 'react';
import { filterEnabled } from 'store/hooks/lookup/utils';
import {
  FieldSize,
  FormikDatePicker,
  FormikSelect,
  IOptionItem,
  Row,
  Show,
  TimeInput,
} from 'tno-core';
import { getSourceOptions } from 'utils';

import { IContentForm } from './interfaces';
import { TopicForm } from './TopicForm';

// TODO: This is horrible to hardcode these sources, the image form is for any type of image and shouldn't be limited to a few sources.
const validSources = ['TC', 'PROVINCE', 'GLOBE', 'POST', 'SUN'];

interface IImageSectionProps {}

/** Contains form field in a layout specific to the image snippet. */
export const ImageSection: React.FunctionComponent<IImageSectionProps> = () => {
  const { values, setFieldValue } = useFormikContext<IContentForm>();
  const [{ sources, series, productOptions }] = useLookupOptions();

  const [sourceOptions, setSourceOptions] = React.useState<IOptionItem[]>([]);

  const source = sources.find((s) => s.id === values.sourceId);
  const program = series.find((s) => s.id === values.seriesId);

  React.useEffect(() => {
    setSourceOptions(
      getSourceOptions(sources.filter((s) => validSources.some((v) => v === s.code))),
    );
  }, [sources]);

  return (
    <Row>
      <FormikSelect
        name="sourceId"
        label="Media Outlet"
        width={FieldSize.Big}
        value={sourceOptions.find((mt) => mt.value === values.sourceId) ?? ''}
        onChange={(newValue: any) => {
          if (!!newValue) {
            const source = sources.find((ds) => ds.id === newValue.value);
            setFieldValue('sourceId', newValue.value);
            setFieldValue('otherSource', source?.code ?? '');
            if (!!source?.licenseId) setFieldValue('licenseId', source.licenseId);
            if (!!source?.productId) setFieldValue('productId', source.productId);
          }
        }}
        options={filterEnabled(sourceOptions, values.sourceId)}
        required={!values.otherSource || values.otherSource !== ''}
        isDisabled={!!values.tempSource}
      />
      <FormikSelect
        name="productId"
        value={productOptions.find((mt) => mt.value === values.productId) ?? ''}
        label="Product"
        width={FieldSize.Small}
        options={productOptions}
        required
      />
      <FormikDatePicker
        name="publishedOn"
        label="Published On"
        required
        autoComplete="false"
        width={FieldSize.Medium}
        selectedDate={!!values.publishedOn ? moment(values.publishedOn).toString() : undefined}
        value={!!values.publishedOn ? moment(values.publishedOn).format('MMM D, yyyy') : ''}
        onChange={(date) => {
          if (!!values.publishedOnTime) {
            const hours = values.publishedOnTime?.split(':');
            if (!!hours && !!date) {
              date.setHours(Number(hours[0]), Number(hours[1]), Number(hours[2]));
            }
          }
          setFieldValue('publishedOn', moment(date).format('MMM D, yyyy HH:mm:ss'));
        }}
      />
      <TimeInput
        name="publishedOnTime"
        label="Time"
        disabled={!values.publishedOn}
        width="7em"
        value={!!values.publishedOn ? values.publishedOnTime : ''}
        placeholder={!!values.publishedOn ? values.publishedOnTime : 'HH:MM:SS'}
        onChange={(e) => {
          const date = new Date(values.publishedOn);
          const hours = e.target.value?.split(':');
          if (!!hours && !!e.target.value && !e.target.value.includes('_')) {
            date.setHours(Number(hours[0]), Number(hours[1]), Number(hours[2]));
            setFieldValue('publishedOn', moment(date.toISOString()).format('MMM D, yyyy HH:mm:ss'));
          }
        }}
      />
      <Show visible={source?.useInTopics || program?.useInTopics}>
        <TopicForm />
      </Show>
    </Row>
  );
};