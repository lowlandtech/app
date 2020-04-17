import { CardComment, CardData } from '@lowlandtech/api';
import { Action, createReducer, on } from '@ngrx/store';
import * as CardActions from './card.actions';

export interface Card {
  data: CardData;
  comments: CardComment[];
  loading: boolean;
  loaded: boolean;
}

export interface CardState {
  readonly card: Card;
}

export const cardInitialState: Card = {
  data: {
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
    }
  },
  comments: [],
  loaded: false,
  loading: false,
};

const reducer = createReducer(
  cardInitialState,
  on(CardActions.loadCardSuccess, (state, action) => ({
    ...state,
    data: action.card,
    loaded: true,
    loading: false,
  })),
  on(CardActions.loadCardFail, state => ({
    ...state,
    data: cardInitialState.data,
    loaded: false,
    loading: false,
  })),
  on(CardActions.addCommentSuccess, (state, action) => {
    const comments: CardComment[] = [action.comment, ...state.comments];
    return { ...state, comments };
  }),
  on(CardActions.deleteCommentSuccess, (state, action) => {
    const comments: CardComment[] = state.comments.filter(item => item.id !== action.commentId);
    return { ...state, comments };
  }),
  on(CardActions.initializeCard, state => cardInitialState),
  on(CardActions.deleteCardFail, state => cardInitialState),
  on(CardActions.loadCommentsSuccess, (state, action) => ({
    ...state,
    comments: action.comments,
  })),
  on(CardActions.loadCommentsFail, state => ({
    ...state,
    comments: cardInitialState.comments,
  })),
  on(CardActions.followSuccess, CardActions.unFollowSuccess, (state, action) => {
    const data: CardData = { ...state.data, author: action.profile };
    return { ...state, data };
  }),
  on(CardActions.favoriteSuccess, CardActions.unFavoriteSuccess, (state, action) => ({
    ...state,
    data: action.card,
  })),
);

export function cardReducer(state: Card | undefined, action: Action): Card {
  return reducer(state, action);
}
