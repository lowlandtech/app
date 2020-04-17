import { createAction, props } from '@ngrx/store';
import { CardData } from '@lowlandtech/api';

import { CardListConfig } from './card-list.reducer';

export const setListPage = createAction('[card-list] SET_LIST_PAGE', props<{ page: number }>());

export const setListConfig = createAction('[card-list] SET_LIST_CONFIG', props<{ config: CardListConfig }>());

export const loadCards = createAction('[card-list] LOAD_ARTICLES');

export const loadCardsSuccess = createAction(
  '[card-list] LOAD_ARTICLES_SUCCESS',
  props<{ cards: CardData[]; cardsCount: number }>(),
);

export const loadCardsFail = createAction('[card-list] LOAD_ARTICLES_FAIL', props<{ error: Error }>());

export const favorite = createAction('[card-list] FAVORITE', props<{ slug: string }>());

export const favoriteSuccess = createAction('[card-list] FAVORITE_SUCCESS', props<{ card: CardData }>());

export const favoriteFail = createAction('[card-list] FAVORITE_FAIL', props<{ error: Error }>());

export const unFavorite = createAction('[card-list] UNFAVORITE', props<{ slug: string }>());

export const unFavoriteSuccess = createAction('[card-list] UNFAVORITE_SUCCESS', props<{ card: CardData }>());

export const unFavoriteFail = createAction('[card-list] UNFAVORITE_FAIL', props<{ error: Error }>());
