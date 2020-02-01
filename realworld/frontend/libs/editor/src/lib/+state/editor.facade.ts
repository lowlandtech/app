import { ArticleData } from '@spotacard/api';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import * as EditorActions from './editor.actions';
import { EditorState } from './editor.reducer';
import { editorQuery } from './editor.selectors';

@Injectable()
export class EditorFacade {
  card$ = this.store.select(editorQuery.getArticle);

  constructor(private store: Store<EditorState>) {}

  loadArticle(id: string) {
    this.store.dispatch(EditorActions.loadArticle({ id }));
  }

  loadArticleSuccess(card: ArticleData) {
    this.store.dispatch(EditorActions.loadArticleSuccess({ card }));
  }

  loadArticleFail(error: Error) {
    this.store.dispatch(EditorActions.loadArticleFail({ error }));
  }

  publishArticle() {
    this.store.dispatch(EditorActions.publishArticle());
  }

  initializeArticle() {
    this.store.dispatch(EditorActions.initializeArticle());
  }
}
