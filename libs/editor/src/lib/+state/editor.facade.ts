import { CardData } from '@lowlandtech/api';
import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';

import * as EditorActions from './editor.actions';
import { EditorState } from './editor.reducer';
import { editorQuery } from './editor.selectors';

@Injectable()
export class EditorFacade {
  card$ = this.store.select(editorQuery.getCard);

  constructor(private store: Store<EditorState>) {}

  loadCard(id: string) {
    this.store.dispatch(EditorActions.loadCard({ id }));
  }

  loadCardSuccess(card: CardData) {
    this.store.dispatch(EditorActions.loadCardSuccess({ card }));
  }

  loadCardFail(error: Error) {
    this.store.dispatch(EditorActions.loadCardFail({ error }));
  }

  publishCard() {
    this.store.dispatch(EditorActions.publishCard());
  }

  initializeCard() {
    this.store.dispatch(EditorActions.initializeCard());
  }
}
