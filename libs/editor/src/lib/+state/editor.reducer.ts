import { CardData, CardStatus } from '@spotacard/api';

import { Action, createReducer, on } from '@ngrx/store';
import * as EditorActions from './editor.actions';

export interface Editor {
  card: CardData;
}

export interface EditorState {
  readonly editor: Editor;
}

export const editorInitialState: Editor = {
  card: {
    slug: '',
    title: '',
    description: '',
    body: '',
    tagList: [],
    createdAt: '',
    updatedAt: '',
    favorited: false,
    favoritesCount: 0,
    author: {
      username: '',
      bio: '',
      image: '',
      following: false,
      loading: false,
    },
    status: CardStatus.NORMAL
  },
};

const reducer = createReducer(
  editorInitialState,
  on(EditorActions.loadCardSuccess, (state, action) => ({ ...state, card: action.card })),
  on(EditorActions.loadCardFail, EditorActions.initializeCard, () => editorInitialState),
);

export function editorReducer(state: Editor | undefined, action: Action): Editor {
  return reducer(state, action);
}
