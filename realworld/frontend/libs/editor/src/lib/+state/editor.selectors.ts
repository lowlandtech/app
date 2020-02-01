import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Editor } from './editor.reducer';

const getEditor = createFeatureSelector<Editor>('editor');
export const getArticle = createSelector(
  getEditor,
  (state: Editor) => state.card,
);

export const editorQuery = {
  getArticle,
};
