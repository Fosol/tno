import { IAuditColumnsModel, ISourceModel } from '.';

export interface ITopicScoreRuleModel extends IAuditColumnsModel {
  id: number;
  sourceId: number;
  source?: ISourceModel;
  seriesId?: number;
  section?: string;
  pageMin?: string;
  pageMax?: string;
  hasImage?: boolean;
  timeMin?: string;
  timeMax?: string;
  characterMin?: number;
  characterMax?: number;
  score: number;
  sortOrder: number;
  remove?: boolean;
}
